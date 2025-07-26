using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;

public class TeacherScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;

    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Wait to be given a target (e.g., by the EnemyVision script)
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.path.corners.Length > 1)
        {
            //Debug.Log(agent.path.corners[1] - agent.path.corners[0]);
            Debug.Log(agent.path.corners[1]);
        }

        if (target)
        {
            // Face the target (most likely the player)
            Vector2 targ = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            agent.SetDestination(target.transform.position);
        }
    }
}
