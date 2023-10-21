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
    [SerializeField] private State state = State.walk;

    public static PlayerControl instance;

    private void Awake()
    {
        instance = this;
    }

    public enum State //���״̬
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
        
        if (movable)
        {
            Move();
        }
        
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(moveX * walkSpeed, rb.velocity.y);
        rb.velocity = move;

    }

    private void StateMachine()
    {
        if (state == State.walk)
        {
            movable = true;
        }

        if(state == State.interact)
        {
            movable = false;
        }
    }

    public void SetState(State toState)
    {
        state = toState;
    }


}
