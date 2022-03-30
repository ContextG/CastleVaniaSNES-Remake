using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   [SerializeField] public float attackCooldown;
   [SerializeField] public Transform firePoint;
   [SerializeField] public GameObject[] Knives;
     
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        Knives[0].transform.position = firePoint.position;
        Knives[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindKnife()
    {
        for (int i = 0; i <Knives.Length; i++)
        {
            if (Knives[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
   

}