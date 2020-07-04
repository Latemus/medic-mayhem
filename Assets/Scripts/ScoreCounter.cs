using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class ScoreCounter
{
    // Start is called before the first frame update

    public static Text scoreAmount;
    public static int scoreValue;

    public static int GetScoreValue()
    {
        return scoreValue;
    }

    public static void IncreaseScore(int increaseAmount)
    {
        scoreValue += increaseAmount;
        scoreAmount.text = scoreValue.ToString();
    }


}
