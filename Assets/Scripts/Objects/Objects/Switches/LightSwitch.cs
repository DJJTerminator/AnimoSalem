using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	public GameObject _light;
	public bool isBroken;
	public AudioSource turnOn;
	public AudioSource turnOff;
	public GameObject exclamation;
	float wait;


	public void LightsOnOff()
	{
		
		if (_light.activeSelf) {
			gameObject.GetComponent<SpriteRenderer> ().flipY = false;
			_light.SetActive (false);
			turnOff.Play ();
		} else {
			gameObject.GetComponent<SpriteRenderer> ().flipY = true;
			turnOn.Play ();
			if (isBroken)
				StartCoroutine (LightsOn (.08f));
			else
				_light.SetActive (true);
		}

	}

	IEnumerator LightsOn (float waitTime)
	{
		_light.SetActive(true);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(false);
		yield return new WaitForSeconds(Random.Range(.02f,waitTime));
		_light.SetActive(true);

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
		if (Input.GetKeyDown ("return") && Time.time > wait) 
		{
			wait = Time.time + .2f;
			LightsOnOff ();

		}

	}
}
