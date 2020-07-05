using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public GameObject greenWinAnimation;
    public GameObject tanWinAnimation;

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
        if (this.gameObject.tag == "Green") {
            Debug.Log("Tan WINS!");

        } else {
            Debug.Log("Green WINS!");
        }
        GameStateController.instance.ResetGame();        
    }
}
