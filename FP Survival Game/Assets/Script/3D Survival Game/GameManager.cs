using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static bool canPlayerMove; //플레이어의 움직임 제어
    public static bool isOpenInventory = false; // 인벤토리 활성화 여부
    public static bool isPause = false; //메뉴 활성화 여부

    //Text읽어오기 
    public TextAsset textAsset;
    string[] Sentence;
    int lineSize, rowSize;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 서커 잠굼. 
        Cursor.visible = false; // 커서 안보이게 함   

        ReadText();
    }

    
    void Update()
    {
        if(isOpenInventory || isPause){
            Cursor.lockState = CursorLockMode.None;  
            Cursor.visible = true; // 커서 보이게 함   
            canPlayerMove = false;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;  
            Cursor.visible = false; // 커서 보이게 함   
            canPlayerMove = true;
        }

        if(Input.GetKeyDown(KeyCode.P)){

        }
    }

    private void ReadText(){
        string currentText = textAsset.text.Substring(0, textAsset.text.Length  -1);
        print(currentText);
    }
}
