using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Group_setter : MonoBehaviour
{
    Cinemachine.CinemachineTargetGroup.Target target;

    // Start is called before the first frame update
    void Start()
    {
        target.target = GameObject.Find("Player").transform;
        target.weight = 1;
        target.radius = 0;
        this.GetComponent<CinemachineTargetGroup>().m_Targets[0] = target;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
