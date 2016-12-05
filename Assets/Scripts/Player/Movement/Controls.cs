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
    public AudioSource HGReload;
    public AudioSource HGLoaded;
    public AudioSource SGReload;
    public AudioSource SGLoaded;
    public AudioSource MGReload;
    public AudioSource MGLoaded;
    public AudioSource rifleReload;
    public AudioSource rifleLoaded;
    public AudioSource magnumReload;
    public AudioSource magnumLoaded;
    public AudioSource explosiveReload;
    public AudioSource explosiveLoaded;
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
             switch (DataStorage.weaponType[DataStorage.curWeapon])
            {
                case "Handgun":
                    if (DataStorage.HGAmmo > 1)
                    {
                        isReloading = true;
                        DataStorage.HGAmmo += DataStorage.holster[DataStorage.curWeapon];
                        DataStorage.holster[DataStorage.curWeapon] = 0;
                        HGReload.Play();
                        DataStorage.UpdateHolster();
                        StartCoroutine(Reload(0f));
                    }
                    else break;
                    DataStorage.reloadingText.SetActive(true);
                    DataStorage.reloadingText.GetComponent<Animator>().Play("Reloading", -1, 0f);
                    DataStorage.reloadingText.GetComponent<Text>().text = "Reloading";
                    break; 
            }
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
    {
        DataStorage.reloadPiBar.SetActive(true);
        DataStorage.reloadPiBar.GetComponent<Animator>().Play("ReloadBarEnabled", -1, 0f);
        DataStorage.reloadBar.GetComponent<Image>().fillAmount = 0f;

        while (DataStorage.reloadBar.GetComponent<Image>().fillAmount < 1)
        {
            yield return new WaitForSeconds(DataStorage.reload[DataStorage.curWeapon]/8);
            DataStorage.reloadBar.GetComponent<Image>().fillAmount = reloadTime / DataStorage.reload[DataStorage.curWeapon];
            reloadTime += DataStorage.reload[DataStorage.curWeapon] / 8;
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
                HGLoaded.Play();
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