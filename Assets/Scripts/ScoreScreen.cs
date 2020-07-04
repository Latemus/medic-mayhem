using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Start()
    {
        GameObject scoreAmount = GameObject.Find("ScoreAmount");
        GameObject scoreNumber = GameObject.Find("ScoreAmount");

        scoreNumber = scoreAmount;
    }
}
