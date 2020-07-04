    using UnityEngine;
    using System.Collections;
     
    public static class AudioFadeOut 
    { 
        // Fade out
        public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) 
        {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0) 
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Stop ();
            audioSource.volume = startVolume;
        }

        // Fade in
        public static IEnumerator FadeIn (AudioSource audioSource, float FadeTime) 
        {
            float startVolume = audioSource.volume;
            audioSource.Play();
            audioSource.volume = 0;
            while (audioSource.volume <= 0.99f) 
            {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }     

        }
    }
     
