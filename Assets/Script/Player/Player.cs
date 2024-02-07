using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageble
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpforce = 5.0f;

    private bool _resetJump = false; 
    [SerializeField]
    private float _speed = 5.0f;
    private PlayerAnimation  _PlayerAnim;
  
  private SpriteRenderer _PlayerSprite, _SwordArcSprite;
  [SerializeField]
  private bool _grounded = false;
  public int diamonds;
  public int Health { get; set;}
  public GameController gamecontroller;
  [SerializeField] private AudioSource jumpsound;
   [SerializeField] private AudioSource hitsound;
    [SerializeField] private AudioSource Deathsound;
     [SerializeField] private AudioSource Diamondsound;
     [SerializeField] private AudioSource swordsound;
 
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _PlayerAnim = GetComponent<PlayerAnimation>();
        _PlayerSprite = GetComponentInChildren<SpriteRenderer>();
        _SwordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    
    void Update()
    {
      
        Movement();

        if(Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _PlayerAnim.Attack();
            swordsound.Play();
        }

    }

    void Movement()
    {
         float move = Input.GetAxisRaw("Horizontal");
         _grounded = IsGrounded();

         if(move > 0)
         {
            Flip(true);
         }
         else if(move < 0)
         {
            Flip(false);
         }


        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpRoutine());
            _PlayerAnim.Jump(true);
            jumpsound.Play();
        }
        
        _rigid.velocity = new Vector2(move * _speed , _rigid.velocity.y);

        _PlayerAnim.Move(move);
    }
    bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 6);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if(hitinfo.collider != null)
        {
            if(_resetJump == false)
            {
                _PlayerAnim.Jump(false);
                return true;
                
            }
        }

        return false;
    }

    void Flip(bool faceright)
    {
        if(faceright)
        {
            _PlayerSprite.flipX = false;
            _SwordArcSprite.flipX = false;
            _SwordArcSprite.flipY = false;

            Vector3 newPos = _SwordArcSprite.transform.localPosition;
            newPos.x = 0.47f;
            _SwordArcSprite.transform.localPosition = newPos;


        }
        else
        {
            _PlayerSprite.flipX = true;
       // _SwordArcSprite.flipX = true;
       _SwordArcSprite.flipY = true;

        Vector3 newPos = _SwordArcSprite.transform.localPosition;
        newPos.x = 0.16f;
        newPos.y = 0.45f;
        _SwordArcSprite.transform.localPosition = newPos;

        // Set rotation for facing left
       // _SwordArcSprite.transform.rotation = Quaternion.Euler(-10f, 200f, -260f);
        }
    }


    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }
        //Debug.Log("Player Damager()");
        Health--;
        if(Health >= 1)
        {
            _PlayerAnim.hit();
            hitsound.Play();
        }
        
        UIManager.Instance.UpdateLives(Health);

        if(Health < 1)
        {
            _PlayerAnim.Death();
            Deathsound.Play();
            
          //  gamecontroller.GameOver();
           // Time.timeScale = 0f;
          StartCoroutine(GameOverAfterDelay(1f));
        }
    }


        private IEnumerator GameOverAfterDelay(float delay)
        {
             yield return new WaitForSeconds(delay);

                gamecontroller.GameOver();
                Time.timeScale = 0f;
        }

    
    public void AddGems(int amount)
    {
        Diamondsound.Play();
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "door")
        {
            gamecontroller.LevelComplete();
        }
    }
}
