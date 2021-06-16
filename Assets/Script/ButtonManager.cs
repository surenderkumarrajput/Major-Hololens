using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public Location location;
    public GameObject marker;

    private void Start()
    {
        if(marker)
        {
            marker.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    //Changing Scale of marker when gaze Hovers the Button
    public void ButtonEnter()
     {
        LeanTween.scale(marker,new Vector3(0.006f,0.006f,0.006f),0.5f);
     }

    //Changing Scale of marker when gaze leaves the Button
    public void ButtonLeave()
    {
        LeanTween.scale(marker, new Vector3(0, 0, 0), 0.5f);
    }

}
