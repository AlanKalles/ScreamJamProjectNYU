using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomable : Interactable
{
    public GameObject zoomedPrefab;
    private void Start()
    {
        zoomedPrefab.SetActive(false);
    }
    public override void Action()
    {
        zoomedPrefab.SetActive(true);
    }
    public override void quit()
    {
        zoomedPrefab.SetActive(false);
    }
}
