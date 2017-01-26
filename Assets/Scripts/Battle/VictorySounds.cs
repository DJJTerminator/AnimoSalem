using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySounds : MonoBehaviour {
    public AudioSource quickSwish;
    public AudioSource rakeWoosh;
    public AudioSource bonus;


    void RakeSound()
    {
        rakeWoosh.Play();
    }
    void SwishSound()
    {
        if (quickSwish  != null)
        quickSwish.Play();
    }
    void Bonus()
    {
        bonus.Play();
    }

}
