using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float currentSpeed;

    private void Awake()
    {

        instance = this;
    }

    public enum State 
    {
        walk,
        run,
        interact,
        wait
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
            Move(currentSpeed);
        }

        if (Input.GetKeyDown(KeyCode.I) && !interactionManager.iManager.IsInInteraction())
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name != "StartScene") 
            {
                // 如果不是开始场景或其他不允许使用道具列表的场景，则切换道具列表的显示
                InventoryManager.instance.ToggleInventoryDisplay();
            }
            
        }

    }

    private void Move(float speed)
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(moveX * walkSpeed, rb.velocity.y);
        rb.velocity = move;

        //camera movement
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = transform.position.x;
        Camera.main.transform.position = camPos;
    }

    private void StateMachine()
    {
        if (state == State.walk)
        {
            movable = true;
            currentSpeed = walkSpeed;
        }

        if(state == State.interact || state == State.wait)
        {
            movable = false;
        }

        if (state == State.run)
        {
            movable = true;
            currentSpeed = runSpeed;
        }
    }

    public void SetState(State toState)
    {
        state = toState;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        transform.position = position;

        SceneManager.LoadScene(data.sceneName);
    }
}
