using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float xOne;
    public float xTwo;
    public float speed;
    public float toGo;
    bool isAttacking = false;
    SpriteRenderer sr;
    Animator anim;
    public int myHits;
    float AttackDelay;

    public RuntimeAnimatorController sl;
    bool iAmonDragon;
    Vector2 whereIsPlayer;
    public float DelayToGoUp;
    public Rigidbody2D rb;
    public bool iHaveToMove;
    // Start is called before the first frame update
    void Start()
    {
        iAmonDragon = false;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iHaveToMove == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(whereIsPlayer.x, transform.position.y), 2 * Time.deltaTime);
        }
        if (myHits <= 6)
        {
            anim.runtimeAnimatorController = sl;
            iAmonDragon = true;
        }
        if (iAmonDragon == true)
        {
            DelayToGoUp += Time.deltaTime;
            if (DelayToGoUp >= 3)
            {
                JumpApAndAttack();
                DelayToGoUp = 0;
            }
        }    
        AttackDelay += Time.deltaTime;
        if (isAttacking == false)
        {
            anim.SetTrigger("back");

            if (iHaveToMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(toGo, transform.position.y), Time.deltaTime * speed);
                //transform.Translate(new Vector2(toGo, 0) * Time.deltaTime * speed);
                if (transform.position.x == toGo)
                {
                    UpdatePortal();
                }
            }
          
        }
        else
        {
            if (AttackDelay >= 1f)
            {
                anim.SetTrigger("Attack");

                AttackDelay = 0;
            }
        }
        if (myHits <= 0)
        {
            FlyingCoinManager.instance.SpawnAFlyingCoin(gameObject);
            Destroy(gameObject);
        }
        if (rb.velocity.magnitude < 0.01)
        {
            iHaveToMove = true;
        }
    }
    public void LoseHealthPlayer()
    {
        if (isAttacking == true)
        {
            PlayerMovement.Instance.Health -= 5;

        }
    }
    public void UpdatePortal()
    {
        if (toGo == xOne)
        {
            toGo = xTwo;
            sr.flipX = true;
        }
        else
        {
            toGo = xOne;
            sr.flipX = false;

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerMovement.Instance.Health -= 5;
            isAttacking = true;
        }
        
        if (col.gameObject.tag == "ground" && col.gameObject.tag == "Player")
        {
            iHaveToMove = true;
        }

    }
    public void JumpApAndAttack()
    {
        whereIsPlayer = PlayerMovement.Instance.gameObject.transform.position;
        iHaveToMove = false;
        anim.SetTrigger("JumpUp");
        rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * 4, ForceMode2D.Impulse);
        

    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isAttacking = false;
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerAttack")
        {
            myHits--;

            anim.SetTrigger("GotHit");

        }

        if (col.gameObject.tag == "HighDMG")
        {
            PlayerMovement.Instance.Health -= 10;


        }

    }
}
