using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public static GameObject curCameraObj;
    public static Camera curCamera;

    private void Awake()
    {
        curCamera = this.gameObject.GetComponent<Camera>();
        curCameraObj = this.gameObject;
    }
}
