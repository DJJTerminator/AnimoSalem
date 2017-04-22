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
    [SerializeField]
    AudioSource[] storageMusic;
    [SerializeField]
    AudioSource[] menuMusic;
    [SerializeField]
    AudioSource[] battleResults;
    [SerializeField]
    AudioSource[] startle;
    [SerializeField]
    AudioSource[] extremeBattle;
    static public int musicType = 2; //0 is general, 1 is safe room, and 2 is battle.
    float musicTimer = 0;
    int curMusic;
    bool startTrack = true;
    float curVolume;
    IEnumerator volumeDown;
    IEnumerator volumeUp;

    // Use this for initialization
    void Start()
    {
        volumeDown = VolumeDown(curVolume);
        volumeUp = VolumeDown(curVolume);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > musicTimer && startTrack == true)
        {
            ChangeTrack();
            startTrack = false;
        }
    }
    IEnumerator VolumeUp(float volume)
    {//turning the volume up
        while (curVolume < .4f)
        {
            curVolume+= .05f;
            if (curVolume > .4f)
                curVolume = .4f;
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    inGameMusic[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was safe room
                case 1:
                    safeMusic[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was battle
                case 2:
                    battleMusic[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was storage
                case 3:
                    storageMusic[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was battle results
                case 4:
                    curVolume += .05f;
                    if (curVolume > .4f)
                        curVolume = .4f;
                    battleResults[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was menu
                case 5:
                    menuMusic[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was startle
                case 6:
                    startle[curMusic].volume = curVolume;
                    break;
                //checking to see if the music was extreme Battle
                case 7:
                    extremeBattle[curMusic].volume = curVolume;
                    break;

            }
            yield return new WaitForSeconds(.2f);
        }
        yield return curVolume;
    }
    IEnumerator VolumeDown(float volume)
    {//turning the volume down
        while (curVolume > 0)
        {
            curVolume -= .05f;
            if (curVolume <= 0)
                curVolume= 0;
            switch (musicType)
            {
                //checking to see if the music was general
                case 0:
                    DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().inGameMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was safe room
                case 1:
                    DataStorage.gameManager.GetComponent<MusicScript>().safeMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume<= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().safeMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was battle
                case 2:
                    DataStorage.gameManager.GetComponent<MusicScript>().battleMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume<= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().battleMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was storage
                case 3:
                    DataStorage.gameManager.GetComponent<MusicScript>().storageMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().storageMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was battle results
                case 4:
                    curVolume -= .05f;
                    DataStorage.gameManager.GetComponent<MusicScript>().battleResults[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().battleResults[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was menu
                case 5:
                    DataStorage.gameManager.GetComponent<MusicScript>().menuMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().menuMusic[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was startle
                case 6:
                    DataStorage.gameManager.GetComponent<MusicScript>().startle[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().startle[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
                //checking to see if the music was extreme Battle
                case 7:
                    DataStorage.gameManager.GetComponent<MusicScript>().extremeBattle[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].volume = curVolume;
                    if (curVolume <= 0)
                        DataStorage.gameManager.GetComponent<MusicScript>().extremeBattle[DataStorage.gameManager.GetComponent<MusicScript>().curMusic].Stop();
                    break;
            }
            yield return new WaitForSeconds(.2f);
        }
        yield return curVolume;
    }

    //*****changing the track
    void ChangeTrack()
    {
        musicTimer = Time.time + (Random.Range(45, 60));
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
                    temp = Random.Range(0, battleMusic.Length);//if so, find a new track
                }
                break;
            //checking to see if the music was battle results
            case 4:
                temp = Random.Range(0, battleResults.Length);//if so, find a new track
                break;
            //checking to see if the music was menu
            case 5:
                temp = Random.Range(0, menuMusic.Length);//if so, find a new track
                break;
            //checking to see if the music was startle
            case 6:
                temp = Random.Range(0, startle.Length);//if so, find a new track
                break;
            //checking to see if the music was extreme
            case 7:
                temp = Random.Range(0, extremeBattle.Length);//if so, find a new track
                break;
        }
        StartCoroutine(PlayMusic(musicTimer - Time.time, temp));
    }

    //playing in game music for a certain length of time
    IEnumerator PlayMusic(float playLength, int nextTrack)
    {
        if (curVolume> 0)
        {
            //turning down the volume
            volumeDown = VolumeDown(curVolume);
            yield return StartCoroutine(volumeDown);
            yield return new WaitForSeconds((.4f / .05f) * .2f);
        }
        curMusic = nextTrack;
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
            //checking to see if the music was storage
            case 3:
                storageMusic[curMusic].Play();
                break;
            //checking to see if the music was battle results
            case 4:
                battleResults[curMusic].Play();
                break;
            //checking to see if the music was menu
            case 5:
                menuMusic[curMusic].Play();
                break;
            //checking to see if the music was startle
            case 6:
                startle[curMusic].Play();
                break;
            //checking to see if the music was extreme Battle
            case 7:
                extremeBattle[curMusic].Play();
                break;
        }
        //turning up the volume
        volumeUp = VolumeUp(curVolume);
        yield return StartCoroutine(volumeUp);
        //waiting to turn down the music
        yield return new WaitForSeconds(playLength -5f);
        startTrack = true;
    }
    //stopping al couritnes and preparing t start the next track
    static public void PrepareTrack(int nextTrack, float waitTime, bool playTrack)
    {
        DataStorage.gameManager.GetComponent<MusicScript>().StopAllCoroutines();
        DataStorage.gameManager.GetComponent<MusicScript>().musicTimer = Time.time + (Random.Range(45, 65));
        DataStorage.gameManager.GetComponent<MusicScript>().startTrack = playTrack;
        DataStorage.gameManager.GetComponent<MusicScript>().StartCoroutine(TurnOffTrack(nextTrack));
    }

    static IEnumerator TurnOffTrack(int nextTrack)
    {
        MusicScript tempScript = DataStorage.gameManager.GetComponent<MusicScript>();
        //DataStorage.gameManager.GetComponent<MusicScript>().musicTimer = Time.time;
        //turning down the volume
        tempScript.volumeDown = tempScript.VolumeDown(tempScript.curVolume);
        yield return tempScript.StartCoroutine(tempScript.volumeDown);

        musicType = nextTrack;
        if (tempScript.startTrack)
            tempScript.ChangeTrack();
    }
}
