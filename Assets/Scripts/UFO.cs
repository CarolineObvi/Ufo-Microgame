using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public PointAndShoot controller;
    public bool go = false;
    public int ufoNumber = 0;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private float distance;
    private int waypointIndex = 0;
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;
    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (go == true)
        {
            Move();
        }
    }

    void OnMouseDown()
    {
        controller.UfoCaught();

    }

    private void Move()
    {

        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            //Debug.Log("In Move!");
            //Debug.Log(waypointIndex);

            distance = Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position);
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (distance <= 0.1f)
            {
                waypointIndex++;
            }
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void Enable ()
    {
        Debug.Log("Go " + ufoNumber + "!");
        go = true; // this sucks
    }


}