using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkableBox : Talkable
{

    public Dialogue d = new Dialogue();

    // Start is called before the first frame update
    void Start()
    {
        selfPos = this.transform;
        spriteModifier sMod1 = new spriteModifier(1,
                                                (GameObject gm) => { gm.SetActive(true); },
                                                (GameObject gm) => { gm.SetActive(true); });
        spriteModifier sMod2 = new spriteModifier(3,
                                                _rm: (GameObject gm) => { dialogueManager.SetImageOpacity(gm.GetComponent<Image>(), 0.2f); });
        spriteModifier sMod3 = new spriteModifier(4,
                                                 (GameObject gm) => { gm.GetComponent<Image>().color = Color.gray; },
                                                 (GameObject gm) => { dialogueManager.SetImageOpacity(gm.GetComponent<Image>(), 1f); });
        d.SetModQue(new Queue<spriteModifier>(new[] { sMod1, sMod2, sMod3 }));
        iController = new DialogueInteraction(d);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
