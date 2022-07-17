using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSoundController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clips;
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
    }

    public AudioClip GetClip() {
        int index = lastIndex;
        while (index == lastIndex) {
            index = Random.Range(0, clips.Length);
        }
        lastIndex = index;
        return clips[index];
    }

    public void Trigger() {
        // Debug.Log("Trigger Dice Sound");
        AudioClip clip = GetClip();
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }
}
