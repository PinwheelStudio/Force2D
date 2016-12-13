using UnityEngine;
using System.Collections;

public class TestOriginRotate : MonoBehaviour {

    [Header("Result")]
    public Vector2 origin;

    [Header("Parameter")]
    public BoxCollider2D box;
    public AnimationCurve curve;

    public void Update()
    {
        //origin = (box.bounds.center - new Vector3(box.bounds.extents.x, box.bounds.extents.y)).rota;
    }
}
