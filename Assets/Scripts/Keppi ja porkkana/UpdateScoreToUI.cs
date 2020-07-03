using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateScoreToUI : MonoBehaviour
{
    public Text scoreAmount;
    void Update()
    {
        scoreAmount.text = ScoreCounter.scoreValue.ToString();
    }
    
}
