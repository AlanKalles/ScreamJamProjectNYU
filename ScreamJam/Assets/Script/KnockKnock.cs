using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockKnock : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public float TimeLimit = 30f; // Ê±ÏÞÎª30Ãë
    public stageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


}
