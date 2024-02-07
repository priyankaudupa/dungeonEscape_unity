using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  private Animator _anim;
  private Animator _swordanimation;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordanimation = transform.GetChild(1).GetComponent<Animator>();
    }

   
    public void Move(float move)
    {
      _anim.SetFloat("Move",Mathf.Abs(move));
    }

   public void Jump(bool jumping)
   {
    _anim.SetBool("JUMPING",jumping);
   }

   public void Attack()
   {
    _anim.SetTrigger("Attack");
    _swordanimation.SetTrigger("SwordAnimation");
   }

   public void hit()
   {
    _anim.SetTrigger("hit");
   }

   public void Death()
   {
    _anim.SetTrigger("Death");
   }
}
