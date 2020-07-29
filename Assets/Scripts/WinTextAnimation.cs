﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextAnimation : MonoBehaviour
{
    private Vector3 end_location;
    private AudioSource audioSource;
    private bool have_already_played = false;
    public bool victory = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        end_location = transform.position - new Vector3(0, 25, 0);
        //TriggerWin();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (victory) 
        {
            transform.position = Vector3.MoveTowards(transform.position, end_location, .3f);
            Vector3 distance_vector = transform.position - end_location;
            if (distance_vector.sqrMagnitude < 0.1 && !audioSource.isPlaying && !have_already_played) 
            {
                have_already_played = true;
                audioSource.Play();
            }
        }
    }
}
