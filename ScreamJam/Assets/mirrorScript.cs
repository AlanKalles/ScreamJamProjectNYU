using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorScript : Clickable
{
    public Vector3 focusPos;
    private bool inFocusing = false;
    private Vector3 recordPos;
    private float focusCameraSize = 3;

    internal override void OnClick()
    {
        if (!inFocusing)
        {
            recordPos = cameraScript.curCameraObj.transform.position;
            cameraScript.curCameraObj.transform.position = focusPos;
            cameraScript.curCamera.orthographicSize = focusCameraSize;
        }
    }

    
}
