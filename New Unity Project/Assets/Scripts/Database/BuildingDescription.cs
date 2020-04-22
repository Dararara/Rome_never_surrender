using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDescription
{
    static readonly object Padlock = new object();
    static private BuildingDescription buildingDescription;
    static public BuildingDescription getInstance()
    {
        if(buildingDescription == null)
        {
            lock (Padlock)
            {
                if(buildingDescription == null)
                {
                    buildingDescription = new BuildingDescription();
                }
            }
        }
        return buildingDescription;
    }
    public Dictionary<string, string> building_description_dict = new Dictionary<string, string>();
    public BuildingDescription()
    {

        TextAsset text = Resources.Load<TextAsset>("text");
        string[] temp = text.text.Trim().Split('\n');
        string name = null;
        string description = null;
        string str_trim;
        for (int i = 0; i < temp.Length; i++)
        {

            str_trim = temp[i].Trim();
            Debug.Log(str_trim);
            if (str_trim.Length > 0 && name == null) {
                name = str_trim;
            }else if (str_trim.Length > 0 && name != null)
            {
                if(description == null)
                {
                    description = str_trim;
                }
                else
                {
                    description += "\n" + str_trim;
                }
            }else if(str_trim.Length == 0 && name != null && description != null)
            {

                building_description_dict.Add(name, description);
                name = null;
                description = null;
            }
        }
        building_description_dict.Add(name, description);
        Debug.Log("hello world my god");
    }
}
