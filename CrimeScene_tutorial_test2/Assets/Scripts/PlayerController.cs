using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed; // inspector창에서 직접 설정 가능하도록 public
    
    [SerializeField]
    private Camera cam;   // 카메라

    void Start()
    {

    }

    void Update()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 move = (cam.transform.right * _moveDirX + cam.transform.forward * _moveDirZ).normalized;
        //이동 
        transform.position  +=move * speed * Time.deltaTime;
        //플레이어가 나가아는 방향을 바라보도록 
        transform.LookAt(transform.position + move);
    }
}
