using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInventoryCanvas : MonoBehaviour
{
    public static KeepInventoryCanvas KIC;
    private void Awake()
    {
        if (KIC != null)
        {
            Destroy(gameObject); // ���ٵ�ǰ�����еĶ���ʵ��
        }
        else
        {
            KIC = this;
            DontDestroyOnLoad(gameObject); // ��ֹ���ٶ���
        }
    }
}
