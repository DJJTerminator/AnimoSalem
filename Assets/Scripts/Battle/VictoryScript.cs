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
    GameObject bonusAwards;

    [SerializeField]
    GameObject score;
    [SerializeField]
    GameObject pressStart;
    bool isStarting = false;


    void Enabled()
    {
        isStarting = false;
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
        if (DataStorage.player == null)
            DataStorage.player = GameObject.Find("Player");
    }
    void Update()
    {
        if (pressStart.activeSelf && Input.anyKey && isActiveAndEnabled && !isStarting)
        {
            StartCoroutine(StartingGame());
            isStarting = true;
        }
    }

    public void VictoryScene(float ac, int xp)
    {
        dealt.text = CombatScript.damageGiven.ToString();
        taken.text = CombatScript.damageRecieved.ToString();
        experience.text = CombatScript.xpGained.ToString();
        totalTime.text = Mathf.Floor(CombatScript.battleTime / 60f).ToString("00") + ":" + Mathf.Floor(CombatScript.battleTime % 60f).ToString("00") + "." + (Mathf.Floor((CombatScript.battleTime % 60f) % 10f)).ToString("00");
        accuracy.text = ac.ToString() + "%";
        float tempScore = 100f;
        ac /= 100;
        if (ac > 1)
            ac = 1;
        tempScore -= (CombatScript.damageRecieved / (CombatScript.damageRecieved + CombatScript.damageGiven));
        tempScore += ac;
        if (tempScore > 100)
            tempScore = 100;
        if (1 * (CombatScript.battleTime / CombatScript.allottedTime) > 1)
            tempScore -= (2 * (CombatScript.battleTime / CombatScript.allottedTime));
        print(tempScore);
        tempScore /= 100;
        print(tempScore);
        //assorting the bonus awards (which are based on the grades)
        int awardedMoney;
        int awardedXP;
        int awardedItem;
        //the additional awards based on player's luck
        int myLuck = (DataStorage.luck / 2);

        if (tempScore >= 1)
        {
            tempScore = 0;
            grade.text = "S";
            awardedXP = Random.Range(155 + myLuck, 240 + myLuck);
            awardedMoney = Random.Range(155 + myLuck, 240 + myLuck);
            awardedItem = Random.Range(0 + myLuck, 10 + myLuck);
        }
        else if (tempScore >= .97f)
        {
            grade.text = "A+";
            awardedXP = Random.Range(155 + myLuck, 195 + myLuck);
            awardedMoney = Random.Range(155 + myLuck, 195 + myLuck);
        }
        else if (tempScore >= .93f)
        {
            grade.text = "A";
            awardedXP = Random.Range(120 + myLuck, 155 + myLuck);
            awardedMoney = Random.Range(120 + myLuck, 155 + myLuck);
        }
        else if (tempScore >= .90f)
        {
            grade.text = "B+";
            awardedXP = Random.Range(90 + myLuck, 120 + myLuck);
            awardedMoney = Random.Range(90 + myLuck, 120 + myLuck);
        }
        else if (tempScore >= .86f)
        {
            grade.text = "B";
            awardedXP = Random.Range(70 + myLuck, 90 + myLuck);
            awardedMoney = Random.Range(70 + myLuck, 90 + myLuck);
        }
        else if (tempScore >= .82f)
        {
            grade.text = "C+";
            awardedXP = Random.Range(50 + myLuck, 70 + myLuck);
            awardedMoney = Random.Range(50 + myLuck, 70 + myLuck);
        }
        else if (tempScore >= .78f)
        {
            grade.text = "C";
            awardedXP = Random.Range(35 + myLuck, 50 + myLuck);
            awardedMoney = Random.Range(35 + myLuck, 50 + myLuck);
        }
        else if (tempScore >= .75f)
        {
            grade.text = "D+";
            awardedXP = Random.Range(20 + myLuck, 35 + myLuck);
            awardedMoney = Random.Range(20 + myLuck, 35 + myLuck);
        }
        else if (tempScore >= .72f)
        {
            grade.text = "D";
            awardedXP = Random.Range(10 + myLuck, 20 + myLuck);
            awardedMoney = Random.Range(10 + myLuck, 20 + myLuck);
        }
        else
        {
            grade.text = "F";
        }



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
            xpBar.transform.localScale = new Vector3((float)DataStorage.XP / (float)DataStorage.maxXP, 1, 1);
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
        StartCoroutine(ReturnToGame(3f));
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
        victory.GetComponent<Animator>().Play("Score");
        yield return new WaitForSeconds(3f);
        levelingUp.Stop();
        prizes.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        pressStart.SetActive(true);
    }

    IEnumerator StartingGame()
    {
        pressStart.GetComponent<Animator>().Play("StartWasPressed");
        DataStorage.screenFader.Play("FadeOut", -1, 0);
        yield return new WaitForSeconds(4.5f);
        DataStorage.gameManager.GetComponent<StatActivation>().enabled = true;
        DataStorage.gameManager.GetComponent<InventoryActivation>().enabled = true;
        DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
        try
        {
            DataStorage.player.GetComponent<Controls>().enabled = true;
        }
        catch
        {
            DataStorage.player = GameObject.Find("Player");
            DataStorage.player.GetComponent<Controls>().enabled = true;
        }
        //getting the camera and turning on the follow script
        Camera myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        myCamera.GetComponent<CameraFollow>().enabled = true;
        pressStart.SetActive(false);
        gameObject.SetActive(false);
        score.SetActive(false);
        victory.SetActive(false);
        DataStorage.battleSystem.SetActive(false);
        DataStorage.UpdateHUD();
    }
}
