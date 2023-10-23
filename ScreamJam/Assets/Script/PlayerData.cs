using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData 
{
 
    public int progress;
    public float[] playerPosition;
    public string sceneName;


    public PlayerData(PlayerControl player)
    {
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        sceneName = SceneManager.GetActiveScene().name;
    }
}
