using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField] protected int gems;
    [SerializeField]
    protected Transform pointA,pointB;
    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer Sprite;
    protected bool IsHit = false;
    protected Player player;
    protected bool isDead = false;
    protected BoxCollider2D enemyCollider;
    //public GameObject diamond;
   // [SerializeField]
    //protected RuntimeAnimatorController enemyAnimatorController;
    
public virtual void Init()
{
    anim = GetComponentInChildren<Animator>();
    Sprite = GetComponentInChildren<SpriteRenderer>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    enemyCollider = GetComponent<BoxCollider2D>();
    //Debug.Log("Animator for " + gameObject.name + ": " + anim);


   // if (enemyAnimatorController != null)
        //{
          //  anim.runtimeAnimatorController = enemyAnimatorController;
       // }
      //  else
      //  {
           // Debug.LogError("Animator Controller not assigned to the Enemy script.");
      //  }
}
private void Start()
{
    Init();
}

public virtual void Update()
{
    if( anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && anim.GetBool("InCombat") == false)
   {
    return;
   }
   if(isDead == false)
   Movement();
}

public virtual void Movement()
{
       if(currentTarget == pointA.position)
        {
            Sprite.flipX = true;
        }
        else{
            Sprite.flipX = false;
        }

    if( transform.position == pointA.position)
    {
        currentTarget = pointB.position;
        anim.SetTrigger("Idle");
    }
    else if(transform.position == pointB.position)
    {
        currentTarget = pointA.position;
        anim.SetTrigger("Idle");
    }

    if(IsHit == false)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
    if(distance > 2.0f)
    {
        IsHit = false;
        anim.SetBool("InCombat",false);
    }

    
         Vector3 direction = player.transform.localPosition - transform.localPosition;
       
        if(direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            Sprite.flipX = false;
        }
        else if(direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            Sprite.flipX = true;
        }



    }
}

