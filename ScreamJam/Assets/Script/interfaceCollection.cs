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
    public GameObject player;
    private Transform selfPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            interactionManager.iManager.connectInteraction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            interactionManager.iManager.disconnect(this);
        }
    }

    public Vector2 returnPos() { return selfPos.position; }

    public abstract void Action();
}


public abstract class Talkable : Interactable
{
    DialogueInteraction iController { get; set; }

    public override void Action()
    {
        iController.triggerDialogue();
    }
}