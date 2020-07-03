using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown: MonoBehaviour {

    public float targetTime = 60.0f;

    private Text countDownLabel;

    private void Start()
    {
        countDownLabel = GetComponent<Text>(); 
    }

    void Update(){

        targetTime -= Time.deltaTime;

        countDownLabel.text = System.Math.Round(targetTime, 0).ToString();

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        Debug.Log("End");
        SceneManager.LoadScene("ScoreScreen");
    }
}