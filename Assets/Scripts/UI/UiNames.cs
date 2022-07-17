using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiNames : MonoBehaviour
{
    public Vector3 hoverOffset = new Vector3(0, 0, 1f);
    public GameObject playerCamera;
    PlayerDice playerDice;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerDice = PlayerDice.Instance;
        animator = GetComponent<Animator>();
        LevelEventManager.PlayerShot?.AddListener(onDiceHit);
        playerDice.OnPlayerStationary?.AddListener(onDiceResult);
    }

    // Update is called once per frame
    void Update()
    {
        //update position to player position
        transform.position = playerDice.WorldCenterOfMass + hoverOffset;
        
        //update rotation
        var direction = playerDice.WorldCenterOfMass - playerCamera.transform.position;
        direction = (new Vector3(direction.x, 0, direction.z)).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up); 
    }

    void onDiceHit()
    {
        animator.SetInteger("result", 0);
    }

    void onDiceResult(int result)
    {
        animator.SetInteger("result", result);
    }
}
