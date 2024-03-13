using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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

	public void OnDrop(PointerEventData eventData) {
		Tower tmpTower;
		if (_tower != null)
			return;
		if (eventData.pointerDrag.name == "TurretLow")
			BuildManager.main.SetSelectedTower(0);
		else if (eventData.pointerDrag.name == "TurretMid")
			BuildManager.main.SetSelectedTower(1);
		else if (eventData.pointerDrag.name == "TurretHigh")
			BuildManager.main.SetSelectedTower(2);
		tmpTower = BuildManager.main.getSelectedTower();
		if (tmpTower.cost > GameManager.Instance.currency) {
			Debug.Log("You can't buy that");
			// gameObject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
			return;
		}
		GameManager.Instance.spendCurrency(tmpTower.cost);
		_tower = Instantiate(tmpTower.prefab, transform.position, Quaternion.identity);
	}
}
