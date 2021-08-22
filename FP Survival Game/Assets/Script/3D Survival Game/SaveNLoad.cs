using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable] // 직렬화 : 데이터를 직렬화 하면 데이터들이 나열돼서 저장하거나 읽기 쉬워짐. 
public class SaveData
{
    public Vector3 playerPos; // 플레이어 위치값 기억시킬 변수 
}
public class SaveNLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData(); // 
    private string SAVE_DATA_DEIRECTORY; // 데이터 저장 경로 
    private string SAVE_FILENAME = "/SaveFile.txt"; //데이터 파일이름
    private PlayerController thePlayer; //플레이어 위치를 가져올 변수 



    void Start()
    {
        SAVE_DATA_DEIRECTORY = Application.dataPath + "/Saves/"; // 
        if(!Directory.Exists(SAVE_DATA_DEIRECTORY)){ // 만약 데이터 디렉토리가 없으면 생성해야함
            Directory.CreateDirectory(SAVE_DATA_DEIRECTORY); //생성 
        }
    }

   public void SaveData(){
       thePlayer = FindObjectOfType<PlayerController>(); //일단 오브젝트 찾아넣기
       saveData.playerPos = thePlayer.transform.position; // 플레이어 위치 저장
       string json = JsonUtility.ToJson(saveData); // saveData안에있는 플레이어 위치를 json화 시킴. 
       File.WriteAllText(SAVE_DATA_DEIRECTORY + SAVE_FILENAME, json); // 위에서 생성한 json을 실제 물리적인 파일로 저장시킴. 경로+파일이름에 json을 넣어줌. 
       Debug.Log("저장완료");
   }

   public void LoadData(){
        if(File.Exists(SAVE_DATA_DEIRECTORY + SAVE_FILENAME)){ 
            string loadJson = File.ReadAllText(SAVE_DATA_DEIRECTORY + SAVE_FILENAME); // 경로에있는 데이터를 loadJson에 다 가져옴
            saveData = JsonUtility.FromJson<SaveData>(loadJson); // 지금 json형태인loadJson을 다시 saveData에 맞게 넣기 

            thePlayer = FindObjectOfType<PlayerController>();
            thePlayer.transform.position = saveData.playerPos;  //위치 넣어주기 
        }
        else{
            Debug.Log("save file이 없습니다");
        }
   }
}
