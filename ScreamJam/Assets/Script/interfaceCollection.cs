using System.Collections;
using System.Collections.Generic;
using UnityEngine;





//并未测试
public class DialogueInteraction
{
    public Dialogue selfDialogue = null;

    public DialogueInteraction(Dialogue inputD)
    {
        selfDialogue = inputD;
    }

    public void triggerDialogue()
    {
        if (selfDialogue != null)
        {
            dialogueManager.dManager.StartDialogue(selfDialogue);
        }
    }

}

public abstract class Interactable : MonoBehaviour
{
    public Transform selfPos { get; set; }
    public bool quitable = false;
    public bool changeStage;
    public float n;

    internal virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionManager.iManager.connectInteraction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionManager.iManager.disconnect(this);
        }
    }

    public Vector2 returnPos() { return selfPos.position; }

    public abstract void Action();

    public virtual void quit() { }
}


public abstract class Talkable : Interactable
{
    public DialogueInteraction iController { get; set; }
    public bool checkStage = false;
    public GameStage tarStage;

    public override void Action()
    {
        if (checkStage && stageManager.curStage != tarStage) { return; }
        iController.triggerDialogue();
        dialogueManager.dManager.dEvent.endDialogue += () => { if (changeStage) { stageManager.instance.StartAndWait((GameStage)((int)stageManager.curStage + 1), n);} };
        
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkStage && stageManager.curStage != tarStage) { return; }
        base.OnTriggerEnter2D(collision);
    }
}