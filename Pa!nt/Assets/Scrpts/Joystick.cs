using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform rect_Background;
    public RectTransform rect_Joystick;

    private float radius;
    public GameObject Player;
    public float moveSpeed;

    //private bool isTouch = false;
    private Vector3 movePosition;

    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }

    void Update()
    {
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;
        
        value = Vector2.ClampMagnitude(value, radius); // 가두기
        rect_Joystick.localPosition = value;
        
        float distance = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;
        value = value.normalized;
        //movePosition = new Vector2(value.x * moveSpeed * distance * Time.deltaTime, 0);
        
        if(value.x < 0)
        {
            Player.GetComponent<CH_move>().SetLeft(true);
            Player.GetComponent<CH_move>().SetRight(false);

            if (value.y > 0.5)
            {
                //Player.GetComponent<CH_move>().SetJump(true);
            }

            Debug.Log("left");
        }

        if(value.x > 0)
        {
            Player.GetComponent<CH_move>().SetRight(true);
            Player.GetComponent<CH_move>().SetLeft(false);

            if (value.y > 0.5)
            {
                //Player.GetComponent<CH_move>().SetJump(true);
            }
            Debug.Log("right");
        }

        else if (value.y > 0.5)
        {
            //Player.GetComponent<CH_move>().SetJump(true);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //isTouch = false;
        rect_Joystick.localPosition = Vector2.zero;
        movePosition = Vector2.zero;

        Player.GetComponent<CH_move>().SetLeft(false);
        Player.GetComponent<CH_move>().SetRight(false);
        Player.GetComponent<CH_move>().Set_Interact(false);
        Player.GetComponent<CH_move>().SetJump(false);
    }
}
