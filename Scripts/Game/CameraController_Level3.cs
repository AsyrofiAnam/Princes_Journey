using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Level3 : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float minXPositionFollowingPlayer = 19f;
    private float maxXPosition = 25.73993f;
    private float minXShiftThreshold = 9.4f;
    private bool shouldFollowPlayer = false;

    private Vector3 initialPlayerPosition;
    private Vector3 initialPlayerVelocity;

    private void Start()
    {
        // Simpan posisi dan kecepatan awal pemain.
        initialPlayerPosition = player.position;
        initialPlayerVelocity = Vector3.zero;
    }

    private void Update()
    {
        // Jika player melewati batas minXShiftThreshold (10) pada sumbu x, maka mulai mengikuti pergerakan player.
        if (player.position.x > minXShiftThreshold)
        {
            shouldFollowPlayer = true;
        }

        // Tentukan posisi x target kamera.
        float targetX = shouldFollowPlayer ? Mathf.Clamp(player.position.x, minXPositionFollowingPlayer, maxXPosition) : transform.position.x;

        // Atur posisi kamera.
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
}
