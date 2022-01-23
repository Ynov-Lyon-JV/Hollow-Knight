using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Chasse : MonoBehaviour
{
    public Transform target;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;

    public float nextWaypointDistance = 3f;
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if(Player.instance)
            target = Player.instance.transform.Find("Head").transform;

        InvokeRepeating("UpdatePath", 0f, 0.4f);
    }
    void UpdatePath()
    {
        if (target == null)
        {
            GetComponent<Mob>().timeDetect = 0;
            return;
        }
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    // Update is called once per frame
    public Vector2 UpdateMove(float speed)
    {
        if (path == null)
            return Vector2.zero;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return Vector2.zero;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        rb.velocity = direction * speed;
        return direction;
    }
}
