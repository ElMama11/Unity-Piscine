 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 
public class DeathReSpawn : MonoBehaviour
{
	public Vector3 initialPos;
	void Start() {

		initialPos = transform.position;
	}
	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Floor")
		{
			Debug.Log("Game Over");
			Destroy(this.gameObject);
			// transform.localPosition = initialPos;
		}
	}
}