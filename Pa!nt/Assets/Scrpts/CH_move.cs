using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_move : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform trans;
    float X;
    bool InJump;
    [SerializeField]
    GameObject Ground;
    [SerializeField]
    bool IsGround;
    Animator anime;
    [SerializeField]
    KeyCode jump;
    [SerializeField]
    float Jump_power;
    [SerializeField]
    float Move_speed;
    [SerializeField]
    bool SuperJump;
    [SerializeField]
    int Mode;

    [SerializeField]
    static int ButtonMode=1;

    [SerializeField]
    private bool Interactive;
    [SerializeField]

    private bool InputLeft;
    [SerializeField]

    private bool InputRight;
    // Start is called before the first frame update
    void Start()
    {
        trans = this.GetComponent<Transform>();
        anime = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Mode == 0)
        {
            GetInputsFromKeyboards();
        }
        else if (Mode == 1) {
            GetInputsFromUI_1();
        }
    }
    void GetInputsFromUI_1() {
    }
    void GetInputsFromKeyboards() {

        InJump = false;
        Interactive = false;
        if (0 < Input.GetAxis("Horizontal"))
        {
            InputRight = true;
            InputLeft = false;
        }
        else if (0 > Input.GetAxis("Horizontal"))
        {
            InputLeft = true;
            InputRight = false;
        }
        else
        {
            InputLeft = false;
            InputRight = false;
        }
       
        if (Input.GetKey(jump))
        {
            InJump = true;
        }
        if (Input.GetAxis("Interaction") != 0) {
            Interactive = true;
        }
    }

  
    void FixedUpdate()
    {
        Jump();
        Move();   
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground") {
            if (!IsGround)
            {
                if (!SuperJump) {
                    if (rigid.velocity.y != 0)
                        return;                        
                }
                Ground.SetActive(true);
                anime.SetBool("Jump", false);
                IsGround = true;
            }
        }
    }

    public void SetLeft(bool input) {
        InputLeft = input;
    }
    public void SetRight(bool Input) {
        InputRight = Input;
    }
    public void SetJump(bool Jump) {
        InJump = Jump;
    }
    public void Set_Interact(bool interact) {
        if(interact)
        Debug.Log("WSHy");
        if (!interact)
            Debug.Log("WHy");
        Interactive = interact;
        if (Interactive==true) {
            Debug.Log("Work");
        }
    }
    public bool GetInteractive()
    {
        return Interactive;
    }
    private void Move()
    {
        if (InputLeft || InputRight)
        {
            anime.SetBool("Working", true);
        }
        else {
            anime.SetBool("Working", false);
            X = 0;
        }

        if (InputLeft) {
                X = -1;
            }
            if (InputRight)
            {
                X = 1;
            }
        trans.Translate(Vector3.right*X*Move_speed*Time.deltaTime);
      }
    private void Jump() {

        if (IsGround && InJump&&rigid.velocity.y<20)
        {
            rigid.AddForce(transform.up * Jump_power, ForceMode2D.Impulse);
            IsGround = false;
            anime.SetBool("Jump", true);
            Ground.SetActive(false);
        }
    }
    private void Jump_Change() {
        anime.SetBool("SJump",true);
    }
    private void Landing_Change() {
        anime.SetBool("SJump",false);

    }
    public bool Check_ground() {
        return IsGround;
    }

    public void SetButtonMode(int new_BtnMode){
        ButtonMode = new_BtnMode;
    }
    public int GetButtonMode(){
        return ButtonMode;
    }
}
