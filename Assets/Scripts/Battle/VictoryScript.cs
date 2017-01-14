using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public GameObject victory;
    public GameObject xpBar;
    public Text xpText;
    int xpValue;
    public GameObject LevelUp;
    public AudioSource levelingUp;
    public AudioSource levelUp;
	[SerializeField]
	GameObject prizes;
    //result texts
    [SerializeField]
    Text dealt;
    [SerializeField]
    Text taken;
    [SerializeField]
    Text grade;
    [SerializeField]
    Text accuracy;
    [SerializeField]
    Text experience;
    [SerializeField]
    Text totalTime;

    [SerializeField]
    GameObject score;



    void Enabled()
    {
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
		if (DataStorage.player == null)
			DataStorage.player = GameObject.Find ("Player");
    }

    public void VictoryScene(float ac, int xp)
    {
        dealt.text = CombatScript.damageGiven.ToString();
        taken.text = CombatScript.damageRecieved.ToString();
        experience.text = CombatScript.xpGained.ToString();
        totalTime.text = Mathf.Floor(CombatScript.battleTime / 60f).ToString("00") + ":" + Mathf.Floor(CombatScript.battleTime % 60f).ToString("00") + "." + (Mathf.Floor((CombatScript.battleTime % 60f) % 10f)).ToString("00");
        accuracy.text = ac.ToString() + "%";
        float tempScore = 100f;
        print(tempScore);
        tempScore -= ac + (CombatScript.damageRecieved/(CombatScript.damageRecieved + CombatScript.damageGiven));
        print(tempScore);
        tempScore -= (((int)CombatScript.battleTime ^ (int)CombatScript.allottedTime)/ CombatScript.allottedTime);
        print(tempScore);
        tempScore /= 100;
        print(tempScore);
        if (tempScore < 0)
        {
            tempScore = 0;
            grade.text = "S";
        }
        else if (tempScore < .03f)
            grade.text = "A+";
        else if (tempScore < .06f)
            grade.text = "A";
        else if (tempScore < .1f)
            grade.text = "B+";
        else if (tempScore < .14f)
            grade.text = "B";
        else if (tempScore < .18f)
            grade.text = "C+";
        else if (tempScore < .22f)
            grade.text = "C";
        else if (tempScore < .26f)
            grade.text = "D+";
        else if (tempScore < .3f)
            grade.text = "D";
        else 
            grade.text = "F";

        xpValue = xp;
        xpText.text = xp.ToString();
        victory.SetActive(true);
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
        StartCoroutine(LoadXP(.02f));
    }

    IEnumerator LoadXP(float waitTime)
    {
        while (xpValue > 0)
        {
            if (LevelUp.activeSelf)
                LevelUp.SetActive(false);
            if (!levelingUp.isPlaying)
                levelingUp.Play();
            //checking to see if the xp value is greater than the current level
            if (xpValue > 1 * DataStorage.currentLevel)
            {
                xpValue -= 1 * DataStorage.currentLevel;
                DataStorage.XP += 1 * DataStorage.currentLevel;
            }
            else
            {
                DataStorage.XP += xpValue;
                xpValue = 0;
            }
            xpText.text = xpValue.ToString();
            xpBar.transform.localScale = new Vector3((float)DataStorage.XP/(float)DataStorage.maxXP,1,1);
            if (DataStorage.XP >= DataStorage.maxXP)
            {
                //player has gained a level
                levelingUp.Stop();
                levelUp.Play();
                DataStorage.XP = 0;
                DataStorage.maxXP += 100;
                xpBar.transform.localScale = new Vector3((float)DataStorage.XP / (float)DataStorage.maxXP, 1, 1);
                DataStorage.playerStats += 5;
                //decreasing the wait for the next time the player levels up
                LevelUp.SetActive(true);
                DataStorage.currentLevel++;
				DataStorage.levelBackground.Play("StatsRemain");
				DataStorage.levelNumber.text = DataStorage.currentLevel.ToString();
                yield return new WaitForSeconds(4f);
            }
            yield return new WaitForSeconds(waitTime);
        }
        xpText.text = null;
        //StartCoroutine (MysteryBox(3f));
		StartCoroutine (ReturnToGame(3f));
    }
	//activating the mysterybox
	    IEnumerator MysteryBox(float waitTime)
    {
		levelingUp.Stop();
        yield return new WaitForSeconds(waitTime);
		prizes.SetActive(true);
    }

    IEnumerator ReturnToGame(float waitTime)
    {
        score.SetActive(true);
        victory.GetComponent<Animator>().Play ("Score");
        yield return new WaitForSeconds(3f);
        levelingUp.Stop();
		prizes.SetActive(false);
        yield return new WaitForSeconds(waitTime);
		DataStorage.gameManager.GetComponent<StatActivation> ().enabled = true;
		DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = true;
		DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
		try
		{
			DataStorage.player.GetComponent<Controls>().enabled = true;
		}
		catch
		{
			DataStorage.player = GameObject.Find ("Player");
			DataStorage.player.GetComponent<Controls>().enabled = true;
		}
				//getting the camera and turning on the follow script
		Camera myCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	    myCamera.GetComponent<CameraFollow>().enabled = true;
        gameObject.SetActive(false);
        score.SetActive(false);
        victory.SetActive(false);
        DataStorage.battleSystem.SetActive(false);
        DataStorage.UpdateHUD();
    }
}
