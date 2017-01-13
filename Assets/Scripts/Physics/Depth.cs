using UnityEngine;
using System.Collections;

public class Depth : MonoBehaviour {
	private float Zposition;
//	private float Xposition;
    public bool isShadow;
    [SerializeField]
    bool isPlayer = false;
	
	// Update is called once per frame
	void Update () 
	{
		Zposition = transform.position.z;
		if (!isShadow)
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-Zposition);
		else 
		{
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-Zposition - 3);
		}
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPlayer && other.tag == "Player")
            gameObject.GetComponent<Depth>().enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!isPlayer && other.tag == "Player")
            gameObject.GetComponent<Depth>().enabled = false;
    }
}