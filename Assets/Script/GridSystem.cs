using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float size;
    
    public Vector3 GetNearestPointonGrid(Vector3 pos)
    {
        pos -= transform.position;

        int xCount = Mathf.RoundToInt(pos.x/size);
        int yCount = Mathf.RoundToInt(pos.y/size);
        int zCount = Mathf.RoundToInt(pos.z/size);

        Vector3 result = new Vector3(
            (float)xCount * size, 
            (float)yCount * size, 
            (float)zCount * size);
        result += transform.position;

        return result;
    }
}
