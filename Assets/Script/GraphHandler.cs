using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;


public class GraphHandler : MonoBehaviour
{
    public List<GameObject> graphCubes=new List<GameObject>();
    public TextMeshProUGUI locationName;
    public TextMeshProUGUI[] CubeNames;
    private void Start()
    {
        GetComponent<Billboard>().TargetTransform = Camera.main.transform;
    }
}
