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
	//	Xposition = transform.position.x;
		Zposition = transform.position.z;
	//	transform.position = new Vector3(Xposition, Zposition, Zposition);
		if (!isShadow)
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-Zposition);
		else 
		{
		//	Vector3 pos = new Vector3 (transform.rotation.x,transform.rotation.y,transform.rotation.z);
			gameObject.GetComponent<SpriteRenderer> ().sortingOrder = (int)(-Zposition - 3);
		//	transform.rotation = pos;
		//	transform.rotation = Quaternion.Euler(-300, pos.y, pos.z);
			//transform.eulerAngles = pos; 
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