using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_CH_Controller : MonoBehaviour
{
    public Tutorial_GameManager GameManager;
    public float moveSpeed;

    public Transform player;
    public Transform cameraArm;

    Vector3 moveDir;

    float hAxis;
    float vAxis;

    bool isSitting;
    bool isJumping;
    bool isBorder;

    // Start is called before the first frame update
    void Start()
    {
        isSitting = false;
        isJumping = false;
        isBorder = false;

    }

    // Update is called once per frame
    void Update()
    {
        camRotate();
        Interact();
        changeStatus();
    }

    void FixedUpdate()
    {
        Stop();
        Move();   
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isMove = moveInput.magnitude != 0;

        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            player.forward = moveDir;

            if(!isBorder && !isSitting && !isJumping)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
        }

        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }
   
    void camRotate()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 camAngle = cameraArm.rotation.eulerAngles;

            float x = camAngle.x - mouseDelta.y;

            if(x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }
            
            cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        }
    }

    void changeStatus() // 플레이어 상태 변경
    {
        if(Input.GetKeyDown("z"))
        {
            if(isSitting)
            {
                isSitting = false;
                isJumping = false;
                player.position = new Vector3(player.position.x, player.position.y + 10, player.position.z);
                Debug.Log("Stand Up!!");
            }

            else
            {
                isSitting = true;
                isJumping = false;
                player.position = new Vector3(player.position.x, player.position.y - 10, player.position.z);

                if(isJumping)
                {
                    player.position = new Vector3(player.position.x, player.position.y - 10, player.position.z);
                }

                Debug.Log("Sitting!");
                
            }
        }

        else if(Input.GetKeyDown("x"))
        {
            if(isJumping)
            {
                isJumping = false;
                isSitting = false;
                player.position = new Vector3(player.position.x, player.position.y - 10, player.position.z);
                Debug.Log("Stand Up!!");
            }

            else
            {
                isJumping = true;
                isSitting = false;
                if(isSitting)
                {
                    player.position = new Vector3(player.position.x, player.position.y + 10,player.position.z);
                }
                player.position = new Vector3(player.position.x, player.position.y + 10, player.position.z);
                
                Debug.Log("Jumping!");
            }
        }
    }

    void Interact()
    {
        RaycastHit rayHit;
        float hitDistance = 30.0f;

        Debug.DrawRay(player.position, player.forward * hitDistance, Color.red);

        if(Physics.Raycast(player.position, player.forward * hitDistance, out rayHit, hitDistance, LayerMask.GetMask("Item")))
        {
            GameObject hitTarget = rayHit.collider.gameObject;
            if(rayHit.distance < hitDistance)
            {
                Debug.Log(hitTarget.name);
                if(Input.GetButtonDown("Investigate"))
                {
                    if(GameManager.get_talkIndex() == 26 && isSitting && hitTarget.name == "Diary")
                    {
                        GameManager.showDiary_UI();
                    }

                    else if(GameManager.get_talkIndex() == 35 && isJumping && hitTarget.name == "Letter")
                    {
                        GameManager.showLetter_UI();
                    }

                    else if(GameManager.get_talkIndex() == 41 && !isJumping && !isSitting && hitTarget.name == "Smartphone_A")
                    {
                        GameManager.show_Smartphone_A_UI();
                    }

                    else if(GameManager.get_talkIndex() == 57 && !isJumping && !isSitting && hitTarget.name == "Smartphone_B")
                    {
                        GameManager.show_Smartphone_B_UI();
                    }

                    else if(GameManager.get_talkIndex() == 65 && !isJumping && !isSitting && hitTarget.name == "Knife")
                    {
                        GameManager.showKnife_UI();
                    }

                    else if(GameManager.get_talkIndex() == 69 && !isJumping && !isSitting && hitTarget.name == "Airplane")
                    {
                        GameManager.showAirplane_UI();
                    }
                    
                }
            }
        }
    }

    void Stop()
    {
        RaycastHit rayHit;
        float hitDistance = 30.0f;

        Debug.DrawRay(player.position, player.forward * hitDistance, Color.red);
        isBorder = (Physics.Raycast(player.position, player.forward * hitDistance, out rayHit, hitDistance, LayerMask.GetMask("Wall"))) || (Physics.Raycast(player.position, player.forward * hitDistance, out rayHit, hitDistance, LayerMask.GetMask("Item")));
    }
    
}
