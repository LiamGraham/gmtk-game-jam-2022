using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // public GameObject Player;
    public Vector3 Offset = new Vector3(0, 0.5f, 0);
    public float MinDistance = 3f;
    public float InterpolationSpeed = 2f;
    public bool EnableSmooth = true;
    public float lookSense = 1f;
    public float maxZoom = 5f;
    public float minZoom = 0.5f;
    private PlayerDice playerDice;
    CameraState cameraState = CameraState.Follow;

    void Start() {
        playerDice = PlayerDice.Instance;
    }

    void LateUpdate()
    {
        if (cameraState == CameraState.Follow) {
            PositionFollowCamera();
        } else if (cameraState == CameraState.Free) {
            PositionFreeCamera();
        }
    }

    private void PositionFollowCamera()
    {
        float interpolationRatio = Mathf.Min(InterpolationSpeed * Time.deltaTime, 1);
        //current player direction
        var playerDirection = playerDice.WorldCenterOfMass - transform.position;
        //goal player direction
        playerDirection.y = 0;
        playerDirection = (playerDirection.normalized) * MinDistance;
        var goalPlayerDirection = playerDirection - Offset;
        //goal rotation
        var goalRotation = Quaternion.LookRotation(goalPlayerDirection, Vector3.up);
        //goal position
        var goalPosition = playerDice.WorldCenterOfMass - goalPlayerDirection;

        // Here we use linear interpolation for smooth camera movement
        transform.position = EnableSmooth ? Vector3.Lerp(transform.position, goalPosition, interpolationRatio) : goalPosition;
        transform.rotation = EnableSmooth ? Quaternion.Lerp(transform.rotation, goalRotation, interpolationRatio) : goalRotation;
    }

    private void PositionFreeCamera() {
        var distanceToTarget = (playerDice.WorldCenterOfMass - transform.position).magnitude;
        //rotate around y axis axis
        var rotateHorizontal = Input.GetAxis("Mouse X");
        transform.RotateAround(playerDice.WorldCenterOfMass, Vector3.up, rotateHorizontal);
        //rotate around x axis
        var rotateVertical = Input.GetAxis("Mouse Y");
        transform.RotateAround(playerDice.WorldCenterOfMass, Vector3.left, rotateVertical);
        //handle zoom
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        distanceToTarget = Mathf.Clamp(distanceToTarget * (1 - scroll), minZoom, maxZoom);
        //do zoom
        this.transform.position = playerDice.WorldCenterOfMass;

        transform.Rotate(Vector3.left, rotateVertical);
        transform.Rotate(Vector3.up, rotateHorizontal, Space.World);
        transform.Translate(new Vector3(0,0,-distanceToTarget));
        transform.LookAt(playerDice.WorldCenterOfMass);
    }

    public void SetFreeCamera() {
        cameraState = CameraState.Free;
    }

    public void SetFollowCamera() {
        cameraState = CameraState.Follow;
    }

}
