/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Script_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public string place;
    public string Q;
    public string num;
    public string NPC;
    public string Player;
    public int SizeOfQuest;

    public bool SceneOn;
    public bool TextPannelOn;
    private int line_counter;
    public int daycounter;
    public bool dayend;

    public GameObject Quest;
    public GameObject testCharactor;
    public GameObject scriptTextManagerObject;
    public GameObject TextPannel;

    
    private TextManager text_manager;
    private List<Dictionary<string, object>> data;
    private Quest_address quest;
    private List<Dictionary<string,object>> schedule;
    private List<Dictionary<string, object>> Save;
    private  string path;

    void Start()
    {
        line_counter = 0;
        daycounter=0;
        schedule= CSVReader.Read("Test_Daily_Quest");
        quest = new Quest_address();
        Save_Load();
        Load_DailyQuest_Schedule();
        path = quest.GetQuestAddress(place, Q, num);
        SizeOfQuest=quest.GetQuestEnd(place,Q);
        data = CSVReader.Read(path);

        Script_Manager show = new Script_Manager();
        text_manager = scriptTextManagerObject.GetComponent<TextManager>();
        CheckNPC();
        readCSV();

    }

    void Save_Load() {
        Save = CSVReader.Read("Save_Game_Progress");
       daycounter =int.Parse(Save[0]["daycounter"].ToString())-1;
        num = Save[0]["num"].ToString() ;
    }
    void Save_SaveProgress() { 
//        Save[0][""]
            using (var writer = new CsvFileWriter("Assets/Resources/Save_Game_Progress.csv"))
        {
            List<string> columns = new List<string>() { "daycounter", "num" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();
            columns.Add(daycounter.ToString());
            columns.Add(num.ToString());
            writer.WriteRow(columns);
            columns.Clear();

        }



        Debug.Log("Finished Save");
    }
    void CheckNPC()
    {
        for(int i=0;i<data.Count;i++){
            if(data[i]["category"].ToString()!="Interaction" && data[i]["category"].ToString()!="Animation" && data[i]["category"].ToString()!="Scene_S" && data[i]["category"].ToString()!="Scene_E"&&data[i]["category"].ToString()!="Reward"&&data[i]["category"].ToString()!="Me")
            {
                NPC=data[i]["category"].ToString();
                break;
            }
        }
    }
    public void CloseScene()
    {
        int EndScene = 0;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["category"].ToString() == "Scene_E")
            {
                EndScene = i;
                break;
            }
        }
        line_counter = EndScene + 1;
    }

    void Load_DailyQuest_Schedule()
    {
        place = schedule[daycounter]["Place"].ToString();
        Q = schedule[daycounter]["Q"].ToString();
        Debug.Log("Daily Quest Loaded");
        daycounter++;
        num ="1";
        
    }
    void updateQuest()
    {
        line_counter=0;
        if(SizeOfQuest.ToString() == num){
            Debug.Log("Quest is updating");
            
            Load_DailyQuest_Schedule();
            SizeOfQuest = quest.GetQuestEnd(place,Q);

        }else{
            num = (int.Parse(num)+1).ToString();
        }
        path = quest.GetQuestAddress(place,Q,num);
        line_counter=0;
        data =CSVReader.Read(path);
        Save_SaveProgress();
        CheckNPC();
    }


    public void readCSV()
    {
        if (line_counter == data.Count)
        {
            Debug.Log("EndIs Working");
            updateQuest();
        }
        //인터렉션과 애니메이션은 종료시 리턴을 받아와야함
        if (data[line_counter]["category"].ToString() == "Interaction")
        {
            Debug.Log("Interaction");            
            TextPannel.SetActive(false);
            TextPannelOn=false;
            testCharactor.GetComponent<ClickMove3D>().enabled = true;//애니메이션 팀에서 set active 사용
            Quest.GetComponent<Quest_Manager>().Interaction_set();//퀘스트 호출

            line_counter++;
            return;
            // Scene열려 있는지 확인후 Scene이 열려 있으면 Scene객체에게  value전달
            //scene이 열려있지 않으면, Pannel이 열려 있는지 확인하고  Pannel객체를 끈다.->이후에 퀘스트 객체에게 호출
        }
        else if (data[line_counter]["category"].ToString() == "Animation")
        {   
            Debug.Log("Animation");
            TextPannel.SetActive(false);
            TextPannelOn=false;
            Quest.GetComponent<Quest_Manager>().Interaction_set();
            // testCharactor.GetComponent<ClickMove3D>().enabled = true;//애니메이션 팀에서 set active로 사용
            // testCharactor.GetComponent<DBtest>().StartAnime_DBtest(data[line_counter]["value"].ToString());//애니메이션 팀 plable로 변경

            line_counter++;
            return;
            // Scene열려 있는지 확인후 Scene이 열려 있으면 Scene객체에게  value전달
            //scene이 열려있지 않으면, Pannel이 열려 있는지 확인하고  Pannel객체를 끈다.->이후에 퀘스트 객체에게 호출
        }
        else if (data[line_counter]["category"].ToString() == "Scene_S")
        {
            Debug.Log("Scene_S");
            
            Quest.GetComponent<Quest_Manager>().Interaction_set();
            // if (!SceneOn)
            //     Quest.GetComponent<Quest_Manager>().Scene_set();
            // SceneOn=true;
            line_counter++;
            return;            
        }
        else if (data[line_counter]["category"].ToString() == "Scene_E")
        {
            Debug.Log("Scene_E");
            // if (SceneOn)
            //     Quest.GetComponent<Quest_Manager>().Scene_end();
                
            // SceneOn=false;
            
            Quest.GetComponent<Quest_Manager>().Interaction_set();
            line_counter++;
            return;
        }
        else if (data[line_counter]["category"].ToString() == "Reward")
        {
            Debug.Log("Reward");

            line_counter++;
            //Quest. 보상 패널을 활성화, 보상패널 종료시, 자동적으로 ReadCSV를 호출  
            return;
        }
        else
        {

            Debug.Log("script");
            // if(!TextPannelOn)
            TextPannel.SetActive(true);
            TextPannelOn=true;
            testCharactor.GetComponent<ClickMove3D>().enabled = false;//캐릭터 조작 비활성화 애니메이션 팀에서 변환
            text_manager.Action(data[line_counter]["value"],Player,NPC,"temp Sound Path");
            line_counter++;
            return;
        }
        
    
    
    }

/*
        // Update is called once per frame
    void Update()
    {
        if (line_counter < data.Count)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (data[line_counter]["category"].ToString() == "Interaction")
                {
                    
                    TextPannel.SetActive(false);
                    //Set.active(false) Panel
                    Debug.Log("Interaaction Called");
                    //Set.active(true) Panel
                }
                else if (data[line_counter]["category"].ToString() == "Animation")
                {
                    
                    TextPannel.SetActive(false);
                    //Set.active(false) Panel
                    Debug.Log("Animation");
                    testCharactor.GetComponent<DBtest>().StartAnime_DBtest(data[line_counter]["value"].ToString());
                    //Set.active(true) Panel
                }
                else if (data[line_counter]["category"].ToString() == "Scene_S")
                { 
                      Debug.Log("Scene_S");
                if (!SceneOn)
                    Quest.GetComponent<Quest_Manager>().Scene_set();
            
                    //Set.active(true) Scene_Panel
                }
                else if (data[line_counter]["category"].ToString() == "Scene_E")
                {
                    Debug.Log("Scene_E");
                    CloseScene();
                }
                else if (data[line_counter]["category"].ToString() == "Reward")
                {
                    Debug.Log("Reward Called");
                    //Call Reward
                }
                else
                {
                Debug.Log("script");
                if(!TextPannelOn)
                    TextPannel.SetActive(true);
                TextPannelOn=true;
                text_manager.Action(data[line_counter]["value"]);
                }
                line_counter++;
            }
        }
        else Debug.Log("QuestEnd");
    }
    
}
*/
