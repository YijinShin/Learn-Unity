using UnityEngine;
using System.Collections;
using System.Threading;
public class DisplayData : MonoBehaviour
{
	public Texture2D[] signalIcons;
	
	private int indexSignalIcons = 1;
	
    TGCConnectionController controller;

    public int poorSignal1;
    public int attention1;
    public int meditation1;
	public int blinkStrength;
	
	private float delta;

    void Start()
    {
				controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
	
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		controller.UpdateAttentionEvent += OnUpdateAttention;
		controller.UpdateMeditationEvent += OnUpdateMeditation;
		
		controller.UpdateDeltaEvent += OnUpdateDelta;

		// Thread thread = new Thread(threading);
		// thread.Start();
		
    }

	
	void OnUpdatePoorSignal(int value){
		poorSignal1 = value;
		if(value < 25){
      		indexSignalIcons = 0;
		}else if(value >= 25 && value < 51){
      		indexSignalIcons = 4;
		}else if(value >= 51 && value < 78){
      		indexSignalIcons = 3;
		}else if(value >= 78 && value < 107){
      		indexSignalIcons = 2;
		}else if(value >= 107){
      		indexSignalIcons = 1;
		}
	}
	void OnUpdateAttention(int value){
		attention1 = value;
	}
	void OnUpdateMeditation(int value){
		meditation1 = value;
	}
	void OnUpdateDelta(float value){
		delta = value;
	}

	void OnUpdateBlinkstrength(int value){
		blinkStrength = value;
	}

    void OnGUI()
    {
		GUILayout.BeginHorizontal();
		
		
        if (GUILayout.Button("Connect"))
        {
            controller.Connect();
        }
        if (GUILayout.Button("DisConnect"))
        {
            controller.Disconnect();
			indexSignalIcons = 1;
        }
		
		GUILayout.Space(Screen.width-250);
		//GUILayout.Label(signalIcons[indexSignalIcons]);
		
		GUILayout.EndHorizontal();

		
        GUILayout.Label("PoorSignal1:" + poorSignal1);
        GUILayout.Label("Attention1:" + attention1);
        GUILayout.Label("Meditation1:" + meditation1);
		GUILayout.Label("Delta:" + delta);
		GUILayout.Label("EyeBlink" + blinkStrength);

    }
}
