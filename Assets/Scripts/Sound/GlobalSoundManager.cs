using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manager for non-spatial audio.
 */
public class GlobalSoundManager : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip mainGoalSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LevelEventManager.PlayerDied?.AddListener(() => Play(SoundEffect.DEATH));
        LevelEventManager.GoalAchieved?.AddListener((GoalType goalType, int score) => Play(SoundEffect.MAIN_GOAL));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FadeAmbience() {

    }

    public void Play(SoundEffect sound) {
        Debug.Log("Playing " + sound.ToString() + " sound");
        AudioClip clip = null;
        switch (sound) {
            case SoundEffect.DEATH:
                clip = deathSound; 
                break;
            case SoundEffect.MAIN_GOAL:
                clip = mainGoalSound; 
                break;
            default:
                Debug.LogError("No matching sound");
                return;
        }

        if (clip != null) {
            audioSource.PlayOneShot(clip);
        } else {
            Debug.LogError(sound.ToString() + " sound has not been set");
        }
    }
}