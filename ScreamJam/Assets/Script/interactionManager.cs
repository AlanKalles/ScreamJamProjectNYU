using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionManager : MonoBehaviour
{

    //并未测试
    public static interactionManager iManager;
    public bool inInteraction = false;
    public GameObject player;
    private List<Interactable> potentialInteractions = new List<Interactable>();

    private void Awake()
    {
        if(iManager != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        iManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (potentialInteractions.Count != 0)
        {
            disPlayInteraction(potentialInteractions[0]);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (stageManager.curStage == GameStage.waitStage) return;
                if (inInteraction)
                {
                    if (potentialInteractions[0].quitable)
                    {
                        potentialInteractions[0].quit();
                    }
                    inInteraction = false;
                }
                else
                {
                    inInteraction = true;
                    potentialInteractions[0].Action();
                }
            }
            //if (Input.GetKeyDown(KeyCode.E) && ! inInteraction)
            //{
            //    potentialInteractions[0].Action();
            //    inInteraction = true;
            //}
            //if (inInteraction)
            //{
                
            //    if (potentialInteractions[0].quitable)
            //    {
                    
            //        if (Input.GetMouseButtonDown(1))
            //        {
            //            potentialInteractions[0].quit();
            //            inInteraction = false;
            //        }
            //    }
            //}
        }
    }

    private void FixedUpdate()
    {
        if (potentialInteractions.Count > 1)
        {
            sortInteractions(player.transform.position);
        }
    }

    public bool IsInInteraction()
    {
        return inInteraction;
    }

    //这个只需要把最近的放到最前面就行
    private void sortInteractions(Vector2 centerPos)
    {
        potentialInteractions.Sort((a, b) =>  Vector2.Distance(centerPos, a.returnPos()).CompareTo(Vector2.Distance(centerPos, b.returnPos())));
    }

    public void connectInteraction(Interactable pI)
    {
        potentialInteractions.Add(pI);
        sortInteractions(player.transform.position);
    }

    public void disconnect(Interactable pI)
    {
        if (pI.quitable)
            pI.quit();
        inInteraction = false;
        potentialInteractions.Remove(pI);
        sortInteractions(player.transform.position);
    }

    private void disPlayInteraction(Interactable i)
    {

    }
}
