using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject globe;
    public GameObject indiaMap;
    public GameObject graph;

    private Vector3 globeInitialScale;
    private Vector3 indiaMapInitialScale;
    private Vector3 graphInitialScale;

    private void Awake()
    {
        globeInitialScale = globe.transform.localScale;
        indiaMapInitialScale = indiaMap.transform.localScale;
        graphInitialScale = graph.transform.localScale;
    }
    void Start()
    {
        globe.transform.localScale = Vector3.zero;
        indiaMap.transform.localScale = Vector3.zero;
        graph.transform.localScale = Vector3.zero;

        LeanTween.scale(globe, globeInitialScale, 0.5f);
    }

    //Coroutine for Animating Object after one another
    public IEnumerator CoroutineOnButtonClick(GameObject obj1,Vector3 initialScale,float waitTime, GameObject obj2, Vector3 SecondScale)
    {
        LeanTween.scale(obj1, initialScale, 0.5f);
        yield return new WaitForSeconds(waitTime);
        LeanTween.scale(obj2, SecondScale, 0.5f);
    }

    //Function called when clicked on country button
    public void OnCountryButtonClick()
    {
        StartCoroutine(CoroutineOnButtonClick(globe,Vector3.zero,0.6f,indiaMap,indiaMapInitialScale));
    }

    //Function called when clicked on city button
    public void onCityButtonClick()
    {
        StartCoroutine(CoroutineOnButtonClick(indiaMap, Vector3.zero, 0.6f, graph, graphInitialScale));
    }

    //Function called when clicked on back button of India Map Element or Object
    public void OnBackButtonIndiaMapObjectClick()
    {
        StartCoroutine(CoroutineOnButtonClick(indiaMap, Vector3.zero, 0.6f, globe, globeInitialScale));
    }

    //Function called when clicked on back button of Graph Element or Object
    public void OnBackButtonGraphObjectClick()
    {
        StartCoroutine(CoroutineOnButtonClick(graph, Vector3.zero, 0.6f, indiaMap, indiaMapInitialScale));
    }
}
