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
            DontDestroyOnLoad(gameObject); // ȷ���ڼ����³���ʱ�������ٴ���Ϸ����
        }
        else
        {
            Destroy(gameObject); // ȷ��ֻ��һ����Ƶ������ʵ��
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

    // �����������ֲ��ŵķ���...
}
