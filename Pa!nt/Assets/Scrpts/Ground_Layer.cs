using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Layer : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.layer = Player.layer;
    }
}
