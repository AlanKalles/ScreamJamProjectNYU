using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorInteraction : Interactable
{
    public Vector3 focusPos;
    private Vector3 recordPos;
    private float recordSize;
    private float focusCameraSize = 3;

    private void Start()
    {
        selfPos = this.transform;
        quitable = true;
    }

    public override void Action()
    {
        recordPos = cameraScript.curCameraObj.transform.position;
        cameraScript.curCameraObj.transform.position = focusPos;
        recordSize = cameraScript.curCamera.orthographicSize;
        cameraScript.curCamera.orthographicSize = focusCameraSize;
    }

    public override void quit()
    {
        cameraScript.curCameraObj.transform.position = recordPos;
        cameraScript.curCamera.orthographicSize = recordSize;
    }
}
