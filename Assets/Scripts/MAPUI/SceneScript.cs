using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void LoadSimulator()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGUI()
    {
        SceneManager.LoadScene(0);
    }
}
