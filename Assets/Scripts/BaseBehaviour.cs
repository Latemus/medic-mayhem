using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public GameObject WinAnimation;
    public GameObject Score;

    void OnDestroy()
    {   
        if (WinAnimation != null)
        {
            WinAnimation.GetComponent<WinTextAnimation>().victory = true;
            int winning_team; 
            if (this.gameObject.CompareTag("Green"))
            {
                winning_team = 1;
            }
            else 
            {
                winning_team = 0;
            }
            GameStateController.instance.ResetGame(winning_team);
        }
    }
}
