using UnityEngine;
using System.Collections;

public class arguing_parents : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    float fade_time = 30.0f;
    float silent_perdiod_max = 120.0f;

    void Start()
    {
        StartCoroutine(ambient_sound_loop());
    }

    IEnumerator ambient_sound_loop() 
    {
        // Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        // Start the music at random time
        m_MyAudioSource.time = Random.Range(0.0f, m_MyAudioSource.clip.length);

        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(10.0f, silent_perdiod_max));
            StartCoroutine (AudioFadeOut.FadeIn (m_MyAudioSource, fade_time));
            yield return new WaitForSeconds(fade_time); 
            StartCoroutine (AudioFadeOut.FadeOut (m_MyAudioSource, fade_time));
            yield return new WaitForSeconds(Random.Range(10.0f, silent_perdiod_max));
        }
    }
}