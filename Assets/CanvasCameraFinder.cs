using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasCameraFinder : MonoBehaviour {

	// Use this for initialization
	void Start() 
	{
		if (gameObject.GetComponent<Canvas>().worldCamera == null)
			gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

		void OnEnable() 
	{
		if (gameObject.GetComponent<Canvas>().worldCamera == null)
			gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

			void OnDisable() 
	{
		StartCoroutine (FindTheCamera(1f));
	}

	IEnumerator FindTheCamera(float waitTime)
	{
		for (int i = 0; i < 5; i ++)
		{
			yield return new WaitForSeconds (waitTime);
			if (gameObject.GetComponent<Canvas>().worldCamera == null)
				gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		}
	}

}
