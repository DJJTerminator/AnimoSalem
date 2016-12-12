using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    public GameObject Victory;
    public GameObject xpBar;
    public Text xpText;
    int xpValue;
    public GameObject LevelUp;
    public AudioSource levelingUp;
    public AudioSource levelUp;
    float addWait = .05f;

    void Enabled()
    {
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
    }

    public void VictoryScene(int xp)
    {
        xpValue = 10000;
        xpText.text = xp.ToString();
        Victory.SetActive(true);
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
        StartCoroutine(LoadXP(addWait));
    }

    IEnumerator LoadXP(float waitTime)
    {
        addWait = waitTime;
        while (xpValue > 0)
        {
            if (LevelUp.activeSelf)
                LevelUp.SetActive(false);
            if (!levelingUp.isPlaying)
                levelingUp.Play();
            xpValue -= 1;
            xpText.text = xpValue.ToString();
            DataStorage.XP += 1;
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
                addWait = addWait / 10f;
                waitTime = addWait / DataStorage.maxXP;
                LevelUp.SetActive(true);
                //display animation  gain level
                //play sound
                //etc
                yield return new WaitForSeconds(4f);
            }
            if (xpValue > 0)
            yield return new WaitForSeconds(waitTime);
        }
        xpText.text = null;
    }
}
