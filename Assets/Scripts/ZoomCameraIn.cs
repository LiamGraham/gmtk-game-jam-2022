using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCameraIn : MonoBehaviour
{
    public Vector3 StartOffset = new Vector3(10, 10, 0);
    public Vector3 EndOffset = new Vector3(0, 0.5f, 0);
    public float Duration = 1;
    public bool RunOnStart = true;

    private bool running = false;
    private FollowPlayer followPlayer;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        if (RunOnStart)
        {
            RunZoom();
        }
    }

    public void RunZoom()
    {
        followPlayer = GetComponent<FollowPlayer>();

        // Don't change the camera if we can't find the follow player script
        if (followPlayer == null)
        {
            Console.Error.WriteLine($"{nameof(ZoomCameraIn)} Script is not on a component with a {nameof(FollowPlayer)} Script");

            return;
        }

        running = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            var runtime = Time.time - startTime;

            if (runtime > Duration)
            {
                running = false;
                followPlayer.Offset = EndOffset;
            }

            followPlayer.Offset = Vector3.Lerp(StartOffset, EndOffset, runtime / Duration);
        }
    }
}
