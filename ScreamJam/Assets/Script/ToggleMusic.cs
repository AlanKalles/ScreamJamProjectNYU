using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    public AudioClip[] newMusicClips; // Ҫ���ŵ�������
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // ����������ʱȡ��ע��
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ݳ�����ѡ������
        switch (scene.name)
        {
            case "D1ChildDayTime":
                Audio.instance.PlayMusic(newMusicClips[0]);
                break;
            case "D1ChildNightTime 1":
                Audio.instance.PlayMusic(newMusicClips[1]);
                break;
            // ... ��������
            default:
                // ��ѡ�����û��ƥ��ĳ����������ܻ�ֹͣ���ֲ��Ż򱣳ֵ�ǰ����
                return;
        }
    }
}
