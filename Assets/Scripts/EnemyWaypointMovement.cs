using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypointMovement : MonoBehaviour
{
    [Header("Waypoints")]
    public List<Transform> waypoints;
    [Header ("Movement Settings")]
    public float moveSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public bool loop = true;

    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;
    private Vector2 movementDirection;
    public Transform visual;
    private Animator anim;


void Start (){
    rb = GetComponent<Rigidbody2D>();
     anim = visual.GetComponent<Animator>();

    if (waypoints == null || waypoints.Count == 0){
        Debug.LogError("No waypoints assigned to the enemy!");
        enabled = false;
        return;
    }

    SetTargetWaypoint (currentWaypointIndex);
}
void FixedUpdate (){
    MoveTowardsWaypoint();
    CheckIfWaypointReached();
}

void SetTargetWaypoint (int index){
    if (waypoints.Count == 0) return;

    currentWaypointIndex = index;
    Vector2 targetPosition = waypoints[currentWaypointIndex].position;
    movementDirection = (targetPosition - (Vector2)transform.position).normalized;
}

void MoveTowardsWaypoint (){
    if (waypoints.Count == 0) return;

    Vector2 targetPosition = waypoints[currentWaypointIndex].position;
    movementDirection = (targetPosition - (Vector2)transform.position).normalized;

    rb.linearVelocity = movementDirection*moveSpeed;
            if (movementDirection.x > 0.01f)
        {
            visual.localScale = new Vector3(-1, 1, 1);
        }
        else if(movementDirection.x < -0.01f)
        {
            visual.localScale = new Vector3(1, 1, 1);
        }

}

void CheckIfWaypointReached(){
    if (waypoints.Count == 0) return;
    float distanceToWaypoint = Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position);

    if(distanceToWaypoint <= waypointReachedDistance){
        GoToNextWaypoint();
    }
}

void GoToNextWaypoint(){
    currentWaypointIndex++;

    if(currentWaypointIndex >= waypoints.Count){
        if (loop){
            currentWaypointIndex = 0;
        }
        else {
            enabled = false;
            rb.linearVelocity = Vector2.zero;
            return;
        }
    }
    SetTargetWaypoint(currentWaypointIndex);
}
}