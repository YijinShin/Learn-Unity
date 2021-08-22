using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField]
    private Transform ground_T;

    [SerializeField]
    private Transform leftWall_T;

    [SerializeField]
    private Transform rightWall_T;

    [SerializeField]
    private Transform celling_T;

    [SerializeField]
    private Camera camera;

    float zPos;

    void Start(){
        float middleY = (ground_T.transform.position.y+celling_T.transform.position.y)/2;
        float middleX = (leftWall_T.transform.position.x+rightWall_T.transform.position.x)/2;
        zPos = -20f;
        transform.position = new Vector3(middleX, middleY, zPos);

        // Debug.Log(screenPos.x);
    }

    void Update(){
        TryViewTotalMap();
    }

    void TryViewTotalMap(){
        Vector3 viewGround = camera.WorldToViewportPoint(ground_T.position);
        Vector3 viewLeftWall = camera.WorldToViewportPoint(leftWall_T.position);
        Vector3 viewRightWall = camera.WorldToViewportPoint(rightWall_T.position);
        Vector3 viewCelling = camera.WorldToViewportPoint(celling_T.position);

        if(viewGround.y < 0 || viewCelling.y>1 || viewLeftWall.x<0 || viewRightWall.x>1){
            zPos -= 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }

        
    }
    // void TryMiniMap(){
    //     CheckObjectIsInCamera(ground_T);
    // }
}
