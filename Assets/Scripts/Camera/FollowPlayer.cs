using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Offset = new Vector3(0, 0.5f, 0);
    public float MinDistance = 3f;
    public float InterpolationSpeed = 2f;
    public bool EnableSmooth = true;
    public float lookSense = 1f;
    CameraState cameraState = CameraState.Follow;

    void LateUpdate()
    {
        if (cameraState == CameraState.Follow) {
            PositionFollowCamera();
            return;
        }

        if (cameraState == CameraState.Free) {
            PositionFreeCamera();
        }
    }

    private void PositionFollowCamera()
    {
        float interpolationRatio = Mathf.Min(InterpolationSpeed * Time.deltaTime, 1);
        //current player direction
        var playerDirection = Player.transform.position - transform.position;
        //goal player direction
        playerDirection.y = 0;
        playerDirection = (playerDirection.normalized) * MinDistance;
        var goalPlayerDirection = playerDirection - Offset;
        //goal rotation
        var goalRotation = Quaternion.LookRotation(goalPlayerDirection, Vector3.up);
        //goal position
        var goalPosition = Player.transform.position - goalPlayerDirection;

        // Here we use linear interpolation for smooth camera movement
        transform.position = EnableSmooth ? Vector3.Lerp(transform.position, goalPosition, interpolationRatio) : goalPosition;
        transform.rotation = EnableSmooth ? Quaternion.Lerp(transform.rotation, goalRotation, interpolationRatio) : goalRotation;
    }

    private void PositionFreeCamera() {
        var rotateHorizontal = Input.GetAxis("Mouse X");
        transform.RotateAround(Player.transform.position, Vector3.up, rotateHorizontal);
    }

    public void SetFreeCamera() {
        cameraState = CameraState.Free;
    }

    public void SetFollowCamera() {
        cameraState = CameraState.Follow;
    }

}
