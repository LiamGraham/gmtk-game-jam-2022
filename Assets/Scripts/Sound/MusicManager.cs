using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager: MonoBehaviour
{
    public AudioClip levelMusic;
    public AudioClip menuMusic;
    [Range(0, 1)]
    public float volume = 0.5f;
    private AudioSource audioSource;
    private AudioClip currentClip = null;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    // Start is called before the first frame update
    void Start()
    {
       LevelEventManager.LevelStarted?.AddListener(() => Set(Music.LEVEL));
    }

    void Set(Music music) {
        AudioClip clip = null;
        switch (music) {
            case Music.LEVEL:
                clip = levelMusic;
                break;
            case Music.MENU:
                clip = menuMusic;
                break;
            default:
                Debug.LogError("No matching music");
                return; 
        }
        audioSource.clip = clip;
        Debug.Log("Music set to " + music.ToString() + " : " + audioSource.clip);
        Play();
    }


    void Play() {
        if (audioSource.clip == null) {
            Debug.LogError("Not playing. Clip is null");
            return;
        }
        if (audioSource.isPlaying && audioSource.clip == currentClip) {
            Debug.Log("Not playing. Next clip is same as current clip");
            return;
        }
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        audioSource.Play();
        currentClip = audioSource.clip;
        Debug.Log("Playing " + audioSource.clip);
    }

    void Pause() {
        if (audioSource.clip == null || !audioSource.isPlaying) {
            return;
        }
        audioSource.Pause();
        Debug.Log("Paused " + audioSource.clip);
    }

    void Resume() {
        if (audioSource.clip == null || audioSource.isPlaying) {
            return;
        }
        audioSource.UnPause();
        Debug.Log("Resumed " + audioSource.clip);
    }

    void FadeUp() {

    }
    
    void FadeDown() {

    }

    void OnGUI()
    {
        if (GUI.changed)
        {
            audioSource.volume = volume;
        }
    }
}
