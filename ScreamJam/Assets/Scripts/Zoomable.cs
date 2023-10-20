using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomable : Interactable
{
    public GameObject zoomedPrefab;
    private bool zoomed = false;
    private void Start()
    {
        zoomedPrefab.SetActive(false);
    }
    public override void Action()
    {
        if (zoomed)
        {
            zoomed = false;
            zoomedPrefab.SetActive(false);
        }
        else
        {
            zoomed = true;
            zoomedPrefab.SetActive(true);
        }
    }
}
