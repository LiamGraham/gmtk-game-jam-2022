using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSoundController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] bounceSounds;
    public AudioClip strikeSound;
    int lastIndex = 0;
    private static DiceSoundController _instance;
    public static DiceSoundController Instance { get { return _instance; } }
    // Start is called before the first frame update
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
        DiceCollisionManager.Collided?.AddListener(PlayBounce);
        LevelEventManager.PlayerShot?.AddListener(PlayStrike);
    }

    public AudioClip GetBounceSound() {
        if (bounceSounds == null || bounceSounds.Length == 0) {
            return null;
        }

        int index = lastIndex;
        while (index == lastIndex) {
            index = Random.Range(0, bounceSounds.Length);
        }
        lastIndex = index;
        return bounceSounds[index];
    }

    public void PlayBounce() {
        AudioClip clip = GetBounceSound();
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayStrike() {
        audioSource.PlayOneShot(strikeSound);
    }
}
