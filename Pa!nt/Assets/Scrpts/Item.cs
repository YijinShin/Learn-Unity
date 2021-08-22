using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int item;
    
    [SerializeField]
    private GameObject highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        highlight.SetActive(true);
        if (collision.GetComponent<CH_move>().GetInteractive())
        {
            if (collision.GetComponent<CH_move>().Check_ground()&&0==collision.GetComponent<ColorChange>().GetItem())
            {
                collision.GetComponent<ColorChange>().SetItem(item);
                Destroy(this.gameObject);
            }
        }
    }
       private void OnTriggerExit2D(Collider2D collision)
    {
        highlight.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
