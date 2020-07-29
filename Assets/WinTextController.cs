using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTextController : MonoBehaviour
{
    public GameObject green_win_text;
    public GameObject tan_win_text;
    public GameObject[] score_texts;

    private bool game_has_ended = false;

    // GameEnd triggers the winner and score txt elements 0 = green, 1 = tan
    public void GameEnd(int who_won)
    {
        if(!game_has_ended)
        {
            game_has_ended = true;
            score_texts[0].GetComponent<TextMeshPro>().text = GameStateController.instance.number_of_green_wins.ToString();
            score_texts[2].GetComponent<TextMeshPro>().text = GameStateController.instance.number_of_tan_wins.ToString();

            // trigger the win text 
            if (who_won == 0)
            {
                green_win_text.GetComponent<WinTextAnimation>().victory = true;
            }
            else
            {
                tan_win_text.GetComponent<WinTextAnimation>().victory = true;
            }

            // trigger the scores
            StartCoroutine(ActivateScoreAnimations()); 
        }
    }

    private IEnumerator ActivateScoreAnimations() 
    {
        foreach(GameObject text_element in score_texts)
        {
            yield return new WaitForSeconds(1);
            text_element.GetComponent<WinTextAnimation>().victory = true;
        }
    } 
}
