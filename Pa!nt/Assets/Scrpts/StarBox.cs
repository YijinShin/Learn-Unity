using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StarBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Sprite Filled;
    [SerializeField]
    private string nextScene;

    [SerializeField]
    private GameObject highlight;
    void Start()
    {
        
    }
    void Update(){
        if(Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(Input.GetAxis("Cancel")!=0)
            Application.Quit();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        highlight.SetActive(true);
        if (collision.GetComponent<CH_move>().GetInteractive())
        {
            if (collision.GetComponent<CH_move>().Check_ground()&&collision.GetComponent<ColorChange>().GetStar())
            {
                collision.GetComponent<ColorChange>().SetStar(false);
                this.GetComponent<SpriteRenderer>().sprite = Filled;

                Invoke("End",1);
            }
        }
    }
         private void OnTriggerExit2D(Collider2D collision)
    {
        highlight.SetActive(false);
    }
    private void End() {
        SceneManager.LoadScene("Stage_"+nextScene.ToString());
    }
}
