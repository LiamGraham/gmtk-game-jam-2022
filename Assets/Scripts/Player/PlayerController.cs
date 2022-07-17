using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //reference to shotIndicator PREFAB
    public GameObject shotIndicatorPrefab;
    //reference to playerDice INSTANCE
    public PlayerDice playerDice;
    //reference to playerCamera INSTANCE
    public FollowPlayer playerCamera;
    //max angle the arrow can point up
    public float maxYTrajectory = 50f;
    public float aimBoxWidth = 0.4f;
    public float aimBoxHeight = 0.4f;
    public float maxXTrajectory = 50f;
    //impulse member variables
    public float maxImpulse = 20f;
    public float defaultTorque = 30f;

    /// <summary>
    /// The current state of the player controller
    /// </summary>
    public PlayerState State { get; private set; }

    /// <summary>
    /// The location of the player from the last time it was fired
    /// </summary>
    public Vector3 PreviousPlayerPosition { get; private set; }

    /// <summary>
    /// The rotation of the player from the last time it was fired
    /// </summary>
    public Quaternion PreviousPlayerRotation { get; private set; }

    //referance to shotIndicator INSTANCE
    ShotIndicator shotIndicator;

    void Start()
    {
        State = PlayerState.Aiming;

        SetPreviousPlayerPosition();

        // Add event listeners
        playerDice.OnPlayerStationary?.AddListener(OnPlayerStationary);
        LevelEventManager.PlayerDied?.AddListener(OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        State = PlayerState.Aiming;

        // Return to previous location
        playerDice.ResetToPosition(PreviousPlayerPosition, PreviousPlayerRotation);
    }

    //Update is called once per frame
    void Update()
    {
        if (State == PlayerState.Inactive)
        {
            //do nothing
        }
        else if (State == PlayerState.Aiming)
        {
            AimPlayer();
        }
    }
    private void OnPlayerStationary(int result)
    {
        Debug.Log(result.ToString());
        // If we're flying and now stationary, chage state to aiming
        if (State == PlayerState.Flying)
        {
            State = PlayerState.Aiming;

            SetPreviousPlayerPosition();
        }
    }

    private void AimPlayer()
    {
        //do aiming stuff
        if (Input.GetButton("Fire2"))
        {
            //right mouse pressed, rotating changes camera
            DestroyShotIndicator();
            playerCamera.SetFreeCamera();
            return;
        }

        playerCamera.SetFollowCamera();

        //right mouse not pressed, rotating changes arrow position
        if (shotIndicator == null)
        {
            var position = playerDice.WorldCenterOfMass;
            shotIndicator = (Instantiate(shotIndicatorPrefab, position, Quaternion.identity)).GetComponent<ShotIndicator>();
        }

        var direction = GetDirection();
        shotIndicator.setDirection(direction);

        //check if fire button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            //FIRE
            State = PlayerState.Flying;
            LevelEventManager.PlayerShot?.Invoke();
            DestroyShotIndicator();
            CreateDiceImpulse();
        }
    }

    private void DestroyShotIndicator()
    {
        if (shotIndicator != null)
        {
            Destroy(shotIndicator.gameObject);
            shotIndicator = null;
        }
    }

    private Vector3 GetDirection()
    {
        //get camera direction in x/z plane
        var direction = (playerDice.transform.position - playerCamera.transform.position);
        direction = (new Vector3(direction.x, 0, direction.z)).normalized;
        var mousePosition = Input.mousePosition;
        //calculate y axis rotation
        var xPos = Mathf.Max((0.5f - aimBoxWidth) * Screen.width, mousePosition.x);
        xPos = Mathf.Min((0.5f + aimBoxWidth) * Screen.width, xPos);
        var yRotation = ((0.5f * Screen.width - xPos) / (aimBoxWidth * Screen.width)) * maxXTrajectory;
        //calculate x axis rotation
        var yPos = Mathf.Max((0.5f - aimBoxHeight) * Screen.height, mousePosition.y);
        yPos = Mathf.Min((0.5f + aimBoxHeight) * Screen.height, yPos);
        var xRotation = ((yPos - ((0.5f - aimBoxHeight) * Screen.height)) / (aimBoxHeight * 2 * Screen.height)) * maxYTrajectory;

        //calculate rotation
        return direction = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(-xRotation, 0, 0) * Quaternion.Euler(0, -yRotation, 0) * Vector3.forward;
    }

    private float GetMagnitude()
    {
        //temporary
        return 1.0f * maxImpulse;
    }

    private void CreateDiceImpulse()
    {
        //get impulse amount
        playerDice.AddForce(GetDirection() * GetMagnitude());
        // playerDice.addTorque((new Vector3(1,0,0) * ));
    }

    private void SetPreviousPlayerPosition()
    {
        PreviousPlayerPosition = playerDice.transform.position;
        PreviousPlayerRotation = playerDice.transform.rotation;
    }
}
