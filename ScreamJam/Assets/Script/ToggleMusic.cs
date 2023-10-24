using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    private GameStage lastStage;

    public AudioClip[] newMusicClips; // 要播放的新音乐
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastStage = stageManager.curStage;
    }

    void OnDestroy()
    {
        // 当对象被销毁时取消注册
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        CheckForStageChange();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 根据场景名选择音乐
        switch (scene.name)
        {
            case "D1ChildDayTime":
                Audio.instance.PlayMusic(newMusicClips[0]);
                break;
            
            default:
                // 可选：如果没有匹配的场景名，可能会停止音乐播放或保持当前音乐
                return;
        }

        

    
    }

    void CheckForStageChange()
    {
        GameStage currentStage = stageManager.curStage;  

        // 如果阶段发生了变化，并且新阶段与上一个阶段不同，则触发音乐变化
        if (currentStage != lastStage)
        {
            switch (currentStage)
            {
                case GameStage.Day1Dinner:
                    Audio.instance.PlayMusic(newMusicClips[1]);
                    break;

                case GameStage.Day1NightGhostHappen:
                    Audio.instance.PlayMusic(newMusicClips[2]);
                    break;

                // ... 其他游戏阶段和音乐

                default:
                    // 可选：如果没有匹配的游戏阶段，可能会停止音乐播放或保持当前音乐
                    break;
            }

            lastStage = currentStage; // 更新上一个阶段为当前阶段
        }
    }



}
