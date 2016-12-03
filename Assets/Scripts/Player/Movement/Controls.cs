﻿using UnityEngine;
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
    bool canUse = true; //the boolean that allows players to use items
    int addHealth = 0;

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
        if ((Input.GetMouseButton(1) || Input.GetKey("space")) && DataStorage.itemBar.GetComponent<Image>().fillAmount < 1 && canUse == true && int.Parse(DataStorage.itemCount.text) > 0)
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
            switch (DataStorage.equippedItem)
            {
                case 1:
                    DataStorage.itemSmallAid -= 1;
                    addHealth += 20 +(DataStorage.intelligence/2);
                    StartCoroutine(AddToHealth(.00005f));
                    break;
                case 2:
                    DataStorage.itemMedAid -= 1;
                    addHealth += 40 + (DataStorage.intelligence / 2);
                    StartCoroutine(AddToHealth(.00005f));
                    break;
                case 3:
                    DataStorage.itemLargeAid -=1;
                    addHealth += 80 + (DataStorage.intelligence / 2);
                    StartCoroutine(AddToHealth(.00005f));
                    break;
            }
            if (DataStorage.health > DataStorage.maxHealth)
                DataStorage.health = DataStorage.maxHealth;
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
            DataStorage.UpdateHUDHealth();//refreshing the HUD
            yield return new WaitForSeconds(add);
        }

    }

}//end of class