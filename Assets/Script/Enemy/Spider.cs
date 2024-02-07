using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageble
{
    public GameObject acidEffectPrefabs;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource DeathSound;
    public int Health{ get; set;}

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        
    }
    public void Damage()
    {
        if( isDead == true)
             return;
       // Debug.Log("Spider Damage()");
        Health--;
        if(Health < 1)
        {
            isDead = true;
            DeathSound.Play();
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            //Destroy(this.gameObject);
        }
        
    }
    public override void Movement()
    {
        //base.Movement();
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefabs,transform.position,Quaternion.identity);
        attackSound.Play();
    }
}