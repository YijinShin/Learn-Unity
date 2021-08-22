using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Bowl : MonoBehaviour
{
    [SerializeField]
    private GameObject highlight;
    Color G;
    Color Y;
    Color R;
    Color W;
    [SerializeField]
    private int state;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        W.r = 0.3f;
        W.g = 0.8833333f;
        W.b = 1;
        W.a = 1;
        R.r = 1f;
        R.g = 0.3f;
        R.b = 0.3f;
        R.a = 1;
        Y.r = 1f;
        Y.g = 0.8830066f;
        Y.b = 0.2980392f;
        Y.a = 1;
        G.r = 0.5320261f;
        G.g = 1f;
        G.b = 0.2980392f;
        G.a = 1;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (state == 8)
        {
            this.spriteRenderer.color = G;
        }
        else if (state == 9)
        {
            this.spriteRenderer.color = Y;
        }
        else if (state == 10)
        {
            this.spriteRenderer.color = R;
        }
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        highlight.SetActive(true);

        if (collision.GetComponent<CH_move>().GetInteractive()&& collision.tag=="Player"&&collision.GetComponent<CH_move>().Check_ground())
        {
            if (state == 0)
            {
                if (collision.gameObject.GetComponent<ColorChange>().GetItem() != 0)
                {
                    state = collision.gameObject.GetComponent<ColorChange>().GetItem();
                    collision.gameObject.GetComponent<ColorChange>().SetItem(0);
                }
            }
            if (state != 0) {
                collision.gameObject.layer = state;

            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        highlight.SetActive(false);
    }

}
