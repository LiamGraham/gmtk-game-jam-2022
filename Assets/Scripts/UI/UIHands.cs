using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHands : MonoBehaviour
{
    public Vector3 hoverOffset = new Vector3(0, 0, 1f);
    public GameObject playerCamera;
    PlayerDice playerDice;
    Animator animator;

    public AudioClip cyclingSound;
    public AudioClip completeSound;
    private AudioSource audioSource;
    private bool silenced = false;

    // Start is called before the first frame update
    void Start()
    {
        playerDice = PlayerDice.Instance;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        LevelEventManager.PlayerShot?.AddListener(onDiceHit);
        LevelEventManager.PlayerDied?.AddListener(OnPlayerDeath);
        playerDice.OnPlayerStationary?.AddListener(onDiceResult);
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
    }

    void onDiceHit() {
        animator.SetInteger("result", 0);
        silenced = false;
        PlayCycle();
    }

    void onDiceResult(int result) {
        animator.SetInteger("result", result);
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

    void OnPlayerDeath() {
        audioSource.Stop();
        silenced = true;
    }
}
