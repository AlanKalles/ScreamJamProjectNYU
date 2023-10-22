using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{
    private GameStage curStage = GameStage.Day1Wakeup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStage(GameStage newStage)
    {
        if (newStage != GameStage.Day1Wakeup) { EndStage(curStage); }

        switch (newStage)
        {
            case GameStage.Day1Wakeup:

                break;
            default:
                break;
        }

        curStage = newStage;
    }

    private void EndStage(GameStage oldStage)
    {
        switch (oldStage)
        {
            default:
                break;
        }
    }
}

public enum GameStage
{
    Day1Wakeup,
    Day1KitchenTalk,
    Day1ExploreHouse,
    Day1Dinner,
    Day1NightGhost,
    Day1NightAfterGhoset,
    Day2Morning,
    Day2PhoneTalk,
    Day2ParentsRoom,
    Day2LeaveParentsRoom,
    Day2BedroomMirror,
    Day2CollectPaperOnBed,
    Day3Morning,
    Day3ExploreHouse,
    Day3MirrorInRightPos,
    Day3BackToPast,
    Day3BackToCurrent,
    Day3InLoft,
    Day3BackToBedroom,
    Day3MidNight,
    Day3DadMonCome,
    Day3Chase,
    Ending
}
