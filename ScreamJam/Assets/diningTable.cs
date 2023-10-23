using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diningTable : Talkable
{
    public Dialogue d = new Dialogue();
    public GameStage tarStage;

    // Start is called before the first frame update
    void Start()
    {
        selfPos = this.transform;
        iController = new DialogueInteraction(d);
    }

    internal override void OnTriggerEnter2D(Collider2D collision)
    {
        if (stageManager.curStage != tarStage) { return; }
        base.OnTriggerEnter2D(collision);
    }
}
