using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy,IDamageble
{
   public int Health{ get; set;}
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource DeathSound;

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();

    }

    public void Damage()
    {
         if( isDead == true)
             return;
         
        Health --;
        anim.SetTrigger("hit");
        attackSound.Play();
        IsHit = true;
        anim.SetBool("InCombat",true);


        if(Health < 1)
        {
            isDead = true;
            DeathSound.Play();
             anim.SetTrigger("Death");
             GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            enemyCollider.enabled = false;
            //Destroy(this.gameObject);
        }
    }
}
