using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherVision : MonoBehaviour
{
    public Transform player;
    public TeacherScript teacherScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        teacherScript = gameObject.GetComponent<TeacherScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.position - transform.position);
            Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

            float angle = Vector2.Angle(ray.transform.position, transform.forward);

            if (ray.transform.gameObject.tag == "Player")
            {
                // Debug.Log("Player detected");
                teacherScript.target = player.gameObject;
            }
            else
            {
                // Debug.Log("Player not detected");
            }
        }
        
    }
}
