using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
	private Rigidbody2D _rb;
	[SerializeField] private float _health = 3f;
    void Start() {
        _rb = gameObject.GetComponent<Rigidbody2D>();
		_speed = -0.5f;
    }

    void Update() {
		_rb.velocity = new Vector2(0, _speed);
    }

	public void takeDamage(float dmg) {
		_health -= dmg;
		Debug.Log("Mob HP: " + _health);
		if (_health <= 0) {
			Destroy(gameObject);
		}
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
