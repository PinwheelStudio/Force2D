using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

[RequireComponent(typeof(BoxCollider2D))]
public class TextureDrivenPointEffector2D : MonoBehaviour {

    public BoxCollider2D boxCollider;
    public Texture2D forceVariationMap;
    public float forceMagnitude;

#if UNITY_EDITOR
    public bool visualize = true;
#endif


    /// <summary>
    /// Contain float value from 0 to 1.0 indicate how much force will be apply.
    /// </summary>
    float[,] variationMatrix;
    Vector3 boxCenter;

    public void Awake()
    {
        boxCenter = boxCollider.transform.position + (Vector3)boxCollider.offset;
    }

    public void Start()
    {
        BuildVariationMatrix();
    }

    /// <summary>
    /// Put value from alpha channel of forceVariationMap to variationMatrix.
    /// </summary>
    public void BuildVariationMatrix()
    {
        Color[] colorMap = forceVariationMap.GetPixels();

        variationMatrix = new float[forceVariationMap.width, forceVariationMap.height];
        int width = variationMatrix.GetLength(0);
        int height = variationMatrix.GetLength(1);
        
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                variationMatrix[x, y] = colorMap[y * width + x].a;
            }
        }
    }

    //public void Export()
    //{
    //    string filePath = Application.persistentDataPath + "/matrix.txt";
    //    print(filePath);
    //    if (!File.Exists(filePath))
    //    {
    //        File.Create(filePath).Close();
    //    }
    //    using (StreamWriter stream = new StreamWriter(filePath, false))
    //    {
    //        for (int i = 0; i < variationMatrix.GetLength(0); ++i)
    //        {
    //            for (int j = 0; j < variationMatrix.GetLength(1); ++j)
    //            {
    //                stream.Write(variationMatrix[j, i] + " ");
    //            }
    //            stream.WriteLine();

    //        }

    //    }
    //}

    public void OnTriggerStay2D(Collider2D col)
    {
        Vector2 forceDirection = (col.transform.position - boxCenter).normalized;
        //Debug.DrawRay(col.transform.position, forceDirection * forceMagnitude * Time.fixedTime);

        Vector2 positionOffset = col.transform.position - boxCollider.bounds.min;
        float positionRatioX = positionOffset.x / boxCollider.bounds.size.x;
        float positionRatioY = positionOffset.y / boxCollider.bounds.size.y;
        
        int matrixIndexX, matrixIndexY;
        matrixIndexX = Mathf.RoundToInt(positionRatioX * (variationMatrix.GetLength(0) - 1));
        matrixIndexY = Mathf.RoundToInt(positionRatioY * (variationMatrix.GetLength(1) - 1));

        

        //print(matrixIndexX + "  " + matrixIndexY);
        try
        {
            col.GetComponent<Rigidbody2D>().AddForce(forceDirection * forceMagnitude * variationMatrix[matrixIndexX, matrixIndexY]);
        }
        catch
        {
            
        }
    }

    /// <summary>
    /// Assign the boxCollider if it is null or there is only 1 BoxCollider2D attach to the game object.
    /// </summary>
	public void GetBoxCollider()
    {
        if (boxCollider != null)
            return;
        BoxCollider2D[] boxes = GetComponents<BoxCollider2D>();
        if (boxes.Length == 1)
            boxCollider = boxes[0];
    }

    public void OnValidate()
    {
        GetBoxCollider();
    }

    public void OnDrawGizmosSelected()
    {
        if (visualize && forceVariationMap != null) 
        {

            Material mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/Force2D/Gizmos/Gizmos.mat", typeof(Material));
            Gizmos.DrawGUITexture(
                new Rect(boxCollider.bounds.min.x, boxCollider.bounds.max.y, boxCollider.size.x, -boxCollider.size.y),
                forceVariationMap, mat); 
        }
    }
}
