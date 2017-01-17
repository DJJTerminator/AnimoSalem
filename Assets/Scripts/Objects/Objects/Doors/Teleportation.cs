using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {
	public bool transition;
	bool isActive = false;
	public GameObject otherTeleporter;
	public GameObject fade;

	// Use this for initialization
	void Start () 
	{
	}
	 void OnTriggerEnter(Collider other)
	{
		if (!isActive && other.tag == ("Player"))
        {
            DataStorage.player.GetComponent<Controls>().healing.Stop();
            DataStorage.pauseMenus.GetComponent<PauseMenu2>().canUnPause = false;
			isActive = true;
			DataStorage.player.GetComponent<Controls>().enabled=false;
			if (transition == true)
			StartCoroutine(WaitAndSpawn(4.0F));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (isActive && other.tag == ("Player"))
		 {
			isActive = false;
		}
	}

	IEnumerator WaitAndSpawn(float waitTime) {
        DataStorage.screenFader.Play("FadeOut", -1, 0);
        yield return new WaitForSeconds(waitTime);
		DataStorage.player.GetComponent<Controls>().enabled=true;
		otherTeleporter.GetComponent<Teleportation>().isActive = true;
		DataStorage.player.transform.position = new Vector3 (otherTeleporter.transform.position.x,otherTeleporter.transform.position.y,otherTeleporter.transform.position.z);
		DataStorage.pauseMenus.GetComponent<PauseMenu2>().canUnPause = true;
		}
}
