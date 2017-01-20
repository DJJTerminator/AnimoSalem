﻿using UnityEngine;
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
    IEnumerator Awards(string[] name, int type, int amount, int item, int xp, int money)
    {
        print(name[type] + " " + amount);
        yield return new WaitForSeconds(4f);
        bonusAwards.SetActive(true);
        Text awardedText = GameObject.Find("All Canvases/BattleSystem/Victory/BonusAwards/Award1").GetComponent<Text>();
        awardedText.text = "Bonus: " + "+"+ xp + "xp";
        awardedText = GameObject.Find("All Canvases/BattleSystem/Victory/BonusAwards/Award2").GetComponent<Text>();
        awardedText.text = "Awarded: " + "+ $" + money;
        awardedText = GameObject.Find("All Canvases/BattleSystem/Victory/BonusAwards/Award3").GetComponent<Text>();
        if (amount > 0)
            awardedText.text = name[type] +": " + "+"+amount;
        else
            awardedText.text =null;
        awardedText = GameObject.Find("All Canvases/BattleSystem/Victory/BonusAwards/Award4").GetComponent<Text>();
        //the item that was found
        if (item / 100 >= .95f)
            awardedText.text = "Discovered: Small Aid";
        else if (item / 100 >= .95f)
            awardedText.text = "Discovered: Small Aid";
        else if (item / 100 >= .95f)
            awardedText.text = "Discovered: Small Aid";
        else if (item / 100 >= .95f)
            awardedText.text = "Discovered: Small Aid";
        else
            awardedText.text = null;
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
        ac = 1 - ac;
        tempScore -= (CombatScript.damageRecieved / (CombatScript.damageRecieved + CombatScript.damageGiven));
        tempScore -= CombatScript.dodgeFail * 3;
        CombatScript.dodgeFail = 0;

        if (1 * (CombatScript.battleTime / CombatScript.allottedTime) > 1)
        {
            tempScore -= (2 * (CombatScript.battleTime / CombatScript.allottedTime));
            tempScore /= 100;
            tempScore -= ac;
        }
        else
        {
            tempScore += (2 * (CombatScript.battleTime / CombatScript.allottedTime));
            tempScore /= 100;
            tempScore -= ac;
        }

        print(tempScore);
        //assorting the bonus awards (which are based on the grades)
        int awardedMoney;
        int awardedXP;
        int awardedItem;
        int awardedAmmo;
        int ammoType;//this is the type of ammo that is awarded
        string[] ammoName = { null, "Handgun Ammo", "Shotgun Shells", "Rifle Rounds", "Machinegun Bullets", "Magnum Rounds", "Explosive Rounds" };
        //the additional awards based on player's luck
        int myLuck = (DataStorage.luck / 2);

        if (tempScore >= 1)
        {

            tempScore = 0;
            grade.text = "S";
            awardedXP = Random.Range(135 + myLuck, 150 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .97f)
        {
            grade.text = "A+";
            awardedXP = Random.Range(120 + myLuck, 135 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .93f)
        {
            grade.text = "A";
            awardedXP = Random.Range(90 + myLuck, 105 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .90f)
        {
            grade.text = "B+";
            awardedXP = Random.Range(75 + myLuck, 90 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .86f)
        {
            grade.text = "B";
            awardedXP = Random.Range(60 + myLuck, 75 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .82f)
        {
            grade.text = "C+";
            awardedXP = Random.Range(45 + myLuck, 60 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .78f)
        {
            grade.text = "C";
            awardedXP = Random.Range(35 + myLuck, 45 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
            xp += awardedXP;
            }
        else if (tempScore >= .75f)
        {
            grade.text = "D+";
            awardedXP = Random.Range(15 + myLuck, 25 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else if (tempScore >= .72f)
        {
            grade.text = "D";
            awardedXP = Random.Range(5 + myLuck, 15 + myLuck);
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 920 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 40)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;
            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }
        else
        {
            grade.text = "F";
            awardedXP = 0; 
            awardedMoney = Random.Range(0, 50 + myLuck);
            //randomizing the chances of finding ammo, and than randomizing the chances of finding certain ammo
            int magicFind = Random.Range(0, 1000 + myLuck);
            if (magicFind >= 700 - myLuck)
            {
                magicFind = Random.Range(0, 600);
                //randomizing ammo type
                if (magicFind >= 450)
                    ammoType = 1;
                else if (magicFind >= 325)
                    ammoType = 2;
                else if (magicFind >= 250)
                    ammoType = 3;
                else if (magicFind >= 100)
                    ammoType = 4;
                else if (magicFind >= 20)
                    ammoType = 5;
                else
                    ammoType = 6;
            }
            else
                ammoType = 0;

            awardedItem = Random.Range(myLuck, 100);
            switch (ammoType)
            {
                default:
                    ammoType = 0;
                    awardedAmmo = 0;
                    break;
                case 1://handgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 5 + myLuck);
                    DataStorage.HGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 2://shotgun bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.SGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 3://rifle bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.rifleAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 4://machine bullets
                    awardedAmmo = Random.Range(1 + myLuck, 15 + myLuck);
                    DataStorage.MGAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 5://magnum bullets
                    awardedAmmo = Random.Range(1 + myLuck, 2 + myLuck);
                    DataStorage.magnumAmmo += awardedAmmo;
                    //add weight here
                    break;
                case 6://explosive rounds
                    awardedAmmo = Random.Range(1 + myLuck, myLuck);
                    DataStorage.explosiveAmmo += awardedAmmo;
                    //add weight here
                    break;
            }
        }

        xpValue += xp;
        xpText.text = xp.ToString();
        victory.SetActive(true);
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP + xpValue / (float)DataStorage.maxXP, 1, 1);
        StartCoroutine(Awards(ammoName, ammoType, awardedAmmo, awardedItem, awardedXP, awardedMoney)); 
        StartCoroutine(LoadXP(.02f));
    }

    IEnumerator LoadXP(float waitTime)
    {
        score.SetActive(true);
        victory.GetComponent<Animator>().Play("Score");
        xpBar.transform.localScale = new Vector3((float)DataStorage.XP / (float)DataStorage.maxXP, 1, 1);
        LevelUp.SetActive(false);
        yield return new WaitForSeconds(6f);

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
        //score.SetActive(true);
        levelingUp.Stop();
        //victory.GetComponent<Animator>().Play("Score");
        yield return new WaitForSeconds(4f);
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
        bonusAwards.SetActive(false);
        LevelUp.SetActive(false);
        pressStart.SetActive(false);
        gameObject.SetActive(false);
        score.SetActive(false);
        victory.SetActive(false);
        DataStorage.battleSystem.SetActive(false);
        DataStorage.UpdateHUD();
    }
}
