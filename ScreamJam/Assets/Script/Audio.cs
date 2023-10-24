using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 确保在加载新场景时不会销毁此游戏对象
        }
        else
        {
            Destroy(gameObject); // 确保只有一个音频管理器实例
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    // 其他管理音乐播放的方法...
}
