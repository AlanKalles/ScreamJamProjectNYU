using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class stageManager : MonoBehaviour
{
    public static GameStage curStage = GameStage.BeginScene;
    public static stageManager instance;
    public TriggerSL triggerSL;

    private int[] stageCount;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        stageCount = new int[25];//这个数组的个数==GameStage枚举的个数
        for(int i = 0; i < 25; i++)
        {
            stageCount[i] = 1;
        }
        stageCount[(int)GameStage.Day1Dinner] = 2;
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
        //Debug.Log(curStage);
    }

    void StartStage(GameStage newStage)
    {
        Debug.Log(newStage);
        EndStage(curStage);

        switch (newStage)
        {
            case GameStage.Day1Wakeup:
                triggerSL.Save();
                Dialogue d = new Dialogue();
                d.SetText(new string[] { "Rumi: Oh, What a good life!" });
                dialogueManager.dManager.StartDialogue(d);
                interactionManager.iManager.inInteraction = true;
                break;

            case GameStage.Day1NightGhostHappen:
                StartCoroutine(waitSecondsWithAction(2, () =>
                {
                    //play sound
                    //start time record

                    //现在就默认玩家通过了，计时以后过关
                    StartStage(GameStage.Day1NightAfterGhoset);
                    Debug.Log("automatic pass");
                }));
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
        stageCount[(int)newStage] -= 1;
        if (stageCount[(int)newStage] > 0)
            return;
        StartStage(GameStage.waitStage);
        if (f <= 0)
            StartStage(newStage);
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

    IEnumerator waitSecondsWithAction(float n, Action a)
    {
        yield return new WaitForSeconds(n);
        a();
    }
}

public enum GameStage
{
    BeginScene,
    Day1Wakeup,
    Day1ExploreHouse,
    Day1Dinner,
    Day1NightGhost,
    Day1NightGhostHappen,
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
