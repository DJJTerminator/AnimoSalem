using UnityEngine;
using System.Collections;

public class IndestructableScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);

		if (FindObjectsOfType(GetType()).Length > 2)
         {
             Destroy(transform.gameObject);
         }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
