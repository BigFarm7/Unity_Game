using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject hamm;
    public Camera mainCam;
    public GameObject Cam;
    public float speed = 10f;
    public float rotateSpeed = 10.0f;
    public bool isRightClick = false;
    public Animator anim;
    public GunController _gunController;
    public float forwardOffset = 0.2f;
    public float backwardOffset = 0.2f; 
    public float leftOffset = 0.2f; 
    public float rightOffset = 0.2f; 

    Vector3 dir, Cam_dir, L_Cam_dir, U_Cam_dir, Normalize_Cam_dir;
    private float h, v;
    private Rigidbody rigid;
    private float VerMove;
    private float HorMove;
    private Vector3 moveVec;


    bool b_Run;
  

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 0);
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // Rigidbody 설정 변경
        rigid.mass = 10f; // 질량 증가
        rigid.drag = 5f; // 공기 저항 증가
        rigid.angularDrag = 5f; // 회전 저항 증가
    }
    void Update()
    {

        if (isRightClick)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
        }
        if (Input.GetMouseButtonDown(1) && !hamm.activeSelf)
        {
            if (!isRightClick)
            {

                anim.SetBool("isRightClick", true);
                isRightClick = true;
            }
            else
            {

                anim.SetBool("isRightClick", false);
                isRightClick = false;
            }
        }

        // transform.LookAt(transform.position + moveVec);
        if (isRightClick)
            AimAtMouse();


    }

    void FixedUpdate()
    {
        VerMove = Input.GetAxisRaw("Vertical");
        HorMove = Input.GetAxisRaw("Horizontal");
        b_Run = Input.GetButton("Run");
        moveVec = new Vector3(HorMove, 0, VerMove).normalized;
        //transform.position += moveVec * speed *(b_Run ? 2f : 1f)* Time.deltaTime;

        anim.SetBool("isForward", moveVec != Vector3.zero);
        anim.SetBool("isRun", b_Run);

        Cam_dir = this.transform.position - Cam.transform.position;
        Normalize_Cam_dir = Cam_dir.normalized;
        U_Cam_dir = Normalize_Cam_dir - new Vector3(0, Normalize_Cam_dir.y, 0);
        L_Cam_dir = new Vector3(-Normalize_Cam_dir.z, 0, Normalize_Cam_dir.x);

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isFront", true);
            dir = U_Cam_dir;
            Turn();
        }
        else
        {
            anim.SetBool("isFront", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isBack", true);
            dir = -U_Cam_dir;
            Turn();
        }
        else
        {
            anim.SetBool("isBack", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isLeft", true);
            dir = L_Cam_dir;
            Turn();
        }
        else
        {
            anim.SetBool("isLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRight", true);
            dir = -L_Cam_dir;
            Turn();
        }
        else
        {
            anim.SetBool("isRight", false);
        }


    }

    void AimAtMouse()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
    
            Vector3 targetPosition = hitInfo.point;

        
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; 

           
            if (isRightClick)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation*Quaternion.Euler(0,31,0);
            }
           

        }
    }
    void Turn()
    {
        if (isRightClick)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); 
            float verticalInput = Input.GetAxis("Vertical");

         
            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

          
            if (verticalInput > 0) 
            {
                moveDirection += transform.forward + transform.right * forwardOffset;
            }
            else if (verticalInput < 0)
            {
                moveDirection += -transform.forward + -transform.right * backwardOffset;
            }

            if (horizontalInput > 0) 
            {
                moveDirection += transform.right + transform.forward * rightOffset;
            }
            else if (horizontalInput < 0)
            {
                moveDirection += -transform.right + -transform.forward * leftOffset;
            }

           
            if (moveDirection.magnitude > 1f)
            {
                moveDirection.Normalize();
            }

       
            rigid.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        }
        
        if (!isRightClick)
        {
            transform.position += dir.normalized * speed * (b_Run ? 2f : 1f) * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }

}
