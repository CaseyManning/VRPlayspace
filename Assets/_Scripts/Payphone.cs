using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payphone : MonoBehaviour
{

    public int numPressed = 0;

    public AudioClip dialSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPress()
    {
        numPressed += 1;

        if(numPressed >= 10)
        {
            makeCall();
        }
    }

    public void makeCall()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(dialSound);
    }
}
