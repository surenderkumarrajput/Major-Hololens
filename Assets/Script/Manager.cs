using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LitJson;
using Microsoft.MixedReality.Toolkit.UI;

//Enums for Location
//Note : Add Enum According to Backend Database(SerialWise) to be Manually Added.
public enum Location
{
    AndamanAndNicobarIslands,
    AndhraPradesh,
    ArunachalPradesh,
    Assam,
    Bihar,
    Chandigarh,
    Chhattisgarh,
    DadraandNagarHaveliandDamanandDiu,
    Delhi,
    Goa,
    Gujarat,
    Haryana,
    HimachalPradesh,
    JammuandKashmir,
    Jharkhand,
    Karnataka,
    Kerala,
    Ladakh,
    Lakshadweep,
    MadhyaPradesh,
    Maharashtra,
    Manipur,
    Meghalaya,
    Mizoram,
    Nagaland,
    Odisha,
    Puducherry,
    Punjab,
    Rajasthan,
    Sikkim,
    TamilNadu,
    Telangana,
    Tripura,
    Uttarakhand,
    UttarPradesh,
    WestBengal,
}
public class Manager : MonoBehaviour
{
    private ReadJson readJson;
    public Location currentLocation;

    [HideInInspector]
    public List<Data> dataList=new List<Data>(); //List containing the data
    public GameObject graphPrefab; //Graph prefab which is to be spawnned
    public GraphHandler graphHandler; //Graph Handler script
    public Data totalData; //Data Variable for holding Total values in india
    public float multiplier=200;
    public Data selectedData;
    private GameManager gameManager;
    public GameObject currentCaseInIndia, currentDeathInIndia, currentRecoveredInIndia;
    public GameObject casesInSelectedArea, deathsInSelectedArea, RecoveredInSelectedArea;

    // Start is called before the first frame update
    void Start()
    {
        readJson = GetComponent<ReadJson>();
        gameManager = GetComponent<GameManager>();

        if (graphPrefab)
        {
        //Setting default name of cube text to loading.
        foreach (TextMeshProUGUI name in graphHandler.CubeNames)
        {
            name.text = "Loading";
        }

        //Setting deafult name of graph text to Loading as it take time to get and set data
        graphHandler.locationName.text = "Loading";
        }

        //Binding function to delegate event to call it after data is recieved
        readJson.onloaded += SetData;
    }

    //Function to set Data adter loading it from json script
    private void SetData(JsonData data)
    {
        for (int i = 0; i < data["data"]["regional"].Count; i++)
        {
            //Adding Data to Datalist
            dataList.Add(new Data(
                data["data"]["regional"][i]["loc"].ToString(),
                (Location)i, 
                (int)data["data"]["regional"][i]["deaths"],
                 (int)data["data"]["regional"][i]["totalConfirmed"],
                 (int)data["data"]["regional"][i]["discharged"]
                ));
        }

        //Setting total Data
        totalData.cases = (int)data["data"]["summary"]["total"];
        totalData.deaths = (int)data["data"]["summary"]["deaths"];
        totalData.recovered = (int)data["data"]["summary"]["discharged"];

        //Assign Cases, Deaths, Recoved Cases In India on Text UI
        currentCaseInIndia.GetComponent<TextMeshPro>().text = totalData.cases.ToString();
        currentDeathInIndia.GetComponent<TextMeshPro>().text = totalData.deaths.ToString();
        currentRecoveredInIndia.GetComponent<TextMeshPro>().text = totalData.recovered.ToString();

    }

    //Function to GetData
    private Data GetData()
    {
        foreach (Data item in dataList)
        {
            if(currentLocation==item.location)
            {
                //Changing scale and setting names of Cubes according to data
                graphHandler.graphCubes[0].transform.localScale = new Vector3(0.5f, ((float)item.deaths / (float)totalData.deaths)* multiplier, 0.5f);
                graphHandler.CubeNames[0].text = "Deaths";

                graphHandler.graphCubes[1].transform.localScale = new Vector3(0.5f, ((float)item.cases / (float)totalData.cases)* multiplier, 0.5f);
                graphHandler.CubeNames[1].text = "Cases";

                graphHandler.graphCubes[2].transform.localScale = new Vector3(0.5f, ((float)item.recovered / (float)totalData.recovered)* multiplier, 0.5f);
                graphHandler.CubeNames[2].text = "Recovered";

                //Setting Location name to text
                graphHandler.locationName.text = item.name;

                //Assign Cases, Deaths, Recoved Cases of Selected Area on Text UI
                casesInSelectedArea.GetComponent<TextMeshPro>().text = item.cases.ToString();
                deathsInSelectedArea.GetComponent<TextMeshPro>().text = item.deaths.ToString();
                RecoveredInSelectedArea.GetComponent<TextMeshPro>().text = item.recovered.ToString();

                return item;
            }
        }
        return null;
    }

    public void onLocationClicked(PressableButtonHoloLens2 button)
    {
        currentLocation = button.GetComponent<ButtonManager>().location;
        GetData();
        gameManager.onCityButtonClick();
    }
}

//Structure or Blueprint Class for Data
[System.Serializable]
public class Data
{
    public string name;
    public Location location;
    public int deaths;
    public int cases;
    public int recovered;

    //Constructor
    public Data(string name,Location location, int deaths, int cases, int recovered)
    {
        this.name = name;
        this.location = location;
        this.deaths = deaths;
        this.cases = cases;
        this.recovered = recovered;
    }
}
