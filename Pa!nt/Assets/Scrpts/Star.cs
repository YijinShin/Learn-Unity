using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField]
    private GameObject highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        highlight.SetActive(true);
        if (collision.GetComponent<CH_move>().GetInteractive()) {
            if (collision.GetComponent<CH_move>().Check_ground()) {
                collision.GetComponent<ColorChange>().SetStar(true);
                this.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        highlight.SetActive(false);
    }
}
