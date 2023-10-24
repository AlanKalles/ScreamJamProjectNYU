using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMusic : MonoBehaviour
{
    public AudioClip[] newMusicClips; // 要播放的新音乐
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // 当对象被销毁时取消注册
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 根据场景名选择音乐
        switch (scene.name)
        {
            case "D1ChildDayTime":
                Audio.instance.PlayMusic(newMusicClips[0]);
                break;
            case "D1ChildNightTime 1":
                Audio.instance.PlayMusic(newMusicClips[1]);
                break;
            // ... 其他场景
            default:
                // 可选：如果没有匹配的场景名，可能会停止音乐播放或保持当前音乐
                return;
        }
    }
}
