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
                    print(DataStorage.fireRate[DataStorage.curWeapon]);
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
        gunShots[1].Play();
        gunShots[2].Play();
        DataStorage.holster[DataStorage.curWeapon] -= 1;
        DataStorage.UpdateHolster();
    }
}
