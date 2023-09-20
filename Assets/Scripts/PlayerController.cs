using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    //Bubble Paths 2023
    public bool hasCollectable = false;



    //avoid issues if gamemaster is disabled
    [Header("Disable Checkpoints")]
    public bool checkPointsActive = true;

    public bool isActive = true;
    


    //reload panel
    public GameObject reloadPanel;

    //rewired
    [SerializeField] public int playerID = 0;
    [SerializeField] public Player player;

    //flip
    public bool facingRight;

    //Gamemaster
    //private GameMaster gm;


    //animation
    private Animator animator;
    private string currentState;

    //coyote time testin'
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

   
    //movement new guy
    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float maxMoveSpeedOffPlatform;
    [SerializeField] private float maxMoveSpeedOnMovingPlatform;
    [SerializeField] private float _linearDrag;
    private float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float movingJumpForce = 12f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float standingJumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    private bool _canJump => player.GetButtonDown("Jump") && _onGround;


   
    public bool _onGround;

    [Header("Components")]
    public Rigidbody2D _rb;


    public BoxCollider2D playerCollider;

  

    


    //audio
    public AudioSource source;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip[] footSteps;
    private AudioClip footStep;
    public AudioClip landingSound;
    public AudioClip gemCollect;
    public AudioClip restartSound;

    private bool canPlayDeathSound = true;
    private bool canPlayRestartSound = true;

    void Start()
    {
        
        //flip on same bug
        facingRight = true;

        player = ReInput.players.GetPlayer(playerID);
        _rb = GetComponent<Rigidbody2D>();
        //unfreeze player
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();

    }



    void Update()
    {

      



        //movement
        if (isActive == true)
        {
       
            

            if (player.GetButtonDown("Jump") && coyoteTimeCounter > 0f) Jump();

            //coyote time 
            if (_onGround)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
            if (player.GetButtonUp("Jump"))
            {
                coyoteTimeCounter = 0f;


            }
          

        


            _horizontalDirection = GetInput().x;
            if (player.GetButtonDown("Jump") && coyoteTimeCounter > 0f) Jump();
            //flip
            if (player.GetAxisRaw("Move Horizontal") > 0f && facingRight == false)
            {
                Flip();
            }
            if (player.GetAxisRaw("Move Horizontal") < 0f && facingRight == true)
            {
                Flip();
            }

            else if (_onGround && player.GetAxisRaw("Move Horizontal") != 0)
            {
                ChangeAnimationState("Run");
                _jumpForce = movingJumpForce;
            }


            else if (_onGround && player.GetAxisRaw("Move Horizontal") == 0)
            {
                ChangeAnimationState("Idle");
                _jumpForce = standingJumpForce;
            }


            else if (_onGround == false && _rb.velocity.y > 0f)
            {
                ChangeAnimationState("Jump");
            }
            else if (_onGround == false && _rb.velocity.y < 0f)
            {
                ChangeAnimationState("Fall");

            }
            
        }
        //restart
        if (player.GetButtonDown("Restart")){
            reloadPanel.GetComponent<AnimationHandler>().ChangeAnimationState("Load");
            DisablePlayer();
            if (canPlayRestartSound)
            {
                source.PlayOneShot(restartSound, .75f);
            }
            canPlayRestartSound = false;
        }



    }



    private void FixedUpdate()
    {
        MoveCharacter();

        if (_onGround)
        {
            ApplyLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }

    }

    private Vector2 GetInput()
    {
        return new Vector2(player.GetAxisRaw("Move Horizontal"), _rb.velocity.y);
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);
        
            if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
                _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
        

    }


    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _linearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    private void Jump()
    {
        source.PlayOneShot(jump, .05f);
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
       
        

    }

    private void FallMultiplier()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !player.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }



    //Flipping
    void Flip()
    {

        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;

    }

    public void DisablePlayer()
    {
        isActive = false;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        ChangeAnimationState("Idle");
    }

    public void EnablePlayer()
    {
        isActive = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

  



    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayFootStepSound()
    {
        footStep = footSteps[Random.Range(0, footSteps.Length)];
        source.PlayOneShot(footStep);
    }

    public void KillPlayer()
    {
        isActive = false;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        ChangeAnimationState("Death");
        reloadPanel.GetComponent<AnimationHandler>().ChangeAnimationState("Load");
        this.GetComponent<SpriteRenderer>().color = this.GetComponent<ColorLerp>().startColor;
        if (canPlayDeathSound)
        {
            source.PlayOneShot(death, 1.5f);
        }
        canPlayDeathSound = false;
        
    }

    public void ReturnToStartColor()
    {
        this.GetComponent<SpriteRenderer>().color = this.GetComponent<ColorLerp>().startColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            KillPlayer();
        }

        if (collision.gameObject.tag == "Gem")
        {
            if (!hasCollectable)
            {
                source.PlayOneShot(gemCollect, .75f);
            }
            hasCollectable = true;
        }

        if (collision.gameObject.tag == "JumpPad")
        {
            _rb.AddForce(Vector2.up * 40f, ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "LevelExit")
        {
            reloadPanel.GetComponent<AnimationHandler>().ChangeAnimationState("LoadNewLevel");
        }
    }



    //animation
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }




}
