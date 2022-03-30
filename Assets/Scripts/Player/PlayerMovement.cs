using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public float speed;
    public float jumpPower;
    public Projectile ProjectilePrefab;
    public Transform LaunchOffset;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public GameObject gameOver;
    public float Health;

    public CameraController cc;
    public Slider HealthDisplay;
    float maxHealth;
    public GameObject attackTell;
    bool OnGround;
    public Text score;
    public float scoreMine;float AttackDelay;
    public bool iAttacked;
    float Reset;
    void Start()
    {
        Instance = this;
        // reference grabbing 
        maxHealth = Health;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        OnGround = false;
 
    }
    public void mainAttack()
    {
        anim.ResetTrigger("mainAttack");
        anim.ResetTrigger("mainAttackJump");
        iAttacked = true;
        if (OnGround == true)
        {
            anim.SetTrigger("mainAttack");

        }
        else
        {
            anim.SetTrigger("mainAttackJump");
        }
    }
    public void Setoff()
    {
        attackTell.SetActive(false);
    } public void Seton()
    {
        attackTell.SetActive(true);
    }
    void Update()
    {
        Reset += Time.deltaTime;
        if (Reset >= 0.3f)
        {
            iAttacked = false;
            Reset = 0;
        }
        AttackDelay += Time.deltaTime;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        score.text = "Score : " + scoreMine;
        if (Input.GetMouseButton(0) && AttackDelay >= 0.5f)
        {
            mainAttack();
            AttackDelay = 0;
        }
        if (Health <= 0)
        {
            gameOver.SetActive(true);
        }
        HealthDisplay.value = Health / maxHealth;
         horizontalInput = Input.GetAxis("Horizontal");
     

        // Flip player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.85f, 0.85f, 1);


        else if (horizontalInput < -0.01f)
            transform.localScale = new  Vector3(-0.85f, 0.85f, 1);

      

        // Animator parameters
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Wall Jump Logic
        if (wallJumpCooldown > 0.2f)
        {
      
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {

                body.gravityScale = 3;
                body.velocity = Vector2.zero;

            }
            else
                body.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (OnGround == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            Debug.Log("Jumping");
            anim.SetTrigger("jump");
            anim.ResetTrigger("back");
            OnGround = false;
        }
       else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 7, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 2);
            wallJumpCooldown = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 2);
        }
       
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            OnGround = true;
            anim.SetTrigger("back");
        }
        if (col.gameObject.tag == "Hurt")
        {
            
            Health -= col.gameObject.GetComponent<Hurter>().howMuchIHurt;
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag == "ScoreIncrease")
        {
            scoreMine += col.gameObject.GetComponent<Hurter>().howMuchIHurt;
            Destroy(col.gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Door")
        {
            cc.IncreaseStage();
            Destroy(col.gameObject);
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "Coin")
        {
            scoreMine += 80;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "nmbullet")
        {
            Health -= 40;
        }
    }

    // Box Casting
    private bool isGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider !=null;

    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}
