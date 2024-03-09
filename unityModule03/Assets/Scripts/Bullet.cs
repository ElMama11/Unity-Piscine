using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Attributes")]
    private float _bulletSpeed = 1f;
    private float _bulletDamage = 1f;
    private float _basicDmg;
    
    private Transform _target;
    void Start() {

    }

    private void FixedUpdate() {
        if (!_target)
            return;
        Vector2 direction = (_target.position - transform.position).normalized;
        _rb.velocity = direction * _bulletSpeed;
    }

    public void setTarget(Transform aTarget) {
        _target = aTarget;
    }

    public void setBasicDmg(float dmg) {
        _basicDmg = dmg;
    }
    private void OnTriggerEnter2D (Collider2D other) {
        other.gameObject.GetComponent<EnemyController>().takeDamage(_bulletDamage * _basicDmg);
        Destroy(gameObject);
    }
}
