using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherVision : MonoBehaviour
{
    public GameObject player;
    public TeacherScript teacherScript;
    public int[] canSeeThroughDisguises = new int[] { 0 };

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        teacherScript = GetComponentInParent<TeacherScript>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !teacherScript.target)
        {
            // Check if player is obscured before detecting player
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position);
            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    Disguise playerDisguise = player.GetComponent<Disguise>();
                    if (playerDisguise)
                    {
                        int currentPlayerDisguise = playerDisguise.currentDisguise;

                        foreach (int badDisguise in canSeeThroughDisguises)
                        {
                            if (currentPlayerDisguise == badDisguise)
                            {
                                Debug.Log("Player detected");
                                teacherScript.target = player;
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (player && !teacherScript.target)
        // {
        //     RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        //     Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        //     float angle = Vector2.Angle(ray.transform.position, transform.forward);

        //     if (ray.transform.gameObject.tag == "Player")
        //     {
        //         Disguise playerDisguise = player.GetComponent<Disguise>();
        //         if (playerDisguise)
        //         {
        //             int currentPlayerDisguise = playerDisguise.currentDisguise;

        //             foreach (int badDisguise in canSeeThroughDisguises)
        //             {
        //                 if (currentPlayerDisguise == badDisguise)
        //                 {
        //                     // Debug.Log("Player detected");
        //                     teacherScript.target = player;
        //                 }
        //             }
        //         }


        //     }
        //     else
        //     {
        //         // Debug.Log("Player not detected");
        //     }
        // }

    }
}
