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

    public static PlayerControl instance;

    public enum State
    {
        walk,
        run,
        interact,
        wait
    }


    private void Awake()
    {

        instance = this;
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

    }

    private void StateMachine()
    {
        if (state == State.walk)
        {
            movable = true;
            Move(walkSpeed);
        }

        if(state == State.interact || state == State.wait)
        {
            movable = false;
        }

        if(state == State.run)
        {
            movable = true;
            Move(runSpeed);
        }
    }

    public void toggleState(string str)
    {
        if(str == "walk")
        {
            state = State.walk;
        }
        if(str == "run")
        {
            state = State.run;
        }
        if(str == "interact")
        {
            state = State.interact;
        }
    }

    public void SetState(State toState)
    {
        state = toState;
    }


}
