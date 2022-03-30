using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candles : MonoBehaviour
{
    public GameObject[] weSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerAttack")
        {
            Instantiate(weSpawn[Random.Range(0, weSpawn.Length)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
