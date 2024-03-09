using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
	private Rigidbody2D _rb;
    void Start() {
        _rb = gameObject.GetComponent<Rigidbody2D>();
		_speed = -0.5f;
    }

    void Update() {
		_rb.velocity = new Vector2(0, _speed);
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Base") {
			Destroy(gameObject);
			GameManager.Instance.DecreaseBaseHP(1);
		}
		else if (other.gameObject.tag == "Border")
			Destroy(gameObject);
	}
}
