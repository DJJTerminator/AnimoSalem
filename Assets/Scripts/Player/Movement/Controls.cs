using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controls : MonoBehaviour {

    [Range(0, 10)]
    public float speed = 4f;
    RaycastHit hit;
    public LayerMask targetLayer;
    public Rigidbody myBody;
    public AudioSource healing;
    public AudioSource healed;
    public AudioSource cycleItems;
    [Tooltip("These are the reload sounds for the weapons")]
    public AudioSource[] reloadSounds;
    [Tooltip("The sound that plays when a gun is loaded")]
    public AudioSource[] loadedSounds;
    bool canUse = true; //the boolean that allows players to use items
    int addHealth = 0;
    bool isReloading;



    // Update is called once per frame
    void Update()
    {
        Vector3 up = -myBody.velocity;
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);

        if (Physics.Raycast(transform.position, up, targetLayer))
        {
            print("There is something in front of the object!");
        }
        Debug.DrawRay(transform.position, up, Color.white);

        //using hotkeys for items
        if ((Input.GetMouseButton(1) || Input.GetKey("space")) && DataStorage.itemBar.GetComponent<Image>().fillAmount < 1 && canUse == true && int.Parse(DataStorage.itemCount.text) > 0 && !isReloading)
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
        //Reloading your weapon
        if (Input.GetKeyDown("r") && DataStorage.holster[DataStorage.curWeapon] < DataStorage.capacity[DataStorage.curWeapon] && !isReloading && DataStorage.itemBar.GetComponent<Image>().fillAmount <= 0)
            Reload();
        {
      }

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

    }//end of update

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
                    addHealth += 20 +(DataStorage.intelligence/2);
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
                    DataStorage.itemLargeAid -=1;
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
            yield return new WaitForSeconds(DataStorage.reload[DataStorage.curWeapon]/8);
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

}//end of class