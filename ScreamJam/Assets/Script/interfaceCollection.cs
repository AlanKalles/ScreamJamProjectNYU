using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//并未测试
public abstract class Interactable
{
    public Dialogue selfDialogue;
    private Transform selfPos;

    public Interactable(Dialogue inputD, Transform pos)
    {
        selfDialogue = inputD;
        selfPos = pos;
    }

    public void triggerDialogue()
    {
        dialogueManager.dManager.StartDialogue(selfDialogue);
    }

    public Vector2 returnPos() { return (Vector2)selfPos.position; }
}

interface IDialogueTrigger
{
    Interactable iController { get; set; }
    //在实现时可以加入锁钥
    void connectToManager() { interactionManager.iManager.connectInteraction(iController); }
    void disconnectFromManager() { interactionManager.iManager.disconnect(iController); }
}