using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyStateType
{
    Patrol,Dead
}
public class Enemy : MonoBehaviour
{
    private Dictionary<EnemyStateType, IState> states;
    private IState currentState;

    public Rigidbody2D rd;
    public SpriteRenderer sr;
    public Animator anim;


    public Vector2 faceDir;
    public float speed;


    [Header("Åö×²¼ì²â")]
    public LayerMask whatIsGround;
    public float wallCheckDistance;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsPlayer;
    public float attackDistance;

    [Header("ËÀÍö")]
    public bool isDead;
    public float stayTime;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        states = new Dictionary<EnemyStateType, IState>();
        states.Add(EnemyStateType.Patrol,new  EnemyPatrolState(this));
        states.Add(EnemyStateType.Dead, new EnemyDeadState(this));
        tranState(EnemyStateType.Patrol);
        faceDir = new Vector2(1, 0);
    }
    public void tranState(EnemyStateType state)
    {
        if (currentState != null)
            currentState.onExit();
        currentState = states[state];
        currentState.onEnter();
    }
    private void Update()
    {
        currentState.onUpdate();
    }
    private void FixedUpdate()
    {
        currentState.onFixedUpdate();
    }

    public void move()
    {
        rd.velocity = new Vector2(faceDir.x * speed,rd.velocity.y);
        if (faceDir.x > 0)
            sr.flipX = false;
        if (faceDir.x < 0)
            sr.flipX = true;
    }
    public void changeFaceDir()
    {
        faceDir = -faceDir;
    }

    public bool wallCheck()
    {

        return Physics2D.Raycast(transform.position, faceDir, wallCheckDistance, whatIsGround);
    }
    public bool enemyCheck()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, faceDir, wallCheckDistance * 1.3f, whatIsEnemy);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
    
    public void attackPlayer()
    {
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, whatIsPlayer);

        foreach(Collider2D collider in colliders)
        {
             collider.GetComponent<Player>().die();
        }*/
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, attackDistance,whatIsPlayer);
        if(hit.collider!=null)
            hit.collider.GetComponent<Player>().die();
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, attackDistance,whatIsPlayer);
        if (hit1.collider != null)
            hit1.collider.GetComponent<Player>().die();
    }

    public void die()
    {
        isDead = true;
        StartCoroutine(deadAndStay());
    }
    public IEnumerator deadAndStay()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject.Destroy(gameObject);
    }
}
