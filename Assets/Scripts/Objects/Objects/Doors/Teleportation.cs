using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {
	public bool transition;
	GameObject player;
	bool active = false;
	public GameObject otherTeleporter;
	public GameObject fade;
	public GameObject myPause;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	 void OnTriggerEnter(Collider other)
	{
		if (!active && other.tag == ("Player"))
        {
			myPause.GetComponent<PauseMenu2>().canUnPause = false;
			active = true;
			player.GetComponent<Controls>().enabled=false;
			if (transition == true)
			StartCoroutine(WaitAndSpawn(4.0F));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (active && other.tag == ("Player")) {
			active = false;
		}
	}

	IEnumerator WaitAndSpawn(float waitTime) {
		fade.GetComponent<ScreenFade>().anim.SetTrigger("FadeOut");
		yield return new WaitForSeconds(waitTime);
		player.GetComponent<Controls>().enabled=true;
		otherTeleporter.GetComponent<Teleportation>().active = true;
		player.transform.position = new Vector3 (otherTeleporter.transform.position.x,otherTeleporter.transform.position.y,otherTeleporter.transform.position.z);
		myPause.GetComponent<PauseMenu2>().canUnPause = true;
		}
}
