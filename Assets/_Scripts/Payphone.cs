using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Payphone : MonoBehaviour
{

    public GameObject[] screens;
    public GameObject[] videoObjs;
    

    public Material overlayMat;
    private Material screenMat;

    public int numPressed = 0;

    public AudioClip staticNoise;

    bool called = false;

    public AudioSource phoneSource;

    public AudioSource soundEffects;

    public GameObject[] spotLights;
    public GameObject[] mainLights;

    public AudioSource musicPlayer;

    public AudioClip voiceClip;

    float lastPressTime = 0;

    public AudioClip beepSound;

    // Start is called before the first frame update
    void Start()
    {
        
        screens = GameObject.FindGameObjectsWithTag("tscreen");
        videoObjs = GameObject.FindGameObjectsWithTag("tvideo");

        foreach(GameObject v in videoObjs)
        {
            VideoPlayer[] players = v.GetComponents<VideoPlayer>();
            players[0].Prepare();
            players[1].Prepare();
            players[2].Prepare();
        }

        screenMat = screens[0].GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HoverOver()
    {
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeOut()
    {
        float valueA = 0;
        float valueB = 10;
        for(float i = 0; i < 100; i++)
        {
            print(Mathf.Lerp(valueA, valueB, i / 100));
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void buttonPress()
    {
        if (Time.time - lastPressTime > 0.5f)
        {
            soundEffects.PlayOneShot(beepSound);

            lastPressTime = Time.time;
            numPressed += 1;

            if (numPressed >= 10 && !called)
            {
                StartCoroutine(doCallRoutine());
                called = true;
            }
        }
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    IEnumerator doCallRoutine()
    {
        phoneSource.Play();

        yield return new WaitForSeconds(5);

        for (int i = 0; i < screens.Length; i++)
        {
            StartCoroutine(turnOnTV(i));
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(30f);

        soundEffects.volume = 0.5f;
        soundEffects.PlayOneShot(staticNoise);
        for (int i = 0; i < screens.Length; i++)
        {
            StartCoroutine(turnOffTV(i));
        }


        foreach (GameObject light in mainLights)
        {
            light.SetActive(false);
        }
        foreach (GameObject spot in spotLights)
        {
            spot.SetActive(true);
        }
        phoneSource.Stop();
        musicPlayer.Stop();

        phoneSource.PlayOneShot(voiceClip);
    }

    IEnumerator turnOnTV(int i)
    {
        GameObject screen = screens[i];
        screen.GetComponent<MeshRenderer>().material = overlayMat;
        GameObject vidobj = videoObjs[i];

        VideoPlayer[] players = vidobj.GetComponents<VideoPlayer>();
        players[0].Play();
        soundEffects.volume = 0.5f;
        soundEffects.PlayOneShot(staticNoise);
        yield return new WaitForSeconds(1f);
        while(!players[1].isPrepared)
        {
            players[1].Prepare();
            yield return new WaitForSeconds(0.1f);
        }
        players[0].Stop();
        players[1].Play();
        yield return new WaitForSeconds(9f);
        while (!players[2].isPrepared)
        {
            players[2].Prepare();
            yield return new WaitForSeconds(0.1f);
        }
        players[1].Stop();
        players[2].Play();
    }

    IEnumerator turnOffTV(int i)
    {
        
        GameObject vidobj = videoObjs[i];

        VideoPlayer[] players = vidobj.GetComponents<VideoPlayer>();
        while (!players[0].isPrepared)
        {
            players[0].Prepare();
            yield return new WaitForSeconds(0.1f);
        }
        players[2].Stop();
        players[0].Play();

        //yield return new WaitForSeconds(1f);
        //players[0].Stop();
        //GameObject screen = screens[i];
        //screen.GetComponent<MeshRenderer>().material = screenMat;
    }
}
