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
	[SerializeField]
	GameObject prizes;

    void Enabled()
    {
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
		if (DataStorage.player == null)
			DataStorage.player = GameObject.Find ("Player");
    }

    public void VictoryScene(int xp)
    {
        xpValue = xp;
        xpText.text = xp.ToString();
        Victory.SetActive(true);
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
				//getting the camera and turning off the follow script
		Camera myCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		if (myCamera != null)
			myCamera.GetComponent<CameraFollow>().enabled = true;
        gameObject.SetActive(false);
        DataStorage.battleSystem.SetActive(false);
        DataStorage.UpdateHUD();
    }
}
