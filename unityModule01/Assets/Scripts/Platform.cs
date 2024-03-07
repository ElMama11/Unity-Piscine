using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length > 0) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.01f) {
                currentWaypointIndex++;
                // If the platform reaches the last waypoint, loop back to the first one
                if (currentWaypointIndex >= waypoints.Length)
                    currentWaypointIndex = 0;
            }
        }
    }
}
