using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{   
    public Transform teleportTarget;
    public GameObject player;

    void OnTriggerEnter(Collider other) {
        player = other.gameObject;
        player.transform.position = teleportTarget.transform.position + new Vector3(0, 0.5f, 0);
    }
}
