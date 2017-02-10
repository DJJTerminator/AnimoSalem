using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatScript : MonoBehaviour
{
    public AudioSource[] gunShots;
    public static float fireRate;
    bool mgFire = false;
    public static bool isReloading;
    [Tooltip("These are the reload sounds for the weapons")]
    public AudioSource[] reloadSounds;
    [Tooltip("The sound that plays when a gun is loaded")]
    public AudioSource[] loadedSounds;
    AudioSource healing;
    AudioSource healed;
    AudioSource cycleItems;
    bool canUse = true; //the boolean that allows players to use items
    int addHealth = 0;
    //enemy hp and enemy targets
    float[] enemyHP = { 35,22,18 };
    float[] enemyMaxHP = { 105, 75, 100 };
    public GameObject[] enemyTarget;//this is used for accuracy
    public GameObject[] enemyTarget2;//these are only used for colors
    public GameObject[] enemyTarget3;//these are only used for colors
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
    public int[] xp;//the amount of xp per enemy
    float dmg = 0f;
    public GameObject Backgrounds;
    public GameObject reloadBar;
    public GameObject reloadText;
	public GameObject textXP;//this is used for the text that displays the amount of xp gained
    bool isSlow; //this checks to see if the function, slowMo, is running.
    int shotsHit; //this is used to determine how many of the bullets actually hit the enemy (with the trident)
    float battleLength;
    static public float battleTime;//length of the battle
    static public float allottedTime; //the time allotted to ebat the battle for the hgihest grade
    static public int acHit; //total shots hit for this round
    static public int acFired; //total shots fired for this round
    static public int dodgeFail; //total failed dodges for this battle
    static public float damageGiven;
    static public float damageRecieved;
    static public int xpGained;



    public int myXP; //the amount of xp that is gained after a battle is won.
                     //this variable, after the battle is over, will get converted into xp.

    void OnEnable()
    {
        xpGained = 0;
        damageGiven = DataStorage.damageDealt;
        damageRecieved = DataStorage.damageTaken;
        acFired = DataStorage.shotsFired;
        acHit = DataStorage.targetsHit;
        battleTime = Time.time;
        allottedTime = 15;
	//getting the camera and turning off the follow script
        Camera myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        myCamera.GetComponent<CameraFollow>().enabled = false;
     
        //inceasing the alpha color of the target images for each that that is active in battle
        for (int i = 0; i < enemyTarget.Length; i++)
        {
            Color c = enemyTarget[i].GetComponent<Image>().color;
            c.a = .2f;
            enemyTarget[i].GetComponent<Image>().color = c;
            enemyTarget2[i].GetComponent<Image>().color = c;
            enemyTarget3[i].GetComponent<Image>().color = c;
        }
        //DataStorage.player.GetComponent<Controls>().enabled = false;
        for (int i = 0; i < enemyMaxHP.Length; i++)
        {
            xp[i] = 50;
            enemyHP[i] = enemyMaxHP[i];
			enemyTarget[i].transform.localScale = new Vector3(1,1,1);
			enemyTarget[i].transform.localScale = new Vector3(enemyTarget[i].transform.localScale.x + DataStorage.accuracy[DataStorage.curWeapon],1,1);
            xpGained += xp[i];
        }
					//finding any remaining inactive gameobjects
		if (DataStorage.combat != null)
		{
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
			DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
			DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = false;
		}
		else
		{
			DataStorage.gameManager = GameObject.Find ("GameManager");
			DataStorage.pauseMenus = GameObject.Find ("All Canvases/Canvas/PauseMenus");
			DataStorage.crosshair = GameObject.Find("All Canvases/BattleSystem/Combat/HandleSlideArea/Crosshair");
			DataStorage.combat = GameObject.Find("All Canvases/BattleSystem/Combat");
			DataStorage.battleSystem = GameObject.Find("All Canvases/BattleSystem");
			DataStorage.reloadingText = GameObject.Find("All Canvases/BattleSystem/ReloadingText");
			DataStorage.reloadBar = GameObject.Find("All Canvases/BattleSystem/ReloadBar/Image");
			DataStorage.reloadPiBar = GameObject.Find("All Canvases/BattleSystem/ReloadBar");
			DataStorage.itemBar =  GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/LoadingBar");
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
			DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
			DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = false;
		}
        reloadBar.SetActive(true);
        reloadText.SetActive(true);
        gameObject.GetComponent<CombatScript>().enabled = true;
        gameObject.GetComponent<Animator>().Play("Enabled");
    }
    void Start()
    {
	    healing = GameObject.Find ("GameManager/Sounds/ItemsHUD/Healing").GetComponent<AudioSource>();
		healed = GameObject.Find ("GameManager/Sounds/ItemsHUD/Healed").GetComponent<AudioSource>();
	    cycleItems = GameObject.Find ("GameManager/Sounds/ItemsHUD/CycleItems").GetComponent<AudioSource>();
		DataStorage.battleSystem.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //firing the gun
        if (Input.GetKeyDown(KeyCode.Mouse0) && DataStorage.weaponType[DataStorage.curWeapon] != "Automatic" && DataStorage.itemBar.GetComponent<Image>().fillAmount == 0)
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                if (Time.time > fireRate)
                {
                    fireRate = Time.time + DataStorage.fireRate[DataStorage.curWeapon];
                    Shooting();
					//waiting until the color reverts back to normal
					StartCoroutine (RevertTargetColor(fireRate - Time.time));
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
					//waiting until the color reverts back to normal
					StartCoroutine (RevertTargetColor(fireRate - Time.time));
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

    }//end of fixed update

    public void Shooting()
    {
        gunShots[1].Play();//checking to see which weapon was fired
        switch (DataStorage.weaponName[DataStorage.curWeapon])
        {
            default:
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.HGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[2].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++; 
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Oppressor":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.HGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[3].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++; 
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "The Blacklist":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.HGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[4].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Trident":
                StartCoroutine(TrippleShot(.1f));
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                if (DataStorage.holster[DataStorage.curWeapon] > 3)
                    StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                else
                    StartCoroutine(StopWatch(2f));
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Silencer":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.rifleAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[6].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                if (DataStorage.holster[DataStorage.curWeapon] > 2)
                    StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                else
                    StartCoroutine(StopWatch(1f));
                DataStorage.shotsFired++; 
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Seeker":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.rifleAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[5].Play();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));  
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Hunter Killer":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.rifleAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[7].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2)); 
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Crow's Nest":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.rifleAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[8].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "12 Gauge":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                    {
                        NeedToRelad();
                        print(DataStorage.holster[DataStorage.curWeapon]);
                    }
                    else
                    {
                        NoAmmo();
                        print(DataStorage.holster[DataStorage.curWeapon]);
                    }
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[12].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Orthrus":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[9].Play();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Cerberus":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[10].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Devestator":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[11].Play();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Savage One":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[12].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Diminisher":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.MGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[16].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                if (!mgFire)
                {
                    if (!isSlow)
                    {
                        DataStorage.combat.GetComponent<Animator>().speed = 0f;
                        StartCoroutine(StopWatch(.5f));
                        isSlow = true;
                    }
                }
                else
                {
					if (DataStorage.holster[DataStorage.curWeapon] > 1)
					{
                        if (!isSlow)
                        {
                            DataStorage.combat.GetComponent<Animator>().speed = .1f;
                            StartCoroutine(SlowMo(1f));
                        }
					}
					else
					{
                        if (!isSlow)
                        {
                            DataStorage.combat.GetComponent<Animator>().speed = 0f;
                            StartCoroutine(SlowMo(1f));
                        }
					}
                }
                DataStorage.shotsFired++;  
                break;
            case "Revolver":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.magnumAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[13].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Scylla":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.magnumAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[13].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Day Ender":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.MGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[13].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;  
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Redeemer":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.SGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[12].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
            case "Hellfire":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.MGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[2].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                if (!mgFire)
                {
                    DataStorage.combat.GetComponent<Animator>().speed = 0f;
                    StartCoroutine(StopWatch(.5f));
                }
                else
                {
					if (DataStorage.holster[DataStorage.curWeapon] > 1)
					{
						DataStorage.combat.GetComponent<Animator>().speed = .1f;
						StartCoroutine(SlowMo(1f));
					}
					else
					{
						DataStorage.combat.GetComponent<Animator>().speed = 0f;
						StartCoroutine(SlowMo(1f));
					}
				}
                DataStorage.shotsFired++;
                break;
            case "Energy Rifle":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.rifleAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[14].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("ShootingSmall", -1, 0f);
                break;
            case "Eradicator":
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.explosiveAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                //checking to see if any enemies are still alive
                if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
                {
                    Damage();
                }
                gunShots[15].Play();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                DataStorage.shotsFired++;
                Backgrounds.GetComponent<Animator>().Play("Shooting", -1, 0f);
                break;
        }
    }

    IEnumerator TrippleShot(float waitTime)//this function is for the trident only
    {
        for (int i = 0; i < 3; i++)
        {
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                gunShots[5].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                shotsHit++;
                DataStorage.shotsFired++;
                //checking to see how much ammo is left over that last shot that was fired
                if (DataStorage.holster[DataStorage.curWeapon] == 0)
                    if (DataStorage.HGAmmo > 0)
                        NeedToRelad();
                    else
                        NoAmmo();
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
            }
            else
            {
                gunShots[0].Play();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
            }
        }
        //checking to see if any enemies are still alive
        if (enemyHP[0] > 0 || enemyHP[1] > 0 || enemyHP[2] > 0)
            Damage();
    }

    IEnumerator SlowMo(float waitTime)
    {//this function is for automatics only
        isSlow = true;
        yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
        if (!Input.GetKey(KeyCode.Mouse0) || DataStorage.holster[DataStorage.curWeapon] == 0)
        {
            if (DataStorage.holster[DataStorage.curWeapon] == 0)
            {
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                yield return new WaitForSeconds(waitTime);
            }
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
            mgFire = false;
        }
        isSlow = false;

    }
	//this function prevents the target from moving
    IEnumerator StopWatch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (DataStorage.weaponType[DataStorage.curWeapon] != "Automatic")
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
        else
        {
            if (!mgFire)
            {
                StartCoroutine(SlowMo(DataStorage.fireRate[DataStorage.curWeapon]));
                if (!isSlow)
                {
                    DataStorage.combat.GetComponent<Animator>().speed = .1f;
                }
                mgFire = true;
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
                if (DataStorage.explosiveAmmo > 0)
                {
                    isReloading = true;
                    DataStorage.explosiveAmmo += DataStorage.holster[DataStorage.curWeapon];
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

        //checking to see which gun was equipped before reloading
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
            //DataStorage.reloadBar.GetComponent<Image>().fillAmount = 1f;
            DataStorage.reloadPiBar.GetComponent<Animator>().Play("ReloadBar", -1, 0f);
         isReloading = false;
         DataStorage.UpdateHolster();
    }

    public void ResetAmmo()
    {
        //setting the ammo after the battle is over, so the player doesn't have to do it during the beginning
        //of next round
        switch (DataStorage.weaponType[DataStorage.curWeapon])
        {
            case "Handgun":
                if (DataStorage.HGAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.HGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.HGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.HGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.HGAmmo;
                    DataStorage.HGAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
            case "Shotgun":
                if (DataStorage.SGAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.SGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.SGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.SGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.SGAmmo;
                    DataStorage.SGAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
            case "Automatic":
                if (DataStorage.MGAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.MGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.MGAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.MGAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.MGAmmo;
                    DataStorage.MGAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
            case "Rifle":
                if (DataStorage.rifleAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.rifleAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.rifleAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.rifleAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.rifleAmmo;
                    DataStorage.rifleAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
            case "Magnum":
                if (DataStorage.magnumAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.magnumAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.magnumAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.magnumAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.magnumAmmo;
                    DataStorage.magnumAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
            case "Explosive":
                if (DataStorage.explosiveAmmo + DataStorage.holster[DataStorage.curWeapon] > DataStorage.capacity[DataStorage.curWeapon])
                {
                    DataStorage.explosiveAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;
                    DataStorage.explosiveAmmo -= DataStorage.capacity[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.capacity[DataStorage.curWeapon];
                }
                else
                {
                    DataStorage.explosiveAmmo += DataStorage.holster[DataStorage.curWeapon];
                    DataStorage.holster[DataStorage.curWeapon] = 0;

                    DataStorage.holster[DataStorage.curWeapon] = DataStorage.explosiveAmmo;
                    DataStorage.explosiveAmmo = 0;
                }
                DataStorage.UpdateHolster();
                break;
        }
    }//end of resetting ammo

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
					DataStorage.itemsUsed +=1;
                    addHealth += 20 + (DataStorage.intelligence / 2);
					DataStorage.greenFlash.Play("HealAnimation", -1, 0f);
                    StartCoroutine(AddToHealth(.02f));
                    break;
                case 2:
                    DataStorage.CBTHealth.GetComponent<Text>().text = "+ " + (addHealth += 40 + (DataStorage.intelligence / 2)).ToString();
                    DataStorage.itemMedAid -= 1;
					DataStorage.itemsUsed +=1;
                    addHealth += 40 + (DataStorage.intelligence / 2);
					DataStorage.greenFlash.Play("HealAnimation", -1, 0f);
                    StartCoroutine(AddToHealth(.02f));
                    break;
                case 3:
                    DataStorage.CBTHealth.GetComponent<Text>().text = "+ " + (addHealth += 80 + (DataStorage.intelligence / 2)).ToString();
                    DataStorage.itemLargeAid -= 1;
					DataStorage.itemsUsed +=1;
                    addHealth += 80 + (DataStorage.intelligence / 2);
					DataStorage.greenFlash.Play("HealAnimation", -1, 0f);
                    StartCoroutine(AddToHealth(.02f));
                    break;
            }
            healing.Stop();
            DataStorage.itemBar.GetComponent<Image>().fillAmount = 0;
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
        dmg = 0f;
        float temp;
		int totalXP = 0;//this is used to tally up the xp in case of double kills

        for (int i = 0; i < enemyTarget.Length; i++)
        {
            //getting the critical chances
            float crit = Mathf.Round(Random.Range(0.0f, 1) * 100) / 100;

            if (enemyTarget[i].activeSelf)
            {
                if (DataStorage.crosshair.transform.position.x >= enemyTarget[i].transform.position.x - (enemyTarget[i].transform.localScale.x / 4) && DataStorage.crosshair.transform.position.x < enemyTarget[i].transform.position.x + (enemyTarget[i].transform.localScale.x/4))
                {
                    //calling the blood
                    StartCoroutine(Blood(i, 1f));
                    //finding the accuracy
                    if (DataStorage.crosshair.transform.position.x <= enemyTarget[i].transform.position.x)
                        temp = DataStorage.crosshair.transform.position.x / enemyTarget[i].transform.position.x;
                    else
                        temp = enemyTarget[i].transform.position.x/DataStorage.crosshair.transform.position.x;
                    dmg = (Mathf.Round(DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.damage) * 10) / 10;
                    temp = Mathf.Round(100 - (temp * 100));
                    temp += Mathf.Round(dmg / 10);

                    if (crit <= DataStorage.criticalChance[DataStorage.curWeapon])
                    {
                        dmg += (Mathf.Round(dmg * DataStorage.range[DataStorage.curWeapon]) * 10) / 10;
                        DataStorage.criticalHits++;
                    }
                    //amplifying the damage by the amount of bullets fired (if weapon that is used is the Trident tripple shot)
                    if (DataStorage.weaponName[DataStorage.curWeapon] == "Trident")
                    {
                        temp *= shotsHit;
                        dmg *= shotsHit;//***********************This needs to be fixed
                        //checking to see how many of the trident bullets actually hit the eenemy
                        //in otherwords, the enemy might have died on the second shot while the gun still fires three rounds
                        DataStorage.targetsHit += shotsHit;
                        shotsHit = 0;
                    }//counting the shots that were hit
                    else
                    {
                        DataStorage.targetsHit++;
                    }
                    dmg -= temp;

                    if (enemyHP[i] > dmg)
                    {
                        //adding to the total amount of damage the player has dealt over a lifetime
                        DataStorage.damageDealt += dmg;
                        lastHit = i;
                        //adding the damage
                        enemyHP[i] -= dmg;
                    }
                     else
                     {
                        dmg = enemyHP[i];
                        //adding to the total amount of damage the player has dealt over a lifetime
                        DataStorage.damageDealt += dmg;
                        //changing the color of the target to 0 alpha as there is no longer any reason to see the enemy target after the enemy
                        //is dead    
                        Color c = enemyTarget[i].GetComponent<Image>().color;    
                        c.a = 0;    
                        enemyTarget[i].GetComponent<Image>().color = c;
                        enemyTarget2[i].GetComponent<Image>().color = c;    
                        enemyTarget3[i].GetComponent<Image>().color = c;
                        lastHit = i;    
                        //setting hp to 0
                         enemyHP[i] = 0;
                     }
                    //calculating the current health bar for the enemy
                    enemyHealthBar[i].transform.localScale = new Vector3 (enemyHP[i] / enemyMaxHP[i],1,1);
                    //initiating the amount of damage to a foating text
                    if (DataStorage.weaponType[DataStorage.curWeapon] != "Automatic")
                        InitCBT(crit, i, "-" + dmg.ToString());
                    else
                        totalDamage += dmg;
                    if (enemyHP[i] <= 0 && xp[i] > 0)
                    {
                        StartCoroutine(WaitAndDisable(i, 2f));
                        DataStorage.enemiesKilled++;
                        //adding xp
                        myXP += xp[i] + (DataStorage.intelligence / 2);
                        totalXP += xp[i] + (DataStorage.intelligence / 2);
                        xp[i] = 0;

                        //checking to see if all enemies are dead
                        if (enemyHP[0] <= 0 && enemyHP[1] <= 0 && enemyHP[2] <= 0)
                        {
                            //changing the track for the music
                            //variables that are passed musicType, waitime, and a bool to check if the music can play
                            MusicScript.PrepareTrack(0, 0f, false);
                            //resetting the ammo, so the player doesnt have to reload at the beginning of the next round
                            ResetAmmo();
                            fireRate = Time.time + 1f;
                            if (totalDamage > 0)
                                InitCBT(0, lastHit, "-" + totalDamage.ToString());
                            reloadBar.SetActive(false);
                            reloadText.SetActive(false);
                            DataStorage.battlesWon += 1;
                            //assigning the battle time
                            battleLength = Time.time - battleLength;
                            if (battleLength > DataStorage.longestBattle)
                                DataStorage.longestBattle = battleLength;
                            if (battleLength < DataStorage.shortestBattle || DataStorage.shortestBattle == 0)
                                DataStorage.shortestBattle= battleLength;
                            StartCoroutine(GoToVictory(3f));
                        }
                    }
                  //preventing any weapon other than the shotgun from doing damage to multiple enemies
                    if (DataStorage.weaponType[DataStorage.curWeapon] != "Shotgun")
                        i = enemyMaxHP.Length;
                }
                //else
                   // 
            }
        }//end of forloop
		if (totalXP > 0)
		{
		textXP.GetComponent<Text>().text = "+ " + totalXP + "  XP";
		textXP.GetComponent<Animator>().Play("XPGain", -1, 0f);
		totalXP = 0;
		}
        shotsHit = 0;
    }//end of damage function
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
            StartCoroutine(AddDamage(temp, totalDamage, .02f));
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
        for (int i = 0; i <= total; i++)
        {
            //if (total - i > 1000)
            //    i += 1000;
            //else if (total - i > 500)
            //    i += 500;
            //else if (total - i > 100)
            //    i += 100;
            //else if (total - i > 50)
            //    i += 50;
            //else if (total - i > 10)
            //    i += 10;
            //else if (total - i > 5)
            //    i += 5;
            //else if (total - i > 4)
            //    i += 4;
            if (total - i > 3)
                i += 3;
            else if (total - i > 2)
                i += 2;

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
        //tallying up the score
        acFired = DataStorage.shotsFired - acFired;
        acHit = DataStorage.targetsHit - acHit;
        float ac = (Mathf.Round((((float)acHit / (float)acFired) * 1000f) / 10f));
        battleTime = Time.time - battleTime;
        damageGiven = DataStorage.damageDealt - damageGiven;
        damageRecieved = DataStorage.damageTaken - damageRecieved;
        DataStorage.itemBar.GetComponent<Image>().fillAmount -= Time.deltaTime * 1;
        healing.Stop();
        gameObject.GetComponent<CombatScript>().enabled = false;
        GetComponent<Animator>().Play("Disabled");
        GetComponent<Animator>().speed = 1f;
        yield return new WaitForSeconds(waitTime - 1f);
        GetComponent<VictoryScript>().VictoryScene(ac, myXP);
        myXP = 0;
    }

	//waiting to turn the color back to normal
	IEnumerator RevertTargetColor (float waitTime)
	{
	//turning the crosshair yellow
	DataStorage.crosshair.GetComponent<Image>().color = Color.yellow;
	yield return new WaitForSeconds (waitTime);
	//turning the crosshair red
	DataStorage.crosshair.GetComponent<Image>().color = Color.red;
	}
    //if the player's weapon is dry and holster is dry
    void NoAmmo()
    {
        //tell the player there no longer is any ammmo that remains
        DataStorage.reloadingText.GetComponent<Animator>().Play("Reload");
        DataStorage.reloadingText.GetComponent<Text>().text = "No Ammo";
    }
    //if player has ammo but none in holster
    void NeedToRelad()
    {
        //asking the player to reload
     DataStorage.reloadingText.GetComponent<Animator>().Play("Reload");
     DataStorage.reloadingText.GetComponent<Text>().text = "Reload";
    }
}//end of class