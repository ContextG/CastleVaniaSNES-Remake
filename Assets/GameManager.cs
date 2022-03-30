using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScren;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScren.SetActive(true);
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseScren.SetActive(false);

    }
    public void Restart()
    {

        SceneManager.LoadScene(1);
    }
}
