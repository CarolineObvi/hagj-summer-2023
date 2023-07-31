using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Act_0_Church");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
