using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public Rigidbody2D rb;

    [SerializeField] private bool movable = true;
    [SerializeField] private State state = State.walk;

    enum State
    {
        walk,
        run,
        interact
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

}
