using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CircleErraticForceField))]
public class ErraticForceFieldEditor : Editor {

    CircleErraticForceField instance;

    public void OnEnable()
    {
        instance = (CircleErraticForceField)target;
        instance.mask = LayerMask.NameToLayer("Everything");
    }

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        instance.forceVariation = EditorGUILayout.CurveField(
            new GUIContent(
                "Variation", 
                "The variation/ distribution of force strength, from the central point, to the edge. \nX-axis: Relative position of the rigidbody inside the cyan circle. \nY-axis: Variation factor."),
                instance.forceVariation, 
                Color.cyan, 
                new Rect(0, -1, 1, 2));
        instance.dampingVariation = EditorGUILayout.CurveField(
            new GUIContent(
                "Damping", 
                "Some tooltip."),
                instance.dampingVariation,
                Color.magenta,
                new Rect(0, 0, 1, 1));
    }

    public void OnSceneGUI()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(instance.transform.position, Vector3.forward, instance.radius);
    }
}
