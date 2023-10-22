using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class stageManager : MonoBehaviour
{
    public static GameStage curStage = GameStage.BeginScene;
    public static stageManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (curStage)
        {
            case GameStage.BeginScene:
                if (SceneManager.GetActiveScene().name == "D1ChildDayTime")
                {
                    StartAndWait(GameStage.Day1Wakeup, 2);
                }
                break;

            default:
                break;
        }
    }

    public void StartStage(GameStage newStage)
    {
        EndStage(curStage);

        switch (newStage)
        {
            case GameStage.Day1Wakeup:
                Dialogue d = new Dialogue();
                d.SetText(new string[] { "Rumi: Oh, What a good life!" });
                dialogueManager.dManager.StartDialogue(d);
                break;


            case GameStage.waitStage:
                PlayerControl.instance.SetState(PlayerControl.State.wait);
                break;

            default:
                break;
        }

        curStage = newStage;
    }


    public void StartAndWait(GameStage newStage, float f)
    {
        StartStage(GameStage.waitStage);
        StartCoroutine(waitSeconds(f, newStage));
    }

    private void EndStage(GameStage oldStage)
    {
        switch (oldStage)
        {
            case GameStage.waitStage:
                PlayerControl.instance.SetState(PlayerControl.State.walk);
                break;

            default:
                break;
        }
    }



    IEnumerator waitSeconds(float n, GameStage toStage)
    {
        yield return new WaitForSeconds(n);
        StartStage(toStage);
    }   
}

public enum GameStage
{
    BeginScene,
    Day1Wakeup,
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
    Ending,
    waitStage
}
