using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static bool canPlayerMove; //플레이어의 움직임 제어
    public static bool isOpenMenu = false; // 메뉴 활성화 여부
    public static bool isOpenSetting = false; //설정 활성화 여부
    public static bool isOpenVote = false; //투표 활성화 여부 >> 다른 씬으로 이동 

     //Text읽어오기 
    public TextAsset textAsset;
    public string[,] Sentence;
    public int lineSize, rowSize;

    void Start()
    {
        ReadText();
    }

    
    void Update()
    {
        if(!canPlayerMove){
            
        }
    }

    private void ReadText(){
        //엔터 단위와 탭으로 나눠서 배열의 크기 조정
        string currentText = textAsset.text.Substring(0, textAsset.text.Length  -1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence =  new string[lineSize, rowSize];
        
        // 한줄에서 탭으로 나눔
        for(int i=0;i<lineSize;i++){
            string[] row = line[i].Split('\t');
            for(int j = 0 ;j<rowSize;j++){
                Sentence[i,j] = row[j];
            }
        }
    }
}
