using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDagger : MonoBehaviour
{
    public int myHits;
    bool startAttacking = false;
    float DelayA;
    public GameObject Bullet;
    public GameObject firePoint;
    public float attackDir;
    public float kk;
    // Start is called before the first frame update
    void Start()
    {
        if (kk == 0 )
        {
            kk = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DelayA += Time.deltaTime;
        if (myHits <= 0)
        {
            FlyingCoinManager.instance.SpawnAFlyingCoin(gameObject);
            Destroy(gameObject);
        }
        if (startAttacking == true && DelayA >= kk)
        {
            GameObject g = Instantiate(Bullet, firePoint.transform.position, Quaternion.identity);
            g.GetComponent<Projectile>().SetDirection(attackDir);
            g.gameObject.tag = "nmbullet";
            DelayA = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            startAttacking = true;

        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            startAttacking = false;

        }
        if (col.gameObject.tag == "PlayerAttack")
        {
            myHits--;

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
    }
}
