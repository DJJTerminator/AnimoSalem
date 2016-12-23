﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TakingDamageScript : MonoBehaviour {
    int enemyDamage = 20;
    public GameObject Backgrounds;
    public GameObject[] arrowKeys;
    public GameObject actionText;
    int direction;
    //0 is turned off
    //1 is left
    //2 is right
    //3 is up
    //4 is down
    public AudioSource success;
    public AudioSource failed;
    IEnumerator timeFailure;

    void Start()
    {
        actionText.SetActive(true);
        direction = Random.Range(1,5);
        arrowKeys[4].SetActive(true);
        switch (direction)
        {
            default:
                direction = 4;
                goto case 4;
            case 1:
                arrowKeys[0].SetActive(true);
                arrowKeys[0].GetComponent<Animator>().Play("LeftArrow");
                break;
            case 2:
                arrowKeys[1].SetActive(true);
                arrowKeys[1].GetComponent<Animator>().Play("RightArrow");
                break;
            case 3:
                arrowKeys[2].SetActive(true);
                arrowKeys[2].GetComponent<Animator>().Play("UpArrow");
                break;
            case 4:
                arrowKeys[3].SetActive(true);
                arrowKeys[3].GetComponent<Animator>().Play("DownArrow");
                break;
        }
        actionText.GetComponent<Text>().text = "Avoid";
        timeFailure = TimeFailure(2f);
        StartCoroutine(timeFailure);
    }
    void Update()
    {
        switch(direction)
        {
            case 1:
                if (Input.GetAxis("Horizontal") < 0)
                    Dodge();
                else if ((Input.GetAxis("Horizontal") > 0) || Input.GetAxis("Vertical") != 0)
                    StartCoroutine (Failed(2f));
                break;
            case 2:
                if (Input.GetAxis("Horizontal") > 0)
                    Dodge();
                else if ((Input.GetAxis("Horizontal") < 0) || Input.GetAxis("Vertical") != 0)
                    StartCoroutine(Failed(2f));
                break;
            case 3:
                if (Input.GetAxis("Vertical") > 0)
                    Dodge();
                else if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") != 0))
                    StartCoroutine(Failed(2f));
                break;
            case 4:
                if (Input.GetAxis("Vertical") < 0)
                    Dodge();
                else if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") != 0))
                    StartCoroutine(Failed(2f));
                break;
        }
    }


    //the player took too long to respond to the action sequence
    IEnumerator TimeFailure(float waitTime)
    {
        yield return new WaitForSeconds(1.5f);
        TakeDamage();
        failed.Play();
        actionText.GetComponent<Text>().text = "Failed!";
        actionText.GetComponent<Animator>().Play("Failed", -1, 0f);
        direction = 0;
        for (int i = 0; i < arrowKeys.Length; i++)
            arrowKeys[i].SetActive(false);
        yield return new WaitForSeconds(waitTime);
        actionText.SetActive(false);
    }
    //turning off the action text after waiting for a short duration
    IEnumerator Failed(float waitTime)
        {
        TakeDamage();
        StopCoroutine(timeFailure);
        failed.Play();
        actionText.GetComponent<Text>().text = "Failed!";
        actionText.GetComponent<Animator>().Play("Failed", -1, 0f);
        direction = 0;
        for (int i = 0; i < arrowKeys.Length; i++)
            arrowKeys[i].SetActive(false);
        yield return new WaitForSeconds(waitTime);
        actionText.SetActive(false);
    }
    //the dodge function
    public void Dodge()
    {
        //prevent the player from shooting
        CombatScript.fireRate = Time.time + 1.5f;
        StopCoroutine(timeFailure);
        success.Play();
        direction = 0;
        for (int i = 0; i < arrowKeys.Length; i++)
            arrowKeys[i].SetActive(false);
        Backgrounds.GetComponent<Animator>().Play("Dodge", -1, 0f);
        actionText.GetComponent<Animator>().Play("Success", -1, 0f);
        actionText.GetComponent<Text>().text = "Success";
		//adding xp for the successful dodge
		//GameObject xpGain= gameObject.GetComponent<CombatScript>().textXP;
		gameObject.GetComponent<CombatScript>().textXP.GetComponent<Text>().text = "+ " + (5 * DataStorage.currentLevel + (DataStorage.intelligence/4)) + "  XP";
		gameObject.GetComponent<CombatScript>().textXP.GetComponent<Animator>().Play("XPGain", -1, 0f);
		gameObject.GetComponent<CombatScript>().myXP += (5 * DataStorage.currentLevel + (DataStorage.intelligence/4));
		//textXP.GetComponent<Animator>().Play("XPGain", -1, 0f);
    }

    //taking a hit
    public void TakeDamage()
    {
        //prevent the player from shooting
        CombatScript.fireRate = Time.time + 1.5f;
        DataStorage.health -= enemyDamage;
        Backgrounds.GetComponent<Animator>().Play("ScreneHitLeft", -1, 0f);

        //StartCoroutine (ShakeUntil(.05f));
        //play it sound
        //play voice soun
    }
    //animating the screen for taking damage
    IEnumerator ShakeUntil(float waitTime)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Backgrounds.GetComponent<Animator>().Play("ScreenShake", -1, 0f);
        }
    }
}
