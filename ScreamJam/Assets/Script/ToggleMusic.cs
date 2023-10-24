using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    private GameStage lastStage;

    public AudioClip[] newMusicClips; // Ҫ���ŵ�������
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastStage = stageManager.curStage;
    }

    void OnDestroy()
    {
        // ����������ʱȡ��ע��
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        CheckForStageChange();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ݳ�����ѡ������
        switch (scene.name)
        {
            case "D1ChildDayTime":
                Audio.instance.PlayMusic(newMusicClips[0]);
                break;
            
            default:
                // ��ѡ�����û��ƥ��ĳ����������ܻ�ֹͣ���ֲ��Ż򱣳ֵ�ǰ����
                return;
        }

        

    
    }

    void CheckForStageChange()
    {
        GameStage currentStage = stageManager.curStage;  

        // ����׶η����˱仯�������½׶�����һ���׶β�ͬ���򴥷����ֱ仯
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

                // ... ������Ϸ�׶κ�����

                default:
                    // ��ѡ�����û��ƥ�����Ϸ�׶Σ����ܻ�ֹͣ���ֲ��Ż򱣳ֵ�ǰ����
                    break;
            }

            lastStage = currentStage; // ������һ���׶�Ϊ��ǰ�׶�
        }
    }



}
