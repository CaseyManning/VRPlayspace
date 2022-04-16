using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class fingor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRDirectInteractor>().interactionManager = GameObject.FindObjectOfType<XRInteractionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
