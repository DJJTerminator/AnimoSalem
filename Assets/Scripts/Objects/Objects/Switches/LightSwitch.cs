using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	public GameObject _light;
	public bool isBroken;
	public AudioSource switchSound;
    public AudioSource dyingLight;
	GameObject exclamation;
    GameObject shadow1;
    GameObject shadow2;

    void Start()
    {
        exclamation = GameObject.Find("Player/PlayerIcons/Exclamation");
        shadow1 = GameObject.Find("Player/Shadows/ShadowPivotPoint1/myShadow");
        shadow2 = GameObject.Find("Player/Shadows/ShadowPivotPoint2/myShadow2");

    }

 


	public void LightsOnOff()
	{
		if (_light.activeSelf) {
			gameObject.GetComponent<SpriteRenderer> ().flipY = false;
			_light.SetActive (false);
            switchSound.Play();
            shadow1.GetComponent<SpriteRenderer>().enabled = false;
            shadow2.GetComponent<SpriteRenderer>().enabled = false;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().flipY = true;
            if (isBroken)
                StartCoroutine(LightsOn(.08f));
            else
            {
                shadow1.GetComponent<SpriteRenderer>().enabled = true;
                shadow2.GetComponent<SpriteRenderer>().enabled = true;
                _light.SetActive(true);
                switchSound.Play();
            }
		}

	}

	IEnumerator LightsOn (float waitTime)
	{
        dyingLight.Play();
		_light.SetActive(true);
        shadow1.GetComponent<SpriteRenderer>().enabled = true;
        shadow2.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
        shadow1.GetComponent<SpriteRenderer>().enabled = false;
        shadow2.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
        shadow1.GetComponent<SpriteRenderer>().enabled = true;
        shadow2.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
        shadow1.GetComponent<SpriteRenderer>().enabled = false;
        shadow2.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
        shadow1.GetComponent<SpriteRenderer>().enabled = true;
        shadow2.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
        shadow1.GetComponent<SpriteRenderer>().enabled = false;
        shadow2.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
        shadow1.GetComponent<SpriteRenderer>().enabled = true;
        shadow2.GetComponent<SpriteRenderer>().enabled = true;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
        shadow1.GetComponent<SpriteRenderer>().enabled = false;
        shadow2.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
        shadow1.GetComponent<SpriteRenderer>().enabled = true;
        shadow2.GetComponent<SpriteRenderer>().enabled = true;

	}
		void OnTriggerEnter(Collider other)
		{
			if (other.tag =="Player")
				exclamation.SetActive (true);
		}
		void OnTriggerExit(Collider other)
		{
			if (other.tag =="Player")
				exclamation.SetActive (false);
		}

	void OnTriggerStay (Collider other)
	{
		if (other.tag =="Player")
		if (Input.GetKeyDown ("return")) 
		{
			LightsOnOff ();

		}

	}
}
