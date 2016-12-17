﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatScript : MonoBehaviour
{
    public AudioSource[] gunShots;
    float fireRate;
    bool mgFire = false;
    public static bool isReloading;
    [Tooltip("These are the reload sounds for the weapons")]
    public AudioSource[] reloadSounds;
    [Tooltip("The sound that plays when a gun is loaded")]
    public AudioSource[] loadedSounds;
    public AudioSource healing;
    public AudioSource healed;
    public AudioSource cycleItems;
    bool canUse = true; //the boolean that allows players to use items
    int addHealth = 0;
    //enemy hp and enemy targets
    float[] enemyHP = {35,22,18 };
    float[] enemyMaxHP = { 35, 24, 30 };
    public GameObject[] enemyTarget;
    public GameObject[] enemyHealthBar;
    public GameObject[] enemyBar;
    public GameObject CBTprefab;
    [SerializeField]
    GameObject[] myText;
    float totalDamage; //used to tally up all machine damage
    int lastHit;
    [SerializeField]
    GameObject[] blood1;
    [SerializeField]
    GameObject[] blood2;
    [SerializeField]
    GameObject[] blood3;
    public int[] enemyFortitude;
    public int[] xp;//the amount of xp per enemy
    float dmg = 0f;
    public GameObject Backgrounds;
    public GameObject reloadBar;
    public GameObject reloadText;


    public int myXP; //the amount of xp that is gained after a battle is won.
        //this variable, after the battle is over, will get converted into xp.


    void OnEnable()
    {
       //DataStorage.player.GetComponent<Controls>().enabled = false;
        for (int i = 0; i < enemyMaxHP.Length; i++)
        {
            xp[i] = 50;
            enemyHP[i] = enemyMaxHP[i];
            enemyTarget[i].transform.localScale = new Vector3(enemyTarget[i].transform.localScale.x + DataStorage.accuracy[DataStorage.curWeapon],1,1);
        }
        reloadBar.SetActive(true);
        reloadText.SetActive(true);
        gameObject.GetComponent<CombatScript>().enabled = true;
        gameObject.GetComponent<Animator>().Play("Enabled");

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        //firing the gun
        if (Input.GetKeyDown(KeyCode.Mouse0) && DataStorage.weaponType[DataStorage.curWeapon] != "Automatic" && DataStorage.itemBar.GetComponent<Image>().fillAmount == 0)
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                if (Time.time > fireRate)
                {
                    fireRate = Time.time + DataStorage.fireRate[DataStorage.curWeapon];
                    Shooting();
                    if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                         Damage();
                }
            }
            else
                gunShots[0].Play();

        if (Input.GetKey(KeyCode.Mouse0) && DataStorage.weaponType[DataStorage.curWeapon] == "Automatic" && DataStorage.itemBar.GetComponent<Image>().fillAmount == 0)
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                if (Time.time > fireRate)
                {
                    fireRate = Time.time + DataStorage.fireRate[DataStorage.curWeapon];
                    Shooting();
                    if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                        Damage();
                }
            }
            else
            {
                if (Time.time > fireRate)
                {
                    gunShots[0].Play();
                    fireRate = Time.time + .12f;
                    if (totalDamage > 0)
                    {
                        InitCBT(0, lastHit, "-" + totalDamage.ToString());
                    }
                }
            }
        if (Input.GetKeyUp(KeyCode.Mouse0) && totalDamage > 0)
        {
            InitCBT(0, lastHit, "-" + totalDamage.ToString());
        }
        //Reloading your weapon
        if (Input.GetKeyDown("r") && DataStorage.holster[DataStorage.curWeapon] < DataStorage.capacity[DataStorage.curWeapon] && !isReloading && DataStorage.itemBar.GetComponent<Image>().fillAmount <= 0)
            Reload();

        //No Ammo
        if (DataStorage.holster[DataStorage.curWeapon] <= 0 && !isReloading)
        {
            DataStorage.reloadingText.SetActive(true);
            if (!DataStorage.reloadingText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            {
                DataStorage.reloadingText.GetComponent<Animator>().Play("Reload", -1, 0f);
                DataStorage.reloadingText.GetComponent<Text>().text = "Reload";
            }
        }

        //using hotkeys for items
        if ((Input.GetMouseButton(1) || Input.GetKey("space")) && DataStorage.itemBar.GetComponent<Image>().fillAmount < 1 && canUse == true && int.Parse(DataStorage.itemCount.text) > 0 && !CombatScript.isReloading)
        {
            UseItems();
        }
        else if (DataStorage.itemBar.GetComponent<Image>().fillAmount > 0 && !Input.GetMouseButton(1))
        {
            healing.Stop();
            DataStorage.itemBar.GetComponent<Image>().fillAmount -= Time.deltaTime * 1;
        }

        //switcing Items
        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && DataStorage.itemBar.GetComponent<Image>().fillAmount == 0)
        {
            SwitchItemsLeft();
        }

        if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && DataStorage.itemBar.GetComponent<Image>().fillAmount == 0)
        {
            SwitchItemsRight();
        }

    }//end of update

    public void Shooting()
    {
        gunShots[1].Play();//checking to see which weapon was fired
        switch (DataStorage.weaponName[DataStorage.curWeapon])
        {
            default:
                gunShots[2].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Oppressor":
                gunShots[3].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "The Blacklist":
                gunShots[4].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Trident":
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(TrippleShot(.1f));
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Silencer":
                gunShots[6].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Seeker":
                gunShots[5].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Hunter Killer":
                gunShots[7].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Crow's Nest":
                gunShots[8].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "12 Gauge":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Orthrus":
                gunShots[9].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Cerberus":
                gunShots[10].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Devestator":
                gunShots[11].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Savage One":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Diminisher":
                gunShots[16].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                if (mgFire == false)
                {
                    DataStorage.combat.GetComponent<Animator>().speed = 0f;
                    StartCoroutine(StopWatch(.2f));
                }
                else
                {
                    StartCoroutine(SlowMo(1f));
                    DataStorage.combat.GetComponent<Animator>().speed = .1f;
                }
                DataStorage.shotsFired++;
                break;
            case "Revolver":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Scylla":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Day Ender":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Redeemer":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Hellfire":
                gunShots[2].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                if (mgFire == false)
                {
                    DataStorage.combat.GetComponent<Animator>().speed = 0f;
                    StartCoroutine(StopWatch(.2f));
                }
                else
                {
                    StartCoroutine(SlowMo(1f));
                    DataStorage.combat.GetComponent<Animator>().speed = .08f;
                }
                DataStorage.shotsFired++;
                break;
            case "Energy Rifle":
                gunShots[14].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Eradicator":
                gunShots[15].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
        }
    }

    IEnumerator TrippleShot(float waitTime)//this function is for automatics only
    {
        for (int i = 0; i < 3; i++)
        {
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                gunShots[5].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.shotsFired++;
                yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
            }
            else
                i = 3;
        }
        if (Time.time > fireRate || DataStorage.holster[DataStorage.curWeapon] < 1)
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
    }

    IEnumerator SlowMo(float waitTime)//this function is for automatics only
    {
        yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
        if (Time.time > fireRate || DataStorage.holster[DataStorage.curWeapon] < 1)
        {
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
            mgFire = false;
        }
    }

    IEnumerator StopWatch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (DataStorage.weaponType[DataStorage.curWeapon] != "Automatic")
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
        else
        {
            {

                mgFire = true;
                StartCoroutine (SlowMo(DataStorage.fireRate[DataStorage.curWeapon]));
            }
        }
    }

    //start reloading
    void Reload()
    {
        switch (DataStorage.weaponType[DataStorage.curWeapon])
        {
            case "Handgun":
                if (DataStorage.HGAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.HGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[0].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else break;
                break;
            case "Shotgun":
                if (DataStorage.SGAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.SGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[1].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else break;
                break;
            case "Automatic":
                if (DataStorage.MGAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.MGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[2].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else break;
                break;
            case "Rifle":
                if (DataStorage.rifleAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.rifleAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[3].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else
                    break;
                break;
            case "Magnum":
                if (DataStorage.magnumAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.magnumAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[3].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else break;
                break;
            case "Explosive":
                if (DataStorage.magnumAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.magnumAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    reloadSounds[4].Play();
                    DataStorage.UpdateHolster();
                    StartCoroutine(Reload(0f));
                }
                else break;
                break;
        }
    }
    //reloading the weapon
    IEnumerator Reload(float reloadTime)
    {//setting the reload text so that it animates
        DataStorage.reloadingText.SetActive(true);
        DataStorage.reloadingText.GetComponent<Animator>().Play("Reloading", -1, 0f);
        DataStorage.reloadingText.GetComponent<Text>().text = "Reloading";
        //setting the reload pi bar so that it animates
        DataStorage.reloadPiBar.SetActive(true);
        DataStorage.reloadPiBar.GetComponent<Animator>().Play("ReloadBarEnabled", -1, 0f);
        DataStorage.reloadBar.GetComponent<Image>().fillAmount = 0f;
        int temp = 0;
        while (DataStorage.reloadBar.GetComponent<Image>().fillAmount < 1)
        {
            yield return new WaitForSeconds(DataStorage.reload[DataStorage.curWeapon] / 8);
            temp++;
            if (DataStorage.weaponType[DataStorage.curWeapon] == "Shotgun")
            {
                if (temp == 2 || temp == 4 || temp == 6 || temp == 8)
                    reloadSounds[0].Play();//playing reload sounds for shotgun

                DataStorage.reloadBar.GetComponent<Image>().fillAmount = reloadTime / DataStorage.reload[DataStorage.curWeapon];
                reloadTime += DataStorage.reload[DataStorage.curWeapon] / 8;
            }
            else if (DataStorage.weaponType[DataStorage.curWeapon] == "Magnum")
            {
                if (temp == 2 || temp == 4 || temp == 6 || temp == 8)
                    reloadSounds[0].Play();//playing reload sounds for magnum
                DataStorage.reloadBar.GetComponent<Image>().fillAmount = reloadTime / DataStorage.reload[DataStorage.curWeapon];
                reloadTime += DataStorage.reload[DataStorage.curWeapon] / 8;
            }
            else
            {
                DataStorage.reloadBar.GetComponent<Image>().fillAmount = reloadTime / DataStorage.reload[DataStorage.curWeapon];
                reloadTime += DataStorage.reload[DataStorage.curWeapon] / 8;
            }


        }

        //checking to see which gun wa equipped before reloading
        switch (DataStorage.weaponType[DataStorage.curWeapon])
        {
            case "Handgun":
                if (DataStorage.HGAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.HGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.HGAmmo;
                    DataStorage.HGAmmo = 0;
                }
                loadedSounds[0].Play();
                break;
            case "Shotgun":
                if (DataStorage.SGAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.SGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.SGAmmo;
                    DataStorage.SGAmmo = 0;
                }
                loadedSounds[1].Play();
                break;
            case "Automatic":
                if (DataStorage.MGAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.MGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.MGAmmo;
                    DataStorage.MGAmmo = 0;
                }
                loadedSounds[2].Play();
                break;
            case "Rifle":
                if (DataStorage.rifleAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.rifleAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.rifleAmmo;
                    DataStorage.rifleAmmo = 0;
                }
                loadedSounds[3].Play();
                break;
            case "Magnum":
                if (DataStorage.magnumAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.magnumAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.magnumAmmo;
                    DataStorage.magnumAmmo = 0;
                }
                loadedSounds[4].Play();
                break;
            case "Explosive":
                if (DataStorage.explosiveAmmo > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.explosiveAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.explosiveAmmo;
                    DataStorage.explosiveAmmo = 0;
                }
                loadedSounds[5].Play();
                break;
        }
        DataStorage.reloadingText.GetComponent<Animator>().Play("Reloaded", -1, 0f);
        DataStorage.reloadingText.GetComponent<Text>().text = "Reloaded";
        //    DataStorage.reloadBar.GetComponent<Image>().fillAmount = 1f;
        DataStorage.reloadPiBar.GetComponent<Animator>().Play("ReloadBar", -1, 0f);
        isReloading = false;
        DataStorage.UpdateHolster();
    }
    //switch items left
    void SwitchItemsLeft()
    {
        cycleItems.Play();
        if (DataStorage.equippedItem > 1)
        {
            DataStorage.equippedItem -= 1;
        }
        else
        {
            DataStorage.equippedItem = 3;
        }
        //checking if the item matches the current equipped item
        switch (DataStorage.equippedItem)
        {
            default:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_small");
                break;
            case 2:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_medium");
                break;
            case 3:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_large");
                break;
        }
        DataStorage.UpdateHUD();//refreshing the HUD
    }
    //switch items right
    void SwitchItemsRight()
    {
        cycleItems.Play();
        if (DataStorage.equippedItem < 3)
        {
            DataStorage.equippedItem += 1;
        }
        else
        {
            DataStorage.equippedItem = 1;
        }
        //checking if the item matches the current equipped item
        switch (DataStorage.equippedItem)
        {
            default:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_small");
                break;
            case 2:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_medium");
                break;
            case 3:
                DataStorage.HUDItemImage.sprite = Resources.Load<Sprite>("Art/GUI/HUD/first_aid_kit_large");
                break;
        }
        DataStorage.UpdateHUD();//refreshing the HUD
    }

    IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canUse = true;
    }

    //using Items
    void UseItems()
    {
        if (!healing.isPlaying)
            healing.Play();
        DataStorage.itemBar.GetComponent<Image>().fillAmount += Time.deltaTime * (DataStorage.speed * .08f);
        if (DataStorage.itemBar.GetComponent<Image>().fillAmount >= 1)
        {
            DataStorage.CBTHealth.GetComponent<Animator>().Play("IncrementHealth", -1, 0f);
            switch (DataStorage.equippedItem)
            {
                case 1:
                    DataStorage.CBTHealth.GetComponent<Text>().text = "+ " + (addHealth += 20 + (DataStorage.intelligence / 2)).ToString();
                    DataStorage.itemSmallAid -= 1;
                    addHealth += 20 + (DataStorage.intelligence / 2);
                    StartCoroutine(AddToHealth(.02f));
                    break;
                case 2:
                    DataStorage.CBTHealth.GetComponent<Text>().text = "+ " + (addHealth += 40 + (DataStorage.intelligence / 2)).ToString();
                    DataStorage.itemMedAid -= 1;
                    addHealth += 40 + (DataStorage.intelligence / 2);
                    StartCoroutine(AddToHealth(.02f));
                    break;
                case 3:
                    DataStorage.CBTHealth.GetComponent<Text>().text = "+ " + (addHealth += 80 + (DataStorage.intelligence / 2)).ToString();
                    DataStorage.itemLargeAid -= 1;
                    addHealth += 80 + (DataStorage.intelligence / 2);
                    StartCoroutine(AddToHealth(.02f));
                    break;
            }
            healing.Stop();
            DataStorage.itemBar.GetComponent<Image>().fillAmount = 0;
            DataStorage.greenFlash.Play("HealAnimation", -1, 0f);
            healed.Play();
            canUse = false;
            DataStorage.UpdateHUD();//refreshing the HUD
            StartCoroutine(WaitTime(1.5f));
        }
        //use item
        //subtract value from item
    }
    //adds health at a slow rate
    IEnumerator AddToHealth(float add)
    {
        while (addHealth > 0)
        {
            addHealth -= 1;
            DataStorage.health++;
            if ((DataStorage.health > DataStorage.maxHealth))
            {
                DataStorage.health = DataStorage.maxHealth;
                addHealth = 0;
            }
            DataStorage.UpdateHUDHealth();//refreshing the HUD
            yield return new WaitForSeconds(add);
        }
    }
    //checking to see if an enemy was hit, if so, deal te appriopriate damage
    public void Damage()
    {
        //getting the critical chances
        float crit = Mathf.Round(Random.Range(0.0f, 1)*100)/100;
        dmg = 0f;
        float temp;

        for (int i = 0; i < enemyTarget.Length; i++)
        {
            if (enemyTarget[i].activeSelf)
            {
                if (DataStorage.crosshair.transform.position.x >= enemyTarget[i].transform.position.x - (enemyTarget[i].transform.localScale.x / 4) && DataStorage.crosshair.transform.position.x <= enemyTarget[i].transform.position.x + (enemyTarget[i].transform.localScale.x/4))
                {
                    //calling the blood
                    StartCoroutine(Blood(i, 1f));
                    //finding the accuracy
                    if (DataStorage.crosshair.transform.position.x <= enemyTarget[i].transform.position.x)
                        temp = DataStorage.crosshair.transform.position.x / enemyTarget[i].transform.position.x;
                    else
                        temp = enemyTarget[i].transform.position.x/DataStorage.crosshair.transform.position.x;


                    if (enemyHP[i] > 0)
                    {
                        if ((DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.damage) * temp < enemyHP[i])
                        {
                            dmg = (Mathf.Round((DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.damage) * temp) * 100) / 100;
                            if (crit <= DataStorage.criticalChance[DataStorage.curWeapon])
                                dmg *= 2;
                            //adding to the total amount of damage the player has dalt over a lifetime
                            DataStorage.damageDealt += dmg;
                            lastHit = i;
                        }
                        else
                        {
                            dmg = (Mathf.Round(((DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.damage) * temp) - enemyHP[i]) * 100) / 100;
                            //adding to the total amount of damage the player has dalt over a lifetime
                            if (crit <= DataStorage.criticalChance[DataStorage.curWeapon])
                                DataStorage.damageDealt += dmg;
                            lastHit = i;
                        }
                    }
 
                    //adding the damage
                    enemyHP[i] -= (DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.damage)*temp;
                    //calculating the current health bar for the enemy
                    enemyHealthBar[i].transform.localScale = new Vector3 (enemyHP[i] / enemyMaxHP[i],1,1);
                    //initiating the amount of damage to a foating text
                    if (DataStorage.weaponType[DataStorage.curWeapon] != "Automatic")
                    InitCBT(crit, i, "-" + dmg.ToString());
                    else
                    totalDamage += dmg;

                    DataStorage.targetsHit ++;
                    if (enemyHP[lastHit] <= 0 && xp[lastHit] > 0)
                    {
                        StartCoroutine(WaitAndDisable(i, 2f));
                        DataStorage.enemiesKilled++;
                        //adding xp
                        myXP += xp[lastHit];
                        xp[lastHit] = 0;

                        //checking to see if all enemies are dead
                        if (enemyHP[0] <= 0 && enemyHP[1] <= 0 && enemyHP[2] <= 2)
                        {
                            gameObject.GetComponent<CombatScript>().enabled = false;
                            reloadBar.SetActive(false);
                            reloadText.SetActive(false);
                            StartCoroutine(GoToVictory(4f));
                        }
                    }
                  //preventing any weapon other than the shotgun from doing damage to multiple enemies
                    if (DataStorage.weaponType[DataStorage.curWeapon] != "Shotgun")
                        i = enemyMaxHP.Length;
                }
                //else
                   // 
            }
        }
    }
    //disable gameobjects after 2 seconds, so that the text can be seen.
    //otherwise, the game objects get disabled, and the text is never seen
    IEnumerator WaitAndDisable(int number, float disable)
    {
        yield return new WaitForSeconds(disable);
        enemyBar[number].SetActive(false);
        enemyTarget[number].SetActive(false);
    }

    //damage to  floating text
    GameObject InitCBT(float crit, int number, string text)
    {
        GameObject temp = Instantiate(CBTprefab) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(myText[number].transform);
        tempRect.transform.localPosition = CBTprefab.transform.transform.localPosition;
        tempRect.transform.localScale = CBTprefab.transform.localScale;
        tempRect.transform.localRotation = CBTprefab.transform.localRotation;
        if (totalDamage <= 0)
            if (crit > DataStorage.criticalChance[DataStorage.curWeapon])
            {
                temp.GetComponent<Animator>().Play("CBTDamage", -1, 0f);
            }
            else
            {
                temp.GetComponent<Animator>().Play("CBTDamageCrit", -1, 0f);
            }
        else
        {
            temp.GetComponent<Animator>().Play("CBTDamageTotal", -1, 0f);
            StartCoroutine(AddDamage(temp, totalDamage, .05f));
            totalDamage = 0;
            return temp;
        }
        temp.GetComponent<Text>().text = text;
        Destroy(temp.gameObject, 6);
        return temp;
    }
    //animating the machinegun text
    IEnumerator AddDamage(GameObject temp, float total, float waitTime)
    {
        for (int i = 0; i < total; i++)
        {
            yield return new WaitForSeconds(waitTime);
            temp.GetComponent<Text>().text = "-" + i;
        }
        temp.GetComponent<Animator>().SetTrigger("end");
        Destroy(temp.gameObject, 10f);
    }
    //randomizing the area of blood spatter depending upon which enemy was struck
    IEnumerator Blood(int i, float waitTime)
    {
        int temp = Random.Range(0, blood1.Length);
        switch (i)
        {
            default:
                blood1[temp].SetActive(true);
                yield return new WaitForSeconds(waitTime);
                blood1[temp].SetActive(false);
                break;
            case 1:
                blood2[temp].SetActive(true);
                yield return new WaitForSeconds(waitTime);
                blood2[temp].SetActive(false);
                break;
            case 2:
                blood3[temp].SetActive(true);
                yield return new WaitForSeconds(waitTime);
                blood3[temp].SetActive(false);
                break;
        }
    }
    //waiting before the victory occurs
    IEnumerator GoToVictory(float waitTime)
    {
    yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Animator>().Play("Disabled");
        yield return new WaitForSeconds(waitTime - 1f);
        GetComponent<VictoryScript>().VictoryScene(myXP);
    myXP = 0;
    }
}//end of class
