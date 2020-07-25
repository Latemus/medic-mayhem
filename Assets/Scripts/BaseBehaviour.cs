using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public GameObject WinAnimation;
    public GameObject Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        WinAnimation.GetComponent<WinTextAnimation>().victory = true; 
        GameStateController.instance.ResetGame();        
    }
}
