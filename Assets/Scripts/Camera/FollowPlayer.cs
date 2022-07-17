using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followCamera;
    public GameObject freelookCamera;

    CameraState cameraState = CameraState.Follow;

    void Start() {
    }

    public void SetFreeCamera() {
        cameraState = CameraState.Free;

        //ForceToCurrentPerspective(freelookCamera);

        //freelookCamera
        //    .GetComponent<CinemachineFreeLook>()
        //    .ForceCameraPosition(transform.position, transform.rotation);

        freelookCamera.SetActive(true);
        followCamera.SetActive(false);
    }

    public void SetFollowCamera() {
        cameraState = CameraState.Follow;

        ForceToCurrentPerspective(followCamera);
        //followCamera
        //    .GetComponent<CinemachineVirtualCamera>()
        //    .ForceCameraPosition(transform.position, transform.rotation);

        freelookCamera.SetActive(false);
        followCamera.SetActive(true);
    }

    private void ForceToCurrentPerspective(GameObject camera)
    {
        camera
            .GetComponent<CinemachineVirtualCameraBase>()
            .ForceCameraPosition(transform.position, transform.rotation);
    }
}
