using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;
    public GameStateController gamestatecontroller;
    public int number_of_green_wins; 
    public int number_of_tan_wins;
    public GameObject win_texts;

    #if UNITY_EDITOR
    void Update()
    {
        // gamestatecontroller = instance;
    }
    #endif


    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this);
            number_of_green_wins = 0;
            number_of_tan_wins = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        win_texts = GameObject.Find("WinTexts");
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
        win_texts = GameObject.Find("WinTexts");
        win_texts.GetComponent<WinTextController>().GameEnd(who_won);
        StartCoroutine(WaitAndReset()); 
    }

    public IEnumerator WaitAndReset() 
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
}
