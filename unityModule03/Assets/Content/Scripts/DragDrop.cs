using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler  {
    private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	[SerializeField] private Canvas canvas;
    public Vector2 originalPosition;
	[SerializeField] private Image _image;
	private Color _originalColor;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		_image = gameObject.GetComponent<Image>();
		_originalColor = _image.color;
    }
    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnBeginDrag(PointerEventData eventData) {
		originalPosition = rectTransform.anchoredPosition;
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
		canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
		rectTransform.anchoredPosition = originalPosition;
		_image.color = _originalColor;

    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		 Debug.Log("Dropped object was: " + eventData.pointerDrag.name);
		if (eventData.pointerDrag.name == "TurretLow")
			if (GameManager.Instance.currency < 100)
				_image.color = new Color(255,0,0);
		if (eventData.pointerDrag.name == "TurretMid")
			if (GameManager.Instance.currency < 50)
				_image.color = new Color(255,0,0);
		if (eventData.pointerDrag.name == "TurretHigh")
			if (GameManager.Instance.currency < 150)
				_image.color = new Color(255,0,0);
	}
}
