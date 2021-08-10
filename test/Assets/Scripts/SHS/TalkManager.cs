using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    string path;
    List<Dictionary<string, object>> gameText;
    
    void Awake()
    {
        path = "Explanation";
        gameText = CSVReader.Read(path);
    }

    public string GetExplain(int talkIndex)
    {
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

