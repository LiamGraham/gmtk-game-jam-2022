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

    private static GlobalSoundManager _instance;
    public static GlobalSoundManager Instance { get { return _instance; } }
    void Awake()
    {
        if (_instance == null) {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AddSoundEvents();
    }

    void AddSoundEvents() {
        LevelEventManager.GoalAchieved?.AddListener((GoalType _, int __) => Play(SoundEffect.MAIN_GOAL));
        LevelEventManager.PlayerDied?.AddListener(() => Play(SoundEffect.DEATH));
    }

    // Update is called once per frame
    void Update()
    {
        
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