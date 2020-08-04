using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(int players_per_team)
    {
        NumberOfPlayers.instance.number_of_players_per_team = players_per_team;
        SceneManager.LoadScene("GameArena");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
