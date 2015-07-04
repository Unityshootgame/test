using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float bullet_speed;

	void Update () {
		transform.position += transform.forward*bullet_speed;
	}
}
