using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TakingDamageScript : MonoBehaviour {
	public int enemyDamage = 20;
    public GameObject Backgrounds;
    public GameObject[] arrowKeys;
    public GameObject actionText;
    int direction;
	//for the arrow keys,
    //0 is turned off
    //1 is left
    //2 is right
    //3 is up
    //4 is down
    public AudioSource success;
    public AudioSource failed;
    public AudioSource gameOver;
    public AudioSource gameOver2;
    public AudioSource gameOver3;
    public AudioSource gameOver4;

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
        DataStorage.failedDodges++;
        CombatScript.dodgeFail++;
        TakeDamage();
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
        DataStorage.failedDodges++;
        CombatScript.dodgeFail++;
        TakeDamage();
        StopCoroutine(timeFailure);
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
        DataStorage.successfulDodges++;
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
        //adding to the amount of xp that is gained for this battle
        CombatScript.xpGained += (5 * DataStorage.currentLevel + (DataStorage.intelligence / 4));
    }

    //taking a hit
    public void TakeDamage()
    {
        //prevent the player from shooting
        CombatScript.fireRate = Time.time + 1.5f;
		DataStorage.health -= enemyDamage;
        DataStorage.damageTaken += enemyDamage;
        CombatScript.damageRecieved += enemyDamage;
        if (DataStorage.health > enemyDamage)
        {
            Backgrounds.GetComponent<Animator>().Play("ScreneHitLeft", -1, 0f);
            DataStorage.screenFader.Play("DamageEffect", -1, 0f);
            failed.Play();
        }
        else
        {
            Backgrounds.GetComponent<Animator>().Play("PlayerDead", -1, 0f);
            StartCoroutine(DeathSound(1f));
        }
		StartCoroutine (HealthDrain(enemyDamage));

        //StartCoroutine (ShakeUntil(.05f));
        //play hit sound
        //play voice sound
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
	//drain health function
	    IEnumerator HealthDrain(float damageTaken)
    {
        while (damageTaken > 0 && DataStorage.health > 0)
		{
			yield return new WaitForSeconds(.016f);
			DataStorage.health -=1;
			damageTaken-=1;
			DataStorage.UpdateHUDHealth();
		}
        //Player dies
		if (DataStorage.health <= 0)
		{
            //setting the death animation
            DataStorage.screenFader.Play("Dead");
			GetComponent<CombatScript>().enabled = false;
			DataStorage.HUD.SetActive(false);
			yield return new WaitForSeconds(15f);
			DataStorage.screenFader.Play("Default");
		//	GetComponent<CombatScript>().enabled = true;
			DataStorage.HUD.SetActive(true);
			DataStorage.player.GetComponent<Controls>().enabled = true;
		//	DataStorage.gameManager.GetComponent<StatActivation> ().enabled = true;
		//	DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = true;
		//	DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
			DataStorage.battleSystem.SetActive(false);
            DataStorage.GameOver();
		}
    }
    IEnumerator DeathSound(float waitTime)
    {
        //modifying the sound and playing multiple choruses
        gameOver.Play();
        yield return new WaitForSeconds(3f);
        gameOver2.Play();
        yield return new WaitForSeconds(1.5f);
        gameOver3.Play();
        yield return new WaitForSeconds(2.5f);
        gameOver4.Play();
        gameOver4.time = 3f;
        gameOver4.volume = 0f;
        //starting the echo sound
        StartCoroutine(DeathSoundEcho(.2f));
    }
    //this is the echo sound. This is usd so that it plys in the background of the death sound
    IEnumerator DeathSoundEcho(float waitTime)
    {
        while (gameOver4.volume < 1)
        {
            yield return new WaitForSeconds(.2f);
            gameOver4.volume += .1f;
        }
        while (gameOver4.volume > 0)
        {
            yield return new WaitForSeconds(.2f);
            gameOver4.volume -= .1f;
        }
    }
}
