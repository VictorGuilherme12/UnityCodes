using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{

    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //distancia minima pro ataque
    public float moveSpeed;
    public float timer; // tempo entre os ataques
    #endregion 

    #region Private Variables

    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion

   
   void awake()
   {
    intTimer = timer; 
    anim = GetComponent<Animator>();
   }
   

    void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast (rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RaycastDebugger();
        }


        //animation
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();

        }
    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
                target = trig.gameObject;
                inRange = true;
        }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            anim.SetBool("Attack", false); 
        }

    }

    void move()
        {

            anim.SetBool("CanWalk", true);
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Skel_attack"))
            {
                Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }


    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode =  false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
    }
}


