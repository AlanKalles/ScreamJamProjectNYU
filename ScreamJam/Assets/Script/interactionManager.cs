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
        iManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (potentialInteractions.Count != 0)
        {
            disPlayInteraction(potentialInteractions[0]);
            if (Input.GetKeyDown(KeyCode.E) && ! inInteraction)
            {
                potentialInteractions[0].Action();
            }
        }
    }

    private void FixedUpdate()
    {
        if (potentialInteractions.Count > 1)
        {
            sortInteractions(player.transform.position);
        }
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
        potentialInteractions.Remove(pI);
        sortInteractions(player.transform.position);
    }

    private void disPlayInteraction(Interactable i)
    {

    }
}
