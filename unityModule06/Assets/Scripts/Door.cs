
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
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
		openDoor = new Vector3(0, 90, 0);
		pivot.transform.Rotate(openDoor);
	}

	void OnTriggerExit(Collider other) {
		closeDoor = new Vector3(0, -90, 0);
		pivot.transform.Rotate(closeDoor);
	}
}
