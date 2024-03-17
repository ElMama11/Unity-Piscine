
using System.Collections;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
	private Transform pivot;
	private Vector3 openDoor;
	private Vector3 closeDoor;
    void Start() {
		pivot = this.transform.parent.GetChild(1);
    }

    void Update() {
        
    }

	void OnTriggerEnter(Collider other) {
		if (GameManager.Instance.keycount >= 3) {
			openDoor = new Vector3(0, 90, 0);
			pivot.transform.Rotate(openDoor);
		}
		else {
			Debug.Log("You have " + GameManager.Instance.keycount + " keys, need 3");
		}
	}

	void OnTriggerExit(Collider other) {
		if (GameManager.Instance.keycount >= 3) {
			closeDoor = new Vector3(0, -90, 0);
			pivot.transform.Rotate(closeDoor);
		}
	}
}
