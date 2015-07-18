using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public bool isMine;
	public float dx,dy;
	public GameObject Bullet;
	public int Bullet_Tima;
	public int Life = 10;

	// Update is called once per frame
	void Update () 
	{
		Bullet_Tima++;
		if (!NetworkViewManager.connected) 
		{
			return;
		}

		if (isMine) 
		{
			if (Input.GetKey (KeyCode.Space) && Bullet_Tima%30==1) {
				Network.Instantiate(Bullet,transform.position,transform.localRotation,0);
			}
			dx = Input.GetAxis("Horizontal");
			dy = Input.GetAxis ("Vertical");
			transform.Rotate (Vector3.forward,-dx*2);
			transform.Translate(0.0f,dy*0.1f,0.0f);
			GetComponent<NetworkView>().RPC ("MovePlayer",RPCMode.Others,transform.position,dx);
		}
		if (Life <= 0) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (gameObject.tag == "Player") {
			return;
		} else if (col.gameObject.tag == "Bullet") {
			Life--;
			GetComponent<NetworkView>().RPC("Damege",RPCMode.Others,1);
			Destroy(col.gameObject);
		}
	}

	[RPC]
	public void MovePlayer(Vector3 position,float rotate)
	{
		transform.position = position;
		transform.Rotate (Vector3.forward,-rotate*2);
	}
	[RPC]
	public void Damege(int dame){
		Life -= dame;
	}

}