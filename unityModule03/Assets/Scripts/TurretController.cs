using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	[Header("References")]
	public LayerMask enemyMask;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private Transform _firingPoint;

	[Header("Attribute")]
	private float _targetRange = 0.3f;
	private float _bps; // Bullets per second

	public Transform _target;
	private float _timeUntilFire;


	void Start() {
		if (gameObject.tag == "TurretLow")
			_bps = 1f;
		else if (gameObject.tag == "TurretMid")
			_bps = 1.5f;
		else if (gameObject.tag == "TurretHigh")
			_bps = 2f;
	}

	void Update() {
		if (_target == null) {
			findTarget();
			return;
		}
		if (!IsTargetisInRange()) {
			_target = null;
		}
		else {
			_timeUntilFire += Time.deltaTime;
			if (_timeUntilFire >= 1f / _bps) {
				shoot();
				_timeUntilFire = 0f;
			}
		}
	}

	private void shoot() {
		GameObject bulletObj = Instantiate(_bulletPrefab,_firingPoint.position, Quaternion.identity);
		Bullet bulletScript = bulletObj.GetComponent<Bullet>();
		bulletScript.setBasicDmg(getBasicDmgOfTurret());
		bulletScript.setTarget(_target);
	}

	private void findTarget() {
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetRange, Vector2.zero, 0f, enemyMask);
		if (hits.Length > 0) {
			Transform closestTarget = null;
			float closestDistance = Mathf.Infinity;
			foreach (var hit in hits) {
				float distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
				if (distanceToTarget < closestDistance) {
					closestDistance = distanceToTarget;
					closestTarget = hit.transform;
				}
			}
			if (closestTarget != null)
				_target = closestTarget;
		}
	}

	private bool IsTargetisInRange() {
		return Vector2.Distance(_target.position, transform.position) <= _targetRange;
	}

	private void OnDrawGizmosSelected() {
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(transform.position, transform.forward, _targetRange);
	}

	public float getBasicDmgOfTurret() {
		if (gameObject.tag == "TurretLow")
			return 3f;
		else if (gameObject.tag == "TurretMid")
			return 2f;
		else if (gameObject.tag == "TurretHigh")
			return 1f;
		return 0f;
	}
}
