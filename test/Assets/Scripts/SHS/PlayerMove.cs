using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CameraMove newCam; // CameraMove 스크립트에서 활성화되어있는 카메라 가져올 때 사용함
    Camera enabledCam; // 활성화된 카메라
    public float rotateSpeed;
    public float speed;
    float hAxis;
    float vAxis;
    int playerStatus; // 캐릭터 상태
    int talkIndex;
    int number;

    Vector3 moveVec;

    Animator anim;
    Rigidbody rigid;
    public int countInvestigate;
    public GameManager manager;
    public TalkManager talkManager;
    void Awake()
    {
        playerStatus = 0;
        newCam = GetComponent<CameraMove>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        countInvestigate = 0;
        //talkIndex = 0;
    }

    void Update()
    {
        Interaction();

        /*
        if(Input.GetKeyDown(KeyCode.Return))
        {
            manager.talkAction(talkIndex);
            Debug.Log("talkIndex : " + talkIndex);
            talkIndex++;
        }*/

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            manager.selectCriminal();
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {   
        enabledCam = newCam.arrCam[newCam.nNowCam];
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Vector3 forward = (enabledCam.transform.localRotation * Vector3.forward).normalized;
        Vector3 right = (enabledCam.transform.localRotation * Vector3.right).normalized;

        forward.y = 0;
        right.y = 0;

        if(newCam.nNowCam == 0)
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 메인 카메라 (탑뷰)일때만
        }

        else
        {
            moveVec = hAxis*right + vAxis*forward; // 카메라의 시점에 맞게 캐릭터 이동
        }
        transform.position += moveVec * speed * Time.deltaTime;
        
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }

    void Turn() // 캐릭터 천천히 회전
    {
        if(hAxis == 0 && vAxis == 0)
        {
            return;
        }
        Quaternion newRotation = Quaternion.LookRotation(moveVec);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    void Interaction()
    {
        RaycastHit rayHit;
        float hitDistance = 5.0f;

        Debug.DrawRay(transform.position, transform.forward * hitDistance, new Color(1, 0, 0));

        if(Physics.Raycast(transform.position, transform.forward * hitDistance, out rayHit, hitDistance, LayerMask.GetMask("Cube")))
        {
            GameObject hitTarget = rayHit.collider.gameObject;
            if(rayHit.distance < hitDistance)
            {
                changeStatus();
                Debug.Log(hitTarget.name);
                if(Input.GetButtonDown("Investigate"))
                {
                    manager.Action(hitTarget); // 플레이어 상태에 따라 UI 내에서 다른 내용
                    manager.menuAction(playerStatus); // 플레이어 상태에 따라 다른 UI
                    bool talking = manager.talkAction(countInvestigate, "Book", 1);

                    if(!talking)
                    {
                        countInvestigate++;
                        Debug.Log(countInvestigate);
                    }
                }
            }
        }
    }

    void changeStatus() // 플레이어 상태 변경
    {
        if(Input.GetKeyDown("x"))
        {
            playerStatus = 0;
        }

        else if(Input.GetButtonDown("Jump"))
        {
            playerStatus = 1;
        }

        else if(Input.GetButtonDown("Sitting"))
        {
            playerStatus = 2;
        }

        Debug.Log("Player's Status : " + playerStatus);
    }

    public void init_countInvestigate()
    {
        countInvestigate = 0;
        Debug.Log("무야호~");
    }
}
