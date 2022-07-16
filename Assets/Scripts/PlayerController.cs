using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState {Inactive, Aiming, Flying}
    //reference to shotIndicator PREFAB
    public GameObject shotIndicatorPrefab;
    //reference to playerDice INSTANCE
    public PlayerDice playerDice;
    //reference to playerCamera INSTANCE
    public GameObject playerCamera;
    //max angle the arrow can point up
    public float maxYTrajectory = 50f;
    public float aimBoxWidth = 0.4f;
    public float aimBoxHeight = 0.4f;
    public float maxXTrajectory = 50f;
    //impulse member variables
    public float maxImpulse = 20f;
    public float defaultTorque = 30f;
    //referance to shotIndicator INSTANCE
    ShotIndicator shotIndicator;
    PlayerState playerState;

    void Start()
    {
        playerState = PlayerState.Aiming;
    }

    //Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Inactive) {
            //do nothing
        } else if (playerState == PlayerState.Aiming) {
            //do aiming stuff
            if (!Input.GetButton("Fire2")) {
                //right mouse pressed, rotating changes camera
                if (this.shotIndicator != null) {
                    Destroy(shotIndicator.gameObject);
                }
            } else {
                //right mouse not pressed, rotating changes arrow position
                if (this.shotIndicator == null) {
                    var position = playerDice.worldCenterOfMass;
                    this.shotIndicator = (Instantiate(shotIndicatorPrefab, position, Quaternion.identity)).GetComponent<ShotIndicator>();
                }
                var direction = getDirection();
                shotIndicator.setDirection(direction);
                //check if fire button is pressed
                if (Input.GetButtonDown("Fire1")) { 
                    //FIRE
                    createDiceImpulse();
                }
            }
        } else if (playerState == PlayerState.Flying) {
            //do flying stuff
        }
    }

    private Vector3 getDirection() {
        //get camera direction in x/z plane
        var direction = (this.playerDice.transform.position - this.playerCamera.transform.position);
        direction = (new Vector3(direction.x, 0, direction.z)).normalized;
        var mousePosition = Input.mousePosition;
        //calculate y axis rotation
        var xPos = Mathf.Max((0.5f - aimBoxWidth) * Screen.width, mousePosition.x);
        xPos = Mathf.Min((0.5f + aimBoxWidth) * Screen.width, xPos);
        var yRotation = ((0.5f * Screen.width - xPos)/(aimBoxWidth * Screen.width)) * maxXTrajectory;
        //calculate x axis rotation
        var yPos = Mathf.Max((0.5f - aimBoxHeight) * Screen.height, mousePosition.y);
        yPos = Mathf.Min((0.5f + aimBoxHeight) * Screen.height, yPos);
        var xRotation = ((yPos - ((0.5f - aimBoxHeight)*Screen.height))/(aimBoxHeight * 2 * Screen.height)) * maxYTrajectory;
        //calculate rotation
        Debug.Log(yRotation.ToString());
        Debug.Log(xRotation.ToString());
        return direction =  Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(-xRotation, 0, 0) * Quaternion.Euler(0, -yRotation, 0) * Vector3.forward;
    }

    private float getMagnitude() {
        //temporary
        return 1.0f * maxImpulse;
    }

    private void createDiceImpulse() {
        //get impulse amount
        playerDice.addForce(getDirection() * getMagnitude());
        // playerDice.addTorque((new Vector3(1,0,0) * ));
    }


    //this class with likely consume a bunch of signals and we probably won't need
    //a public function for this.
    public void setPlayerState(PlayerState playerState) {
        this.playerState = playerState;
    }
    
}