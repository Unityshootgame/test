using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public bool isMine;
	public float dx,dy;
	public GameObject Bullet;
	public GameObject MyBullet;
	public int Life=10;

	// Update is called once per frame
	void Update () 
	{
	
		if (!NetworkViewManager.connected) 
		{
			return;
		}

		if (isMine) {
			dx = Input.GetAxis ("Horizontal");
			dy = Input.GetAxis ("Vertical");
			transform.Rotate (Vector3.forward, -dx * 2);
			transform.Translate (0.0f, dy * 0.1f, 0.0f);
			GetComponent<NetworkView> ().RPC ("MovePlayer", RPCMode.Others, transform.position, dx);
		
			if (Input.GetKey (KeyCode.Space)) {
				Instantiate (MyBullet, transform.position, transform.localRotation);
			}
		}
		if(Life<=0){
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider col){
		if (gameObject.tag == "Player") {
			return;
		}
		else if (col.gameObject.tag == "MyBullet") {
			Life--;
			GetComponent<NetworkView> ().RPC("Damege",RPCMode.Others,1);
			Destroy(col.gameObject);
		}
	}

	[RPC]
	public void MovePlayer(Vector3 position,float rotate)
	{
		transform.position = position;
		transform.Rotate (Vector3.forward,-rotate*2);
	}
	public void Damege(int dame){
		Life-=dame;
	}
	public void NewBullet(){
		Instantiate (Bullet, transform.position, transform.localRotation);
	}

}
