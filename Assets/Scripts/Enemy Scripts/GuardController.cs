using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    [Header("Detection")]
    public GameObject[] waypoints; // the path the guard moves across
    private int currentWaypoint = 0;
    public float patrolSpeed = 1.5f;

    [Header("Detection")]
    private Transform player; // player's location
    public LayerMask viewMask; // what obfuscates the view of the guard
    private bool isDetectingPlayer; // is the guard detecting the player?
    private float detectionTimer; // how long the player's been in the guard's detection cone
    public float detectionLength = 1f; // how long until the guard detect the player
    public float viewDistance = 3f; // view distance of guard
    private float originalViewDistance; // original view distance
    public float viewAngle = 60f; // angle of detection

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // assigns player
        originalViewDistance = viewDistance;
        detectionTimer = detectionLength;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        if (CanSeePlayer())
        {
            isDetectingPlayer = true;
            Detection();
        }
        else
        {
            detectionTimer = detectionLength;
            isDetectingPlayer = false;
        }

        if (isDetectingPlayer)
        {
            detectionTimer -= Time.deltaTime;
        }

        if (detectionTimer <= 0)
        {
            Debug.Log("You lose!");
        }
    }

    private void Patrol()
    {
        if (currentWaypoint < waypoints.Length)
        {
            float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position);

            if (distance < 0.1f)
            {
                currentWaypoint++;
            }
            else if (!isDetectingPlayer)
            {
                Vector3 direction = waypoints[currentWaypoint].transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);
                transform.Translate(0f, 0f, patrolSpeed * Time.deltaTime);
            }
        }
        else
        {
            currentWaypoint = 0;
        } 
    }

    private void Detection()
    {
        Debug.Log("Guard can see!");
    }

    bool CanSeePlayer()
    {
        
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }

        return false;
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay (transform.position, transform.forward * viewDistance); // draws ray to represent vision distance
    }
}
