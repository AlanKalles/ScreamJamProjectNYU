using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diningTable : Talkable
{
    public Dialogue d = new Dialogue();

    // Start is called before the first frame update
    void Start()
    {
        selfPos = this.transform;
        iController = new DialogueInteraction(d);
    }
}
