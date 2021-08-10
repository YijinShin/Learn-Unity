using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> hintDataList = new List<GameObject>();
    // public List<bool> HintBoolList;
    public Dictionary<string, GameObject> HintDB = new Dictionary<string, GameObject>();
    public Dictionary<GameObject, bool> HintDBbool = new Dictionary<GameObject, bool>();

    void Start()
    {
        for(int i =0; i < hintDataList.Count; i++){
            HintDB.Add(hintDataList[i].gameObject.name,hintDataList[i]);
            HintDBbool.Add(hintDataList[i], false);
        }
        
    }

    
}
