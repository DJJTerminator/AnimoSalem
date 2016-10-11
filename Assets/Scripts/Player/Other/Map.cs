using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	public GameObject myCamera;
	public GameObject mapCamera1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M))
	 	{
			if (myCamera.activeSelf == true)
			{
				myCamera.SetActive(false);
				mapCamera1.SetActive(true);
				return;
			}
			if (myCamera.activeSelf == false)
			{
				myCamera.SetActive(true);
				mapCamera1.SetActive(false);
				return;
			}
		}

	}
}
