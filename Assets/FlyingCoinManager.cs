using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoinManager : MonoBehaviour
{
    public static FlyingCoinManager instance;
    public GameObject flyingCoin;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SpawnAFlyingCoin(GameObject forMe)
    {
        GameObject g = Instantiate(flyingCoin, forMe.transform.position, Quaternion.identity);
        Destroy(forMe);
    }
}
