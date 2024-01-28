using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            // Panggil fungsi FlipX setiap kali mencapai waypoint
            FlipX();
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void FlipX()
    {
        // Tentukan apakah sedang menuju waypoint pertama atau kedua
        int nextWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        // Tentukan arah hadap karakter berdasarkan perbandingan posisi waypoint sekarang dan berikutnya
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Sign(waypoints[nextWaypointIndex].transform.position.x - waypoints[currentWaypointIndex].transform.position.x);
        transform.localScale = newScale;
    }
}
