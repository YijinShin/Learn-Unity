using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_TalkManager : MonoBehaviour
{
    string path;
    List<Dictionary<string, object>> gameText;
    
    void Awake()
    {
        path = "Tutorial_Dialogue";
        gameText = CSVReader.Read(path);
    }

    public string GetExplain(int talkIndex)
    {
        Debug.Log("talkIndex : " + talkIndex+ " gameText : " + gameText.Count);
        if(talkIndex >= gameText.Count)
        {
            return null;
        }

        return gameText[talkIndex]["Text"].ToString();

        
    }

    public string GetByKey(string key, int number)
    {
        for(int i = 0; i < gameText.Count; i++)
        {
            if(key == gameText[i]["key"].ToString() )
            {
                if( number.ToString() == gameText[i]["number"].ToString())
                    return gameText[i]["Text"].ToString();
                return "Wrong";
            }
        }

        return null;
    }
}
