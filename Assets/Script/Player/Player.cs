using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public enum PlayerStateType
{
    Idle,Walk,JumpUp,Air
}
public class Player : MonoBehaviour
{
    private Dictionary<PlayerStateType, IState> states;
    private IState currentState;

    public SpriteRenderer sr;
    public Animator anim;
    public Rigidbody2D rd;
    public PlayerInput playerInput;
    public CapsuleCollider2D collider;


    [Header("ÒÆ¶¯")]
    public bool isMove;
    public float speed;
    public Vector2 direction;

    [Header("ÌøÔ¾")]
    public bool isJump;
    public float jumpForce;

    [Header("ÏÂÂä")]
    public bool isAir;

    [Header("Åö×²¼ì²â")]
    public LayerMask whatIsGround;
    public float groundCheckDistance;
    public Transform groundCheckPosition;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsObj1;

    [Header("brickÅö×²")]
    public Transform brickCheckPosition;
    public float brickCheckDistance;
    public LayerMask whatIsBrick;

    [Header("²ÄÖÊ")]
    public PhysicsMaterial2D yes;
    public PhysicsMaterial2D no;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rd = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();

        states = new Dictionary<PlayerStateType, IState>();
        states.Add(PlayerStateType.Idle, new PlayerIdleState(this));
        states.Add(PlayerStateType.Walk, new PlayerWalkState(this));
        states.Add(PlayerStateType.JumpUp, new PlayerJumpUpState(this));
        states.Add(PlayerStateType.Air, new PlayerAirState(this));

        tranState(PlayerStateType.Idle);
        playerInput.enablePlayerinput();
    }


    private void OnEnable()
    {
        playerInput.onWalk += onMove;
        playerInput.disWalk += disMove;
        playerInput.onJump += onJump;
    }

    private void OnDisable()
    {
        playerInput.onWalk -= onMove;
        playerInput.disWalk -= disMove;
        playerInput.onJump -= onJump;
    }
    private void onJump()
    {
        this.isJump = true;
        
    }
    public void disMove()
    {
        isMove = false;
        this.direction = Vector2.zero;
    }
    public void onMove(Vector2 direction)
    {
        isMove = true;
        this.direction = direction;  
    }
    public void tranState(PlayerStateType state)
    {
        if (currentState != null)
            currentState.onExit();
        currentState = states[state];
        currentState.onEnter();
    }
    private void Update()
    {
        currentState.onUpdate();
        if (transform.position.y < -10)
            die();
    }
    private void FixedUpdate()
    {
        currentState.onFixedUpdate();
    }

    public void move()
    {
        if (direction.magnitude > 0.99f)
        {
            rd.velocity = direction * speed;
            if (direction.x > 0)
                sr.flipX = false;
            if (direction.x < 0)
                sr.flipX = true;
        }
    }

    public void jumpupMove()
    {
        if (direction.magnitude > 0.99f)
        {
            rd.velocity = new Vector2(direction.x * speed, rd.velocity.y);
            if (direction.x > 0)
                sr.flipX = false;
            if (direction.x < 0)
                sr.flipX = true;
        }
    }
    public void grow()
    {
        transform.localScale *= 1.5f;
        groundCheckDistance *= 1.5f;
        brickCheckDistance *= 1.5f;
        jumpForce *= 1.1f;
    }


    public void die()
    {
        SceneManager.LoadScene("died");
    }
    public bool groundCheck()
    {
        return Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, whatIsGround)|| Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, whatIsBrick)|| Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, whatIsObj1);
    }

    public RaycastHit2D brickCheck()
    {
        return Physics2D.Raycast(brickCheckPosition.position, Vector2.up, brickCheckDistance, whatIsBrick);
    }
    public RaycastHit2D enemyCheck()
    {
        return Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, whatIsEnemy);
    }
    public RaycastHit2D obj1Check()
    {
        return Physics2D.Raycast(brickCheckPosition.position, Vector2.up, brickCheckDistance, whatIsObj1);
    }
    

}
