using UnityEngine;
using System.Collections;

public class CircleErraticForceField : MonoBehaviour {

    [Tooltip("Rigidbodies in which layers will be affected?")]
    public LayerMask mask;
    
    public float radius = 1;

    [Tooltip("Force strength.")]
    public float forceMagnitude = 1;

    [HideInInspector]
    public AnimationCurve forceVariation = AnimationCurve.Linear(0, 1, 1, 1);

    [HideInInspector]
    public AnimationCurve dampingVariation = AnimationCurve.Linear(0, 0, 1, 0);

    public void FixedUpdate()
    {
        Collider2D[] affectedObject;
        affectedObject = Physics2D.OverlapCircleAll(transform.position, radius, mask);
        if (affectedObject.Length > 0)
        {
            for (int i=0;i<affectedObject.Length;++i)
            {
                ApplyForce(affectedObject[i].GetComponent<Rigidbody2D>());
            }
        }
    }

    public void ApplyForce(Rigidbody2D rgbd)
    {
        if (rgbd == null)
            return;
        //Debug.DrawLine(transform.position, rgbd.transform.position);

        Vector3 forceDirection = (rgbd.transform.position - transform.position).normalized;
        float scaleFactor = Vector2.Distance(rgbd.transform.position, transform.position) / radius;
        rgbd.AddForce(forceDirection * forceMagnitude * forceVariation.Evaluate(scaleFactor));
        rgbd.AddForce(-rgbd.velocity * rgbd.mass * dampingVariation.Evaluate(scaleFactor));
    }

    public void ClampRadius()
    {
        if (radius < 0)
            radius = 0;
    }

    public void OnValidate()
    {
        ClampRadius();
    }
}
