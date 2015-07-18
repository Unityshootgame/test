using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float bullet_speed;
	private float life=0;
	public Controller Conlife;

	void Update () {
		life++;
		transform.position += transform.up*bullet_speed;
		if (life > 60) {
			Destroy (gameObject);
		}
	}
	void OnCTriggerEnter(Collision Col) {
		Conlife.Damege (1);
		Destroy(gameObject);
		
	}
}
