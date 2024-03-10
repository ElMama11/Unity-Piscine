using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private SpriteRenderer _sr;
	[SerializeField] private Color _hoverColor;

	private GameObject _tower;
	private Color _startColor;

	private void Start() {
		_startColor = _sr.color;
	}
	private void OnMouseEnter() {
		_sr.color = _hoverColor;
	}

	private void OnMouseExit() {
		_sr.color = _startColor;
	}

	private void OnMouseDown() {
		if (_tower != null)
			return;
		GameObject tmpTower = BuildManager.main.getSelectedTower();
		_tower = Instantiate(tmpTower, transform.position, Quaternion.identity);
	}
}
