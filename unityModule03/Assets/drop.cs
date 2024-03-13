using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : EventTrigger {
    public override void OnDrop(PointerEventData eventData) {
        // Check if the eventData is Null.
        if (eventData.pointerDrag != null) {
            Debug.Log("OnDrop called.");
        }
    }

	
    public void Onooooo() {
        // Check if the eventData is Null.

            Debug.Log("rgtrhthsrth.");
    }
}
