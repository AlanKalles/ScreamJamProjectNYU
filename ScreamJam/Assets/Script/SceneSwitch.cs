using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    public Image mask;

    public static SceneSwitch instance;
    private WaitForFixedUpdate wait;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mask.transform.parent.gameObject);
    }
    private void Start()
    {
        wait = new WaitForFixedUpdate();
    }
    public static void SwitchToScene(int toScene)
    {
        instance.StartCoroutine(instance.AnimateSceneSwitch(toScene));
    }
    IEnumerator AnimateSceneSwitch(int toScene)
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(toScene);
        o.allowSceneActivation = false;

        int interval = 35;
        float t;
        Color color = Color.black;
        for (int i = 1; i <= interval; i++)
        {
            t = (float)i / interval;
            color.a = t;
            mask.color = color;
            yield return wait;
        }
        while (o.progress < 0.9f)
            yield return wait;
        o.allowSceneActivation = true;
        transform.SetSiblingIndex(0);
        yield return new WaitForSeconds(0.3f);
        for (int i = interval-1; i >-1; i--)
        {
            t = (float)i / interval;
            color.a = t;
            mask.color = color;
            yield return wait;
        }
    }

    //only for debug, delete when release the project
    public void SwitchSceneUI(TMP_InputField inputField)
    {
        SwitchToScene(int.Parse(inputField.text));
    }
}
