using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TeacherScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public Tilemap tilemap;
    public Transform visionCone;
    public Transform[] patrolWaypoints;
    public int currentPatrolWaypointIndex = 0;

    public float speed;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 nextDest;
    private bool playerDetected = false;

    private bool isPatrolling = true;
    private bool isChasing = false;

    public float waitTime = 0.0f;
    public float defaultWaitTime = 1.0f;

    private Animator animator;
    private Vector2 lastDirection = Vector2.zero;

    void Start()
    {   
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on TeacherScript.");
        } 
        visionCone = transform.GetChild(0);
        nextDest = transform.position;

        if (isPatrolling && currentPatrolWaypointIndex < patrolWaypoints.Length)
        {
            target = patrolWaypoints[currentPatrolWaypointIndex].gameObject;
        }
    }

    class Node
    {
        public Vector3Int pos;
        public Node reachedFrom;

        public int predictedCost;
        public int cost;
        public Node(Vector3Int p)
        {
            pos = p;
        }
    }


    List<Node> getNeighbours(Node n)
    {
        var up = n.pos + Vector3Int.up;
        var down = n.pos + Vector3Int.down;
        var left = n.pos + Vector3Int.left;
        var right = n.pos + Vector3Int.right;
        var neighbours = new List<Node>();

        if (tilemap.GetTile(up) == null)
        {
            neighbours.Add(new Node(up));
        }


        if (tilemap.GetTile(down) == null)
        {
            neighbours.Add(new Node(down));
        }


        if (tilemap.GetTile(left) == null)
        {
            neighbours.Add(new Node(left));
        }


        if (tilemap.GetTile(right) == null)
        {
            neighbours.Add(new Node(right));
        }

        return neighbours;
    }

    int cost(Node n, Node dest)
    {
        // returns manhattan distance
        var diff = n.pos - dest.pos;
        return (Math.Abs(diff.x) + Math.Abs(diff.y));
    }

    void insert(LinkedList<Node> q, Node n)
    {
        // Insert using O(n) linear search
        if (q.Count == 0)
        {
            q.AddFirst(n);
            return;
        }

        LinkedListNode<Node> current = q.First;

        while (current != null)
        {
            if (n.predictedCost < current.Value.predictedCost)
            {
                q.AddBefore(current, n);
                return;
            }
            current = current.Next;
        }
        //If predicted cost not smaller than any of them
        q.AddAfter(q.Last, n);
    }

    Vector3Int Astar()
    {
        var start = new Node(tilemap.WorldToCell(transform.position));
        var dest = new Node(tilemap.WorldToCell(target.transform.position));

        start.cost = 0;
        start.predictedCost = start.cost + cost(start, dest);

        var q = new LinkedList<Node>();
        insert(q, start);

        var nextHop = Vector3Int.zero;

        //int maxSteps = 100;
        //int steps = 0;

        while (q.Count > 0)
        {
            /*
            steps++;
            if (steps > maxSteps)
            {
                break;
            }
            */
            var n = q.First.Value;
            q.RemoveFirst();

            //Debug.Log(n.pos);

            if (cost(n, dest) == 0)
            {
                // We have found the destination.
                //Debug.Log("found at " + n.pos);

                while (n.reachedFrom != null && n.reachedFrom != start)
                {
                    n = n.reachedFrom;
                }
                nextHop = n.pos;
                break;
            }

            foreach (var neigbour in getNeighbours(n))
            {
                neigbour.cost = n.cost + 1;
                neigbour.predictedCost = neigbour.cost + cost(neigbour, dest);
                neigbour.reachedFrom = n;

                insert(q, neigbour);
            }
        }

        return nextHop;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0)
                {
                    currentPatrolWaypointIndex++;
                    if (currentPatrolWaypointIndex >= patrolWaypoints.Length)
                    {
                        currentPatrolWaypointIndex = 0;
                    }

                    target = patrolWaypoints[currentPatrolWaypointIndex].gameObject;
                }
            }
            else
            {
                float distanceToTarget = Vector3.Magnitude(target.transform.position - transform.position);
                if (isPatrolling && distanceToTarget < 1.0f)
                {
                    waitTime = defaultWaitTime;
                }
            }
        }


        if (target)
        {
            // Face the target (either the player or patrol waypoint)
            Vector2 targ = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90f;
            visionCone.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Vector3.Magnitude(transform.position - nextDest) < 0.01)
            {
                var nextHop = Astar();
                nextDest = tilemap.CellToWorld(nextHop) + new Vector3(0.5f, 0.5f, 0.5f);
            }

            // Move towards the next destination
            Vector2 direction = (nextDest - transform.position).normalized;
            HandleAnimation(direction);
            transform.position = Vector3.MoveTowards(transform.position, nextDest, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TogglePatrolling()
    {
        isPatrolling = true;
        isChasing = false;
    }

    public void ToggleChasing()
    {
        isPatrolling = false;
        isChasing = true;
    }

    void HandleAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            if (lastDirection == Vector2.up)
            {
                animator.Play("TeacherIdleDown");
            }
            else if (lastDirection == Vector2.down)
            {
                animator.Play("TeacherIdleUp");
            }
            else if (lastDirection == Vector2.left)
            {
                animator.Play("TeacherIdleRight");
            }
            else if (lastDirection == Vector2.right)
            {
                animator.Play("TeacherIdleLeft");
            }
            return;
        }
        else if (direction.x > 0)
        {
            animator.Play("TeacherMoveRight");
        }
        else if (direction.x < 0)
        {
            animator.Play("TeacherMoveLeft");
        }
        else if (direction.y > 0)
        {
            animator.Play("TeacherMoveUp");
        }
        else if (direction.y < 0)
        {
            animator.Play("TeacherMoveDown");
        }
        lastDirection = direction;
    }
}
