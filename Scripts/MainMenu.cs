using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject cubeSat;

    public void StartBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    void Update()
    {
        cubeSat.transform.Rotate(0f, -.5f, 0f);
    }
}
