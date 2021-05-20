using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

//Enums for Location
//Note : Add Enum According to Backend Database(SerialWise).
public enum Location
{
    AndamanAndNicobarIslands,
    AndhraPradesh,
    ArunachalPradesh,
    Assam,
    Bihar,
    Chandigarh
}
public class Manager : MonoBehaviour
{
    private ReadJson readJson;
    public Location currentLocation;
    public List<Data> dataList=new List<Data>();

    // Start is called before the first frame update
    void Start()
    {
        readJson = GetComponent<ReadJson>();

        //Binding function to delegate event to call it after data is recieved
        readJson.onloaded += SetData;
    }

    //Function to set Data adter loading it from json script
    private void SetData(JsonData data)
    {
        for (int i = 0; i < 6; i++)
        {
            //Adding Data to Datalist
            dataList.Add(new Data(
                data["data"]["regional"][i]["loc"].ToString(),
                (Location)i, 
                data["data"]["regional"][i]["deaths"].ToString(), 
                data["data"]["regional"][i]["totalConfirmed"].ToString()
                ));
        }
        GetData();
    }

    //Function to GetData
    private void GetData()
    {
        foreach (Data item in dataList)
        {
            if(currentLocation==item.location)
            {
                print("Name-->" + item.name);
                print("Deaths-->"+item.deaths);
                print("Cases-->" + item.cases);
            }
        }
    }
}

//Structure or Blueprint Class for Data
[System.Serializable]
public class Data
{
    public string name;
    public Location location;
    public string deaths;
    public string cases;

    //Constructor
    public Data(string name,Location location, string deaths, string cases)
    {
        this.name = name;
        this.location = location;
        this.deaths = deaths;
        this.cases = cases;
    }
}
