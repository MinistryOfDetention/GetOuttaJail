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
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.path.corners.Length > 1)
        {
            //Debug.Log(agent.path.corners[1] - agent.path.corners[0]);
            Debug.Log(agent.path.corners[1]);
        }
        agent.SetDestination(target.transform.position);
    }
}
