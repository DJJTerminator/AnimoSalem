using UnityEngine;
using System.Collections;

public class TakingDamageScript : MonoBehaviour {
    int enemyDamage;
    public GameObject Backgrounds;
    void Start()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        StartCoroutine (ShakeUntil(.05f));
    }
    //animating the screen for taking damage
    IEnumerator ShakeUntil(float waitTime)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Backgrounds.GetComponent<Animator>().Play("ScreenShake", -1, 0f);
        }
    }
}
