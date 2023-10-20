using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;




public class dialogueManager : MonoBehaviour
{
    public Image leftObject;
    public Image rightObject;
    public static Image leftImage;
    public static Image rightImage;
    public static dialogueManager dManager;

    
    public TextMeshProUGUI dialogueText;
    public Dialogue d = new Dialogue();

    private Queue<string> sentences = new Queue<string>();
    private Dialogue curDialogue = null;
    private int curIndex = 0;

    private bool inCoroutine = true;//the bool to stop coroutine
    private bool running = false;//the bool to detect if coroutine is running

    private void Awake()
    {
        leftImage = leftObject;
        rightImage = rightObject;
        if(dManager != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(dialogueText.transform.parent.gameObject);
        dManager = this;
        dialogueText.gameObject.SetActive(false);
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        //for test
        /*
        spriteModifier sMod1 = new spriteModifier(1,
                                                (GameObject gm) => { gm.SetActive(true); },
                                                (GameObject gm) => { gm.SetActive(true); });
        spriteModifier sMod2 = new spriteModifier(3,
                                                _rm: (GameObject gm) => { SetImageOpacity(gm.GetComponent<Image>(), 0.2f); });
        spriteModifier sMod3 = new spriteModifier(4,
                                                 (GameObject gm) => { gm.GetComponent<Image>().color = Color.gray; },
                                                 (GameObject gm) => { SetImageOpacity(gm.GetComponent<Image>(), 1f); });
        d.SetModQue(new Queue<spriteModifier>(new[] { sMod1, sMod2, sMod3}));
        StartDialogue(d);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curDialogue != null)
            {
                nextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue _d)
    {
        //没有加角色禁用的部分，同理没有加解锁部分
        curDialogue = _d;
        curIndex = 0;
        dialogueText.enabled = true;

        leftImage.sprite = curDialogue.sprLeft;
        rightImage.sprite = curDialogue.sprRight;
        dialogueText.gameObject.SetActive(true);
        leftImage.gameObject.SetActive(true); 
        rightImage.gameObject.SetActive(true);
        sentences.Clear();
        foreach (string sentence in _d.text)
        {
            sentences.Enqueue(sentence);
        }
        if (sentences.Count != 0)
        {
            nextSentence();
        }

    }

    private void nextSentence()
    {
        if (running)
        {
            running = false;
            inCoroutine = false;
        }
        else
        {
            if (curIndex == curDialogue.text.Length)
            {
                EndSentence();
                return;
            }
            inCoroutine = true;
            string curText = sentences.Dequeue();
            //display
            StartCoroutine(TypeSentence(curText, curDialogue, curIndex));
            running = true;
            curIndex += 1;
        }
    }

    private void EndSentence()
    {
        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        curDialogue = null;
        dialogueText.enabled = false;
        interactionManager.iManager.inInteraction = false;
    }

    IEnumerator TypeSentence(string sentence, Dialogue d, int index)
    {
        d.checkDialogue(index);
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (inCoroutine)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                dialogueText.text = sentence;
                break;
            }

        }
        running = false;
    }

    public static void SetImageOpacity(Image img, float opacity)
    {
        if (img != null)
        {
            Color currentColor = img.color;
            currentColor.a = opacity;
            img.color = currentColor;
            
        }
    }
}



//用来代表角色对话的类
[System.Serializable]
public class Dialogue
{
    //角色的名字和内容
    [TextArea(3,10)]
    public string[] text = null;//每句话包含说话者， Neil:....
    public Sprite sprLeft = null;//说话时左右的立绘和
    public Sprite sprRight = null;


    private Queue<spriteModifier> modQue = new Queue<spriteModifier>();

    public Dialogue(Queue<spriteModifier> _mQ = null)
    {
        modQue = _mQ;
    }

    public void checkDialogue(int counter)
    {
        if (modQue!= null && modQue.Count > 0)
        {
            if (counter == modQue.Peek().pointer)
            {
                modQue.Dequeue().modifySpr();
            }
        }
    }

    public void SetModQue(Queue<spriteModifier> qsm) { modQue = qsm; }
}


//这种委托代表了对于一个特定的UI Image组件进行调试
public delegate void modSpr(GameObject uiSpr);

//这个类代表了，在dialogue进行到指针处时，可以执行modifySpr方法，
public class spriteModifier
{
    public int pointer;
    private modSpr leftModification;
    private modSpr rightModification;

    public spriteModifier(int _p, modSpr _lm = null, modSpr _rm = null)
    {
        pointer = _p;
        leftModification = _lm;
        rightModification = _rm;
    }

    public spriteModifier(int _p, bool _lm, bool _rm)
    {
        pointer = _p;
        leftModification = (GameObject gm) => { gm.SetActive(_lm); };
        rightModification = (GameObject gm) => { gm.SetActive(_rm); };
    }

    public spriteModifier(int _p, Color _lm, Color _rm)
    {
        pointer = _p;
        leftModification = (GameObject gm) => { gm.GetComponent<Image>().color = Color.gray; };
        rightModification = (GameObject gm) => { gm.GetComponent<Image>().color = Color.gray; };
    }


    //参考中功能为变亮变暗，所以现在为瞬间modify
    public void modifySpr()
    {
        if (leftModification != null) { leftModification(dialogueManager.leftImage.gameObject); }
        if (rightModification != null) { rightModification(dialogueManager.rightImage.gameObject); }

    }
}



