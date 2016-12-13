using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleController : MonoBehaviour {

    public float pushStrength;
    //public float maxVelocity;

    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    Rigidbody2D rgbd;

    public void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        //rgbd.AddForce(new Vector2(1, 1) * 100);
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(up))
        {
            rgbd.AddForce(Vector2.up * pushStrength);
        }
        if (Input.GetKey(down))
        {
            rgbd.AddForce(-Vector2.up * pushStrength);
        }
        if (Input.GetKey(left))
        {
            rgbd.AddForce(-Vector2.right * pushStrength);
        }
        if (Input.GetKey(right))
        {
            rgbd.AddForce(Vector2.right * pushStrength);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rgbd.AddForce(dir * pushStrength);
        }
    }
}
