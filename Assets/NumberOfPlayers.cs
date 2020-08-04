using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfPlayers : MonoBehaviour
{
    public static NumberOfPlayers instance;
    public int number_of_players_per_team = 1;

    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
