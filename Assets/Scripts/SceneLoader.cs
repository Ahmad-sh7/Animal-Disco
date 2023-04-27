using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        LoadMainScene();
    }

    void LoadMainScene()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("SampleScene");
        //if (Input.GetKeyDown(KeyCode.W))
        //    SceneManager.LoadScene("SampleScene");
        //if (Input.GetKeyDown(KeyCode.S))
        //    SceneManager.LoadScene("SampleScene");
        //if (Input.GetKeyDown(KeyCode.D))
        //    SceneManager.LoadScene("SampleScene");
        //if (Input.GetKeyDown(KeyCode.A))
        //    SceneManager.LoadScene("SampleScene");
    }
}
