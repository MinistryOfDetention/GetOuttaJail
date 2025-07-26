using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(RotationConstraint))]
public class FixRotation : MonoBehaviour
{
    Quaternion rotation;

    GameObject rotationConstraint;

    public GameObject defaultRotationConstraint;

    RotationConstraint rc;

    void Awake()
    {
        rc = GetComponent<RotationConstraint>();

        rotationConstraint = GameObject.FindGameObjectWithTag("RotationConstraint");
        if (rotationConstraint == null)
        {
            rotationConstraint = Instantiate(defaultRotationConstraint, Vector3.zero, Quaternion.identity);
            rotationConstraint.tag = "RotationConstraint";
        }
    
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = rotationConstraint.transform;
        source.weight = 1;

        List<ConstraintSource> sources = new List<ConstraintSource>();
        sources.Add(source);

        rc.SetSources(sources);

        rc.constraintActive = true;
        rc.enabled = true;
    }
}
