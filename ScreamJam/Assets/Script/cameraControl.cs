using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class cameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; 

    private GameObject player;

    private static cameraControl _instance;
    private CinemachineConfiner confiner;

    void Awake()
    {
        // Check if instance already exists
        if (_instance == null)
        {
            // if not, set instance to this
            _instance = this;
        }
        // If instance already exists and it's not this:
        else if (_instance != this)
        {
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of CameraControl.
            Destroy(gameObject);
            return;
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        confiner = GetComponent<CinemachineConfiner>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the Main Camera in the new scene
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        GameObject cameraBoundObject = GameObject.FindGameObjectWithTag("cameraBound");

        // Check if the main camera exists
        if (mainCamera != null)
        {
            // Check if the main camera already has a CinemachineBrain component
            CinemachineBrain cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();

            // If not, add one
            if (cinemachineBrain == null)
            {
                mainCamera.AddComponent<CinemachineBrain>();
            }
        }
        else
        {
            Debug.LogError("No main camera found in the new scene.");
        }

        if (scene.name == "StartScene")
        {
            // Disable camera follow in the start scene
            cinemachineCamera.Follow = null;
        }
        else
        {
            // Enable camera follow in other scenes and set the follow target to the player
            player = GameObject.Find("Player"); // Ensure your player gameobject is named "Player" in all scenes
            
            
            if (player != null)
            {
                cinemachineCamera.Follow = player.transform;
                cinemachineCamera.m_Lens.OrthographicSize = 5;
            }
        }

        if (cameraBoundObject != null)
        {
            // 获取 cameraBoundObject 上的 PolygonCollider2D 组件
            var collider = cameraBoundObject.GetComponent<PolygonCollider2D>();

            if (collider != null)
            {
                // 设置 CinemachineConfiner 的 Bounding Shape 为找到的 collider
                confiner.m_BoundingShape2D = collider;

                // 强制 CinemachineConfiner 重新计算边界
                confiner.InvalidatePathCache();
            }
            else
            {
                Debug.LogError("No PolygonCollider2D found on the cameraBound object.");
            }
        }
        else
        {
            Debug.LogError("No object with tag cameraBound found in the scene.");
        }
    }


    void OnDestroy()
    {
        if (_instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Remember to unsubscribe when the script is destroyed
        }
    }
}
