using UnityEngine;
using System.Collections;

public class ItemPickups : MonoBehaviour {
	public float thisWeight;
	public int ammoCount;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(transform.gameObject);

		float Zposition;
		Zposition = transform.position.z;
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-Zposition);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
