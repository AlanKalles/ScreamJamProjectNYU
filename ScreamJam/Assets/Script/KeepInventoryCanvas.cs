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
            Destroy(gameObject); // 销毁当前场景中的额外实例
        }
        else
        {
            KIC = this;
            DontDestroyOnLoad(gameObject); // 防止销毁对象
        }
    }
}
