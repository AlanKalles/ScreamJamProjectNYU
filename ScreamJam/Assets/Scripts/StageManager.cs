using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    private int stage;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    public static void SetStage(int s)
    {
        instance.stage |= s;
    }
}
//Day1
//ºÍÂèÂè½»Á÷
//