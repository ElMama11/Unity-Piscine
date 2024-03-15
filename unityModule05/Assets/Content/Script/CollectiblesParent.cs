using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (GameManager.Instance.isDead == true) {
			StartCoroutine(LeafRespawn());
		}
    }

	private IEnumerator LeafRespawn() {
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < transform.childCount; i++)
       		transform.GetChild(i).gameObject.SetActive(true);
	}
}
