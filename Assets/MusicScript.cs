using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    [SerializeField]
    AudioSource[] inGameMusic;
    [SerializeField]
    AudioSource[] safeMusic;
    [SerializeField]
    AudioSource[] battleMusic;
    static public int musicType; //0 is general, 1 is safe room, and 2 is battle.
    float musicTimer = 0;
    int curMusic;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > musicTimer + 5)
        {
            ChangeTrack();
        }
        if (Input.GetKeyDown("o"))
        {
            StopAllCoroutines();
            StartCoroutine(TurnOffTrack());
        }
    }
    //*****changing the track
    void ChangeTrack()
    {
        musicTimer = Time.time + (Random.Range(45, 65));
        int temp = 0;
        switch (musicType)
        {
            //checking to see if the music was general
            case 0:
                temp = Random.Range(0, inGameMusic.Length);//if so, find a new track
                while (temp == curMusic)//checking to see if the track is already playing
                {
                    temp = Random.Range(0, inGameMusic.Length);//if so, find a new track
                }
                    break;
            //checking to see if the music was safe room
            case 1:
                temp = Random.Range(0, safeMusic.Length);//if so, find a new track
                while (temp == curMusic)//checking to see if the track is already playing
                {
                    temp = Random.Range(0, safeMusic.Length);//if so, find a new track
                }
                break;
            //checking to see if the music was battle
            case 2:
                temp = Random.Range(0, battleMusic.Length);//if so, find a new track
                while (temp == curMusic)//checking to see if the track is already playing
                {
                    temp = Random.Range(0, safeMusic.Length);//if so, find a new track
                }
                break;
        }
        curMusic = temp;
        StartCoroutine(PlayMusic(musicTimer - Time.time));
        print("play: ");
    }


    //playing in game music for a certain length of time
    IEnumerator PlayMusic(float playLength)
    {
        print(curMusic);
        switch (musicType)
        {
                //checking to see if the music was general
                case 0:
                inGameMusic[curMusic].Play();
                break;
                //checking to see if the music was safe room
                case 1:
                safeMusic[curMusic].Play();
                break;
                //checking to see if the music was battle
                case 2:
                battleMusic[curMusic].Play();
                break;
        }
        float volume = 0;
        //turning up the volume
        while (volume < .4f)
        {
            volume += .05f;
            if (volume > .4f)
                volume = .4f;
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    inGameMusic[curMusic].volume = volume;
                    print(inGameMusic[curMusic].volume);
                    break;
                //checking to see if the music was safe room
                case 1:
                    safeMusic[curMusic].volume = volume;
                    break;
                //checking to see if the music was battle
                case 2:
                    battleMusic[curMusic].volume = volume;
                    break;
            }
            yield return new WaitForSeconds(.2f);
        }

        //waiting to turn down the music
        yield return new WaitForSeconds(playLength);
        //turning down the volume
        while (inGameMusic[curMusic].volume > 0)
        {
            volume -= .05f;
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    inGameMusic[curMusic].volume = volume;
                    break;
                //checking to see if the music was safe room
                case 1:
                    safeMusic[curMusic].volume = volume;
                    break;
                //checking to see if the music was battle
                case 2:
                    battleMusic[curMusic].volume = volume;
                    break;
            }
            yield return new WaitForSeconds(.2f);
        }
        switch (musicType)
        {
            //checking to see if the music was general
            case 0:
                inGameMusic[curMusic].Stop();
                break;
            //checking to see if the music was safe room
            case 1:
                inGameMusic[curMusic].Stop();
                break;
            //checking to see if the music was battle
            case 2:
                inGameMusic[curMusic].Stop();
                break;
        }
    }

    static IEnumerator TurnOffTrack()
    {
        DataStorage.gameManager.GetComponent<MusicScript>().musicTimer = Time.time;
        float volume = .4f;
        //turning down the volume
        while (volume > 0)
        {
            volume -= .05f;
            if (volume < 0)
                volume = 0;
            print("printed volume" + volume);
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = volume;
                    print(DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume);
                    break;
                //checking to see if the music was safe room
                case 1:
                    DataStorage.gameManager.GetComponent<MusicScript>().safeMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = volume;
                    break;
                //checking to see if the music was battle
                case 2:
                    DataStorage.gameManager.GetComponent<MusicScript>().battleMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = volume;
                    break;
            }
            yield return new WaitForSeconds(.2f);
        }
        switch (musicType)
        {
            //checking to see if the music was general
            case 0:
                DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                break;
            //checking to see if the music was safe room
            case 1:
                DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                break;
            //checking to see if the music was battle
            case 2:
                DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                break;
        }
        if (musicType < 2)
        {
            musicType++;
        }
        else
            musicType = 0;
    }
}
