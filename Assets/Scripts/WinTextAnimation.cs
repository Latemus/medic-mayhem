using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTextAnimation : MonoBehaviour
{
    public Vector3 start_location = new Vector3(0,40,0);
    public Vector3 end_location = new Vector3(0,10,0);
    private AudioSource audioSource;
    private bool have_already_played = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //TriggerWin();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, end_location, .3f);
        Vector3 distance_vector = transform.position - end_location;
        if (distance_vector.sqrMagnitude < 1 && !audioSource.isPlaying && !have_already_played) 
        {
            have_already_played = true;
            audioSource.Play();
        }
    }
}
