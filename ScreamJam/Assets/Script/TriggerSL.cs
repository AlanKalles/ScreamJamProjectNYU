using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSL : MonoBehaviour
{
    public PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        player.SavePlayer();
    }

    public void Load()
    {
        player.LoadPlayer();
    }
}
