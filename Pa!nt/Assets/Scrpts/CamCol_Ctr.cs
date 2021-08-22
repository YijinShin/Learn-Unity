using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCol_Ctr : MonoBehaviour
{
    [SerializeField]
    private GameObject Cc;

    [SerializeField]
    private GameObject Ma;

    [SerializeField]
    private GameObject Gr;

    [SerializeField]
    private GameObject Lw;
    [SerializeField]
    private GameObject Rw;
    [SerializeField]
    private GameObject Ce;

    private PolygonCollider2D CamColider;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(new Vector2(Lw.transform.position.x -Cc.transform.position.x, Gr.transform.position.y - Cc.transform.position.y));
        Debug.Log(new Vector2(Rw.transform.position.x  - Cc.transform.position.x, Gr.transform.position.y- Cc.transform.position.y));
        Debug.Log(new Vector2(Rw.transform.position.x  - Cc.transform.position.x, Ce.transform.position.y - Cc.transform.position.y));
        Debug.Log(new Vector2(Lw.transform.position.x  - Cc.transform.position.x, Ce.transform.position.y - Cc.transform.position.y));
    }
}
