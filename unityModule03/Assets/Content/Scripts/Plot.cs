using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour, IDropHandler
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

	// private void OnMouseDown() {
	// 	if (_tower != null)
	// 		return;
	// 	Tower tmpTower = BuildManager.main.getSelectedTower();
	// 	if (tmpTower.cost > GameManager.Instance.currency) {
	// 		Debug.Log("You can't buy that");
	// 		return;
	// 	}
	// 	GameManager.Instance.spendCurrency(tmpTower.cost);
	// 	_tower = Instantiate(tmpTower.prefab, transform.position, Quaternion.identity);
	// }

	public void OnDrop(PointerEventData eventData) {
		Debug.Log("YEP");
	if (_tower != null)
			return;
		Tower tmpTower = BuildManager.main.getSelectedTower();
		if (tmpTower.cost > GameManager.Instance.currency) {
			Debug.Log("You can't buy that");
			return;
		}
		GameManager.Instance.spendCurrency(tmpTower.cost);
		_tower = Instantiate(tmpTower.prefab, transform.position, Quaternion.identity);
	}
}
