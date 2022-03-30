using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sekeleton : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackDelay += Time.deltaTime;
        if (isAttacking == false)
        {
            anim.SetTrigger("back");

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(toGo, transform.position.y), Time.deltaTime * speed);
            //transform.Translate(new Vector2(toGo, 0) * Time.deltaTime * speed);
            if (transform.position.x == toGo)
            {
                UpdatePortal();
            }
        }
        else
        {
            if (AttackDelay >= 0.5f)
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
    }
    public void LoseHealthPlayer()
    {
        if (isAttacking == true)
        {
            PlayerMovement.Instance.Health -= 40;
            if (PlayerMovement.Instance.iAttacked)
            {
                myHits--;
            }
        }
    }
    public void UpdatePortal()
    {
        if (toGo == xOne)
        {
            toGo = xTwo;
            sr.flipX = true;
        }else
        {
            toGo = xOne;
            sr.flipX = false;

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isAttacking = true;
        }
       
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
            Debug.Log("GOTA");
            myHits--;

            anim.SetTrigger("GotHit");

        }

    }
}
