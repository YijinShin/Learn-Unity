using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Color G;
    Color Y;
    Color R;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private int Having_item;
    private bool star;
    int layer;
    [SerializeField]
    private GameObject Child;
    [SerializeField]
    private GameObject Star;
    [SerializeField]
    List<Sprite> Items;
    // Start is called before the first frame update
    void Start()
    {
        star = false;
        Having_item = 0;
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


    public void SetItem(int item) {
        Having_item = item;
    }
    public int GetItem()
    {
        return Having_item;
    }
    public void SetStar( bool temp) {
        star = temp;
        Star.SetActive(temp);
    }
    public bool GetStar() {
        return star;
    }
    // Update is called once per frame
    void Update()
    {
        LayerChecker();
        ItemChecker();
    }
    void ItemChecker() 
    {
        if (Having_item != 0)
        {
            Child.GetComponent<SpriteRenderer>().sprite = Items[Having_item - 7];
        }
        else
            Child.GetComponent<SpriteRenderer>().sprite = Items[0];
    }
    void LayerChecker() 
    {
        layer = this.gameObject.layer;
        if (layer == 8)
        {

            spriteRenderer.color = G;
        }
        else if (layer == 9)
        {

            spriteRenderer.color = Y;
        }
        else if (layer == 10)
        {

            spriteRenderer.color = R;
        }
    }
}
