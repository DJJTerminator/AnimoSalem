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
    static public int musicType = 2; //0 is general, 1 is safe room, and 2 is battle.
    float musicTimer = 0;
    int curMusic;
    bool startTrack = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > 5f + musicTimer && startTrack == true)
        {
            ChangeTrack();
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
    }

    //playing in game music for a certain length of time
    IEnumerator PlayMusic(float playLength)
    {
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
        while (volume > 0)
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
                safeMusic[curMusic].Stop();
                break;
            //checking to see if the music was battle
            case 2:
                battleMusic[curMusic].Stop();
                break;
        }
    }
    //stopping al couritnes and preparing t start the next track
    static public void PrepareTrack(int nextTrack, float waitTime, bool playTrack)
    {
        DataStorage.gameManager.GetComponent<MusicScript>().StopAllCoroutines();
        DataStorage.gameManager.GetComponent<MusicScript>().startTrack = playTrack;
        DataStorage.gameManager.GetComponent<MusicScript>().musicTimer = Time.time + waitTime;
        DataStorage.gameManager.GetComponent<MusicScript>().StartCoroutine(TurnOffTrack(nextTrack));
    }

    static IEnumerator TurnOffTrack(int nextTrack)
    {
        DataStorage.gameManager.GetComponent<MusicScript>().musicTimer = Time.time;
        float volume = .4f;
        //turning down the volume
        while (volume > 0)
        {
            volume -= .05f;
            if (volume < 0)
                volume = 0;
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = volume;
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
                DataStorage.gameManager.GetComponent<MusicScript>().safeMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                break;
            //checking to see if the music was battle
            case 2:
                DataStorage.gameManager.GetComponent<MusicScript>().battleMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                break;
        }
        musicType = nextTrack;
    }
}
