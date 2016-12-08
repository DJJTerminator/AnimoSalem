using UnityEngine;
using System.Collections;

public class CombatScript : MonoBehaviour {
    public AudioSource[] gunShots;
    float fireRate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && DataStorage.weaponType[DataStorage.curWeapon] != "Automatic")
             if (DataStorage.holster[DataStorage.curWeapon] > 0)
             {
                if (Time.time > fireRate)
                {
                    fireRate = Time.time + DataStorage.fireRate[DataStorage.curWeapon];
                    Shooting();
                }
            }
             else
                 gunShots[0].Play();

        if (Input.GetKey(KeyCode.Mouse0) && DataStorage.weaponType[DataStorage.curWeapon] == "Automatic")
            if (DataStorage.holster[DataStorage.curWeapon] > 0)
            {
                if (Time.time > fireRate)
                {
                    fireRate = Time.time + DataStorage.fireRate[DataStorage.curWeapon];
                    Shooting();
                }
            }
            else
            {
                if (Time.time > fireRate)
                {
                    gunShots[0].Play();
                    fireRate = Time.time + .12f;
                }
            }

    }





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
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon]/2));
                break;
            case "Oppressor":
                gunShots[3].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon]/2));
                break;
            case "The Blacklist":
                gunShots[4].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Trident":
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(TrippleShot(.1f));
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Silencer":
                gunShots[6].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Seeker":
                gunShots[5].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Hunter Killer":
                gunShots[7].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Crow's Nest":
                gunShots[8].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "12 Gauge":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Orthrus":
                gunShots[9].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Cerberus":
                gunShots[10].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Devestator":
                gunShots[11].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Savage One":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Diminisher":
                gunShots[2].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = .1f;
                StartCoroutine(SlowMo(DataStorage.fireRate[DataStorage.curWeapon] + .5f));
                break;
            case "Revolver":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Scylla":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Day Ender":
                gunShots[13].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Redeemer":
                gunShots[12].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "HellFire":
                gunShots[2].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(SlowMo(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Energy Rifle":
                gunShots[14].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
            case "Eradicator":
                gunShots[15].Play();
                DataStorage.holster[DataStorage.curWeapon] -= 1;
                DataStorage.UpdateHolster();
                DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
                DataStorage.combat.GetComponent<Animator>().speed = 0f;
                StartCoroutine(StopWatch(DataStorage.fireRate[DataStorage.curWeapon] / 2));
                break;
        }
    }

    IEnumerator TrippleShot(float waitTime)//this function is for automatics only
    {
        for (int i = 0; i < 3; i++)
        {
            gunShots[5].Play();
            DataStorage.holster[DataStorage.curWeapon] -= 1;
            DataStorage.UpdateHolster();
            DataStorage.crosshair.GetComponent<Animator>().Play("Hit", -1, 0f);
            yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
        }
        if (Time.time > fireRate || DataStorage.holster[DataStorage.curWeapon] < 1)
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
    }

    IEnumerator SlowMo(float waitTime)//this function is for automatics only
    {
        yield return new WaitForSeconds(waitTime);//if gun is still firing, play animation
        if (Time.time > fireRate || DataStorage.holster[DataStorage.curWeapon] < 1)
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
    }

    IEnumerator StopWatch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
            DataStorage.combat.GetComponent<Animator>().speed = 1f;
    }
}
