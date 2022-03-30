using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow Player Camera
    [SerializeField] private Transform player;
    public float[] firstStop;
    public float[] secondStop;
    public float[] shooop;
    public Vector2 stage2PlayerPos;
    public int currentStage;

    public GameObject[] backStopper;
    void Update()
    {
       
        if (transform.position.x >= firstStop[currentStage])
        {
            
            transform.position = new Vector3(firstStop[currentStage], 0.18f, -10f);
        }
        else
        {
            // When player Reach The End of Room Stop Camera There
            if (player.position.x < shooop[currentStage])
            {
                transform.position = new Vector3(secondStop[currentStage], 0.18f, -10f);
            }
            else
            {
                transform.position = new Vector3(player.position.x, 0.18f, -10f);

            }

        }

      
    }

   
    public void IncreaseStage()
    {
        currentStage++;
        backStopper[currentStage].SetActive(true);
        if (currentStage == 2)
        {
            player.transform.position = stage2PlayerPos;
        }
    }
}

