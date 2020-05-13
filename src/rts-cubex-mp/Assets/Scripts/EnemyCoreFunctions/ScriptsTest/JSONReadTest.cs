using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using LitJson;

public class JSONReadTest : MonoBehaviour
{
    [SerializeField] string pathToFile = "empty";
    private string jSonString;

    void Start()
    {
        //jSonString = File.ReadAllText(Application.dataPath + pathToFile);
        
        //Converts data into json..
        // Colors gameColors = new Colors();
        // TextAsset asset = Resources.Load(pathToFile) as TextAsset;

        // if (asset != null)
        // {
        //     gameColors = JsonUtility.FromJson<Colors>(asset.text);

        //     for (int jIndex = 0; jIndex < gameColors.Count; jIndex++)
        //     {

        //     }
        // }
        // List<string> jsonText = new List<string>();

        // gameColors.Add(new Colors("green", "#21EE2E"));
        // gameColors.Add(new Colors("white", "#FFFFFF"));
        // gameColors.Add(new Colors("white", "#000000"));

        // for (int jIndex = 0; jIndex < gameColors.Count; jIndex++)
        // {
        //     jsonText.Add(JsonUtility.ToJson(gameColors[jIndex]));
        //     Debug.Log(jsonText[jIndex]);
        // }

        // if (System.IO.File.Exists(Application.dataPath + pathToFile))
        // {
        //     for (int jIndex = 0; jIndex < gameColors.Count; jIndex++)
        //     {
        //         var line = jsonText[jIndex] + ",";
        //         File.AppendAllText(Application.dataPath + pathToFile, line + '\n');
        //         Debug.Log(jsonText[jIndex]);
        //     }
        // }
         string jsonText = File.ReadAllText(Application.dataPath + pathToFile);
         File.WriteAllText(Application.dataPath + pathToFile, jsonText);
        //From json to simple data...
        
        Colors colorsLoaded = JsonUtility.FromJson<Colors>(jsonText);
        Debug.Log("Color name: " + colorsLoaded.name);
        Debug.Log("Color value: " + colorsLoaded.value);
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public class Colors
    {
        public string name, value;

        
    }
}
