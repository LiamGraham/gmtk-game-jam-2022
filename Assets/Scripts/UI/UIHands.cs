using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHands : MonoBehaviour
{
    public Vector3 hoverOffset = new Vector3(0, 0, 1f);
    public GameObject playerCamera;
    public UiNames namesObject;
    PlayerDice playerDice;
    Animator animator;

    public AudioClip cyclingSound;
    public AudioClip completeSound;
    private AudioSource audioSource;
    private bool silenced = false;

    //amplitude stuff
    private float updateStep = 0.01f;
    private float maxLoudness = 0.0729f;
    private int sampleDataLength = 512;
    private float currentUpdateTime;
    private int currentResult;
    private float[] clipSampleData;
    private float clipLoudness;
    private bool justStoppedPlaying;
    private float maxMeasuredLoudness;
    
    //material
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        maxMeasuredLoudness = 0f;
        material = GetComponent<SpriteRenderer>().material;
        playerDice = PlayerDice.Instance;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        LevelEventManager.PlayerShot?.AddListener(onDiceHit);
        LevelEventManager.PlayerDied?.AddListener(() => StopAudio(true));
        LevelEventManager.LevelEnded?.AddListener(() => StopAudio(false));
        playerDice.OnPlayerStationary?.AddListener(onDiceResult);

        InitializeVolumeVars();
    }

    void InitializeVolumeVars() {
        clipSampleData = new float[sampleDataLength];
        currentUpdateTime = 0f;
        justStoppedPlaying = false;
        clipLoudness = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //update position to player position
        transform.position = playerDice.WorldCenterOfMass + hoverOffset;
        
        //update rotation
        var direction = playerCamera.transform.position - playerDice.WorldCenterOfMass;
        direction = (new Vector3(direction.x, 0, direction.z)).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        //check if audio is playing and adjust amplitude
        if (audioSource.isPlaying) {
            //audio is playing
            justStoppedPlaying = true;
            currentUpdateTime += Time.deltaTime;
            if (currentUpdateTime >= updateStep) {
                currentUpdateTime = 0f;
                var currentClip = completeSound;
                if (currentResult == 0) {
                    currentClip = cyclingSound;
                }
                currentClip.GetData(clipSampleData, audioSource.timeSamples);
                clipLoudness = 0f;
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength;
                maxMeasuredLoudness = Mathf.Max(maxMeasuredLoudness, clipLoudness);
                setAmplitude();
            }
        }
        else if (justStoppedPlaying)
        {
            InitializeVolumeVars(); 
            setAmplitude();
        }
    }

    void setAmplitude()
    {
        //set amplitude in texture and send off info so that names can set amplitude
        var intensity = Mathf.Clamp(clipLoudness / maxLoudness, 0.2f, 1f);
        Debug.Log(intensity.ToString());
        Debug.Log("Max Measured Loudness: ");
        Debug.Log(maxMeasuredLoudness.ToString());
        material.SetFloat("_intensity",intensity);
        namesObject.setAmplitude(intensity);
    }

    void onDiceHit() {
        animator.SetInteger("result", 0);
        currentResult = 0;
        silenced = false;
        PlayCycle();
    }

    void onDiceResult(int result) {
        animator.SetInteger("result", result);
        currentResult = result;
        PlayComplete();
    }

    void PlayCycle() {
        audioSource.clip = cyclingSound;
        audioSource.Play();
    }

    void PlayComplete() {
        audioSource.Stop();

        if (!silenced) {
            audioSource.clip = completeSound;
            audioSource.Play();
        }
    }

    void StopAudio(bool silence) {
        audioSource.Stop();
        silenced = silence;
    }
}
