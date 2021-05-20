using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class ReadJson : MonoBehaviour
{
    public JsonData JsonData;
    private string jsonString;
    public string url;

    //Delegate for calling events that needs to be called after data is loaded
    public delegate void onLoaded(JsonData data);
    public onLoaded onloaded;

    private void Start()
    {
        StartCoroutine(GetData());
    }

    private IEnumerator GetData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error occured with web request");
            }
            else
            {
                jsonString = (webRequest.downloadHandler.text);
                JsonData = JsonMapper.ToObject(jsonString);
                onloaded(JsonData);
            }
        }
    }
}
