using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manager for non-spatial audio.
 */
public class GlobalSoundManager : MonoBehaviour
{
    public AudioClip ambienceSound;
    public AudioClip deathSound;
    public AudioClip mainGoalSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LevelEventManager.PlayerDied?.AddListener(() => Play(SoundEffect.DEATH));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeAmbience() {

    }

    public void Play(SoundEffect sound) {
        AudioClip clip = null;
        switch (sound) {
            case SoundEffect.DEATH:
                clip = deathSound; 
                break;
            case SoundEffect.MAIN_GOAL:
                clip = mainGoalSound; 
                break;
            default:
                break;
        }

        if (clip != null) {
            audioSource.PlayOneShot(clip);
        } else {
            Debug.LogError(sound.ToString() + " sound has not been set");
        }
    }
}