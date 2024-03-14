using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    void Start() {
        
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag + " : " + gameObject.name + " : " + Time.time);
        if (other.gameObject.CompareTag("Player")) {
            //+1 leaf game mager
            Destroy(gameObject);
        }
    }
}
