using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasCameraFinder : MonoBehaviour 
{

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

		void OnRenderObject()
	{
	//finding the main camera and attaching it to the canvas as son as a new scene has loaded
			if (gameObject.GetComponent<Canvas>().worldCamera == null)
			{
			GameObject myCanvas = GameObject.Find ("All Canvases/Canvas");
			myCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
			DataStorage.battleSystem.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
			}
	}
}
