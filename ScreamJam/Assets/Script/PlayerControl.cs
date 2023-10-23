using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Attributes")]
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    [Space (10)]
    [Header("Player Components")]
    public Rigidbody2D rb;
    public GameObject player;

    [SerializeField] private bool movable = true;
    [SerializeField] private PlayerState state = PlayerState.walk;


    public enum PlayerState //���״̬
    {
        walk,
        run,
        interact
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        StateMachine();
        
    }

    private void Move(float speed)
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(moveX * speed, rb.velocity.y);
        rb.velocity = move;

        //camera movement
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = transform.position.x;
        Camera.main.transform.position = camPos;
    }

    private void StateMachine()
    {
        if (state == PlayerState.walk)
        {
            movable = true;
            Move(walkSpeed);
        }

        if(state == PlayerState.interact)
        {
            movable = false;
        }

        if(state == PlayerState.run)
        {
            movable = true;
            Move(runSpeed);
        }
    }

    public void toggleState(string str)
    {
        if(str == "walk")
        {
            state = PlayerState.walk;
        }
        if(str == "run")
        {
            state = PlayerState.run;
        }
        if(str == "interact")
        {
            state = PlayerState.interact;
        }
    }


}
