using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public string sceneName = "Game Stage";
    public static Title instance;
    private SaveNLoad theSaveNLoad;
    private void Awake(){ // 시작하자마자 실행됨. 싱글턴 하기 위해 만듬. 
        //원래 씬은 한번 이동하면 이전씬은 다 파괴되는데 싱글턴을 해놓으면 유지됨. 

        theSaveNLoad = FindObjectOfType<SaveNLoad>(); 

        if(instance == null){
            instance = this; // 자기 자신을 넣어줌 
            DontDestroyOnLoad(gameObject); // 안에 자기 자신을 넣은 것. 파괴시키지 않음. 
        }
        else{ // 그렇지 않으면 파괴 시킴. 
            Destroy(this.gameObject);
        }
    }

    public void ClickStart(){
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad(){
        StartCoroutine(LoadCoroutine());
    }

    public void ClickExit(){
        Application.Quit();
    }

    IEnumerator LoadCoroutine(){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName); // 
        while(!operation.isDone){ // 로딩이 끝날때까지 
            yield return null; // 대기 
        }
        //여기까지 오면 이미 다음씬으로 넘어가서 이전은 파괴됨(?) 그러니까 다시 찾아주고나서 로드 
        theSaveNLoad = FindObjectOfType<SaveNLoad>(); //다른 씬에 있는 snl을 찾기 
        theSaveNLoad.LoadData();
        Destroy(gameObject); // 타이틀 씬 없애기   
    }
}
