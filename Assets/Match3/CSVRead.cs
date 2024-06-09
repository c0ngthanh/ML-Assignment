using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CSVRead
{   

    public static string[] ReadCSV(string name){
        TextAsset textAssetData = Resources.Load("Config/CSV/"+name) as TextAsset;
        string[] data = textAssetData.text.Split(new string[] {",","\n"},System.StringSplitOptions.None);
        return data;
    }    
}
