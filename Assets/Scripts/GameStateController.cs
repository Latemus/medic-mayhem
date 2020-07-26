using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;
    public static int number_of_green_wins; 
    public static int number_of_tan_wins;
    public int green_wins;
    public int tan_wins;

    #if UNITY_EDITOR
    void Update()
    {
        green_wins = number_of_green_wins;
        tan_wins = number_of_tan_wins;
    }
    #endif


    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null) 
        {
            instance = this;
            number_of_green_wins = 0;
            number_of_tan_wins = 0;
        }
        else
        {
            Destroy(this);
        }
    }

    // who_won variable tells which team won, 0 for green, 1 for tan
    public void ResetGame(int who_won) 
    {
        if (who_won == 0)
        {
            number_of_green_wins += 1;
        }
        else
        {
            number_of_tan_wins += 1;
        }
        StartCoroutine(WaitAndReset()); 
    }

    public IEnumerator WaitAndReset() 
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}
