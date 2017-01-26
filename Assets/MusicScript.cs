using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    [SerializeField]
    AudioSource[] inGameMusic;
    [SerializeField]
    AudioSource[] safeMusic;
    [SerializeField]
    AudioSource[] battleMusic;

    float musicTimer;
    int curMusic;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Time.time > musicTimer + 5 && !DataStorage.battleSystem.activeSelf)
        {
            musicTimer = Time.time + (Random.Range(45, 65));

            int temp = Random.Range(0, inGameMusic.Length);
            if (temp == curMusic)//checking to see if the track is already playing
            {
                temp = Random.Range(0, inGameMusic.Length);//if so, find a new track
                if (temp == curMusic)//checking to see if the track is already playing
                {
                    temp = Random.Range(0, inGameMusic.Length);//if so, find a new track
                    if (temp == curMusic)//checking to see if the track is already playing
                    {
                        temp = Random.Range(0, inGameMusic.Length);//if so, find a new track
                    }
                }
            }
            curMusic = temp;
            inGameMusic[curMusic].Play();
            StartCoroutine(PlayMusic(musicTimer - Time.time));
            print("play: ");
        }
        else if (Time.time > musicTimer + 15 && DataStorage.battleSystem.activeSelf)
            musicTimer += 5;


    }
    
    //playing in game music
    IEnumerator PlayMusic(float playLength)
    {
        inGameMusic[curMusic].volume = 0f;
        //turning up the volume
        while (inGameMusic[curMusic].volume < .4f)
        {
            inGameMusic[curMusic].volume += .05f;
            if (inGameMusic[curMusic].volume > .4f)
                inGameMusic[curMusic].volume = .4f;
            yield return new WaitForSeconds(.2f);
        }

        //waiting to turn down the music
        yield return new WaitForSeconds(playLength);
        //turning down the volume
        while (inGameMusic[curMusic].volume > 0)
        {
            inGameMusic[curMusic].volume -= .05f;
            yield return new WaitForSeconds(.2f);
        }
        inGameMusic[curMusic].Stop();
    }
}
