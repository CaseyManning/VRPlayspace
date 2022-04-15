using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PayphoneButton : XRBaseInteractable
{
    public UnityEvent OnPress = null;

    private float previousHandDepth = 0.0f;
    XRBaseInteractor hoverInteractor;

    float zMin = 0;
    float zMax = 0;

    private bool didPress = false;

    float lastPressTime = 0;

    protected override void Awake()
    {
        base.Awake();
        hoverEntered.AddListener(StartPress);
        hoverExited.AddListener(EndPress);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hoverEntered.RemoveListener(StartPress);
        hoverExited.RemoveListener(EndPress);
    }

    private void StartPress(HoverEnterEventArgs args)
    {
        hoverInteractor = args.interactor;
        previousHandDepth = transform.position.z;
    }

    private void EndPress(HoverExitEventArgs args)
    {
        hoverInteractor = null;
        previousHandDepth = 0f;
        SetzPosition(zMax);
        //OnPress.Invoke();
    }

    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        zMin = transform.position.z - (collider.bounds.size.z) / 2;
        zMax = transform.position.z;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(hoverInteractor)
        {
            float newHandDepth = hoverInteractor.transform.transform.position.z;
            float handDifference = previousHandDepth - newHandDepth;

            previousHandDepth = newHandDepth;

            float newPos = transform.position.z - handDifference;

            SetzPosition(newPos);

            CheckPress();
        }
    }

    private void SetzPosition(float pos)
    {
        Vector3 newPosition = transform.position;
        newPosition.z = Mathf.Clamp(pos, zMin, zMax);

        transform.position = newPosition;
    }
    private void CheckPress()
    {
        bool inPosition = InPosition();

        if(inPosition && Time.time - lastPressTime > 0.5f)
        {
            OnPress.Invoke();
            lastPressTime = Time.time;
            didPress = true;
        } else
        {
            didPress = false;
        }
        
    }
    private bool InPosition()
    {
        return Mathf.Abs(transform.position.z - zMin) < Mathf.Abs(zMax - zMin)/3;
    }
}
