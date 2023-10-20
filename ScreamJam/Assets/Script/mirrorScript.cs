using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorScript : Clickable
{
    public float x_left, x_right,speed;
    private Vector3 reflectionPos;
    private Transform reflectionTrans;
    private Rigidbody2D _rb;

    internal override void Start()
    {
        clickable = false;
        base.Start();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        reflectionPos = this.transform.Find("reflection").position;
        reflectionTrans = this.transform.Find("reflection");
    }

    internal override void FixedUpdate()
    {
        
    }

    internal override void Update()
    {
        if (!clickable) return;
        //Debug.Log("Ue");
        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = speed * new Vector2(-1, 0);
            reflectionTrans.position = reflectionPos;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = speed * new Vector2(1, 0);
            reflectionTrans.position = reflectionPos;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    internal override void OnClick()
    {
        
    }

    
}
