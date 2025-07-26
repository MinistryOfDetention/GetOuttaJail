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

    public float speed;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 nextDest;
    private bool playerDetected = false;

    void Start()
    {
        visionCone = transform.GetChild(0);
        nextDest = transform.position;
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

        if (target)
        {
            // Face the target (most likely the player)
            Vector2 targ = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90f;
            visionCone.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Vector3.Magnitude(transform.position - nextDest) < 0.01)
            {
                var nextHop = Astar();
                nextDest = tilemap.CellToWorld(nextHop) + new Vector3(0.5f, 0.5f, 0.5f);
            }

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
}
