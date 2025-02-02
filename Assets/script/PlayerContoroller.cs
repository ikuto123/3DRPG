using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerContoroller : MonoBehaviour
{
    //アイテム関係
    [SerializeField] GameObject MenuWindow;
    private bool MenuFlag;
    [SerializeField] GameObject ItemBoxManager;

    NPCManager npcManager;
    GameObject NPC;

    public Transform Camera;
    public float PlayerSpeed;
    public float RotationSpeed;

    //ジャンプのための定義
    private Rigidbody Rigidbody;
    private CapsuleCollider col;
    public float JumpPower = 10.0f;
    public LayerMask groundLayers;
    public float groundCheckRadium = 0.1f;

    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    public Animator PlayerAnimator;
    bool isRun;
    bool isWalk;
    private bool NPCFlag;

    public Collider WeaponCollider;
    bool canMove = true;

    public AudioSource audioSource;
    public AudioClip AttackSE;
    // Start is called before the first frame update
    void Start()
    {
        NPC = GameObject.Find("NPC_Priest");
        npcManager = NPC.GetComponent<NPCManager>();
        Rigidbody = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        isRun = false;
        isWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Run();
        Rotation();
        Jump();
        Attack();
        Menu_Window();
        Camera.transform.position = transform.position;
        Debug.Log(isRun);

    }
    //プレイヤーのキー操作
    void Walk()
    {
        if (!canMove || (Time.timeScale == 0)) 
        {
            return;
        }
        speed = Vector3.zero;
        rot = Vector3.zero;
        isWalk = false;

        if (Input.GetKey(KeyCode.W))
        {
            rot.y = 0;
            WalkSet();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rot.y = -90;
            WalkSet();
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot.y = 90;
            WalkSet();
        }
        if (Input.GetKey(KeyCode.S))
        {
            rot.y = 180;
            WalkSet();
        }
        transform.Translate(speed);
        //アニメーターのフラグにboolがたを渡す
        WalkAnim(isWalk);
        RunAnim(isRun);
    }

    void Run()
    {
        if (!canMove || (Time.timeScale == 0))
        {
            return;
        }
        speed = Vector3.zero;
        rot = Vector3.zero;
        isRun = false;
        
       

        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            rot.y = 0;
            RunSet();
        }
        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            rot.y = -90;
            RunSet();
        }
        if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            rot.y = 90;
            RunSet();
        }
        if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            rot.y = 180;
            RunSet();
        }


        transform.Translate(speed);
        //アニメーターのフラグにboolがたを渡す
        RunAnim(isRun);
        WalkAnim(isWalk);
    }

    void Jump()
    {
        //ジャンプ
        if (Input.GetKey(KeyCode.Space) && IsGround())
        {
            Rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    //プレイヤーがカメラの方向を向く
    void WalkSet()
    {
        speed.z = PlayerSpeed * 0.5f;
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
        isWalk = true;
        isRun = false;
    }

    void RunSet()
    {
        speed.z = PlayerSpeed ;
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
        isRun = true;
        isWalk = false;
    }

    //プレイヤーが回転しても前を向く
    void Rotation()
    {
        var speed = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed.y = -RotationSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed.y = RotationSpeed;
        }

        Camera.transform.eulerAngles += speed;
    }

    void Attack()
    {
        NPC = GameObject.Find("NPC_Priest");

        //左クリックで攻撃
        if (Input.GetMouseButtonDown(0))
        {
            //NPCの範囲にある場合攻撃できない
            npcManager = NPC.GetComponent<NPCManager>();
            NPCFlag = npcManager.NPCFlag;
            if (NPCFlag == true)
            {
                return;
            }
            PlayerAnimator.SetBool("attack", true);
            WalkAnim(false);
            RunAnim(false);
            canMove = false;
            
        }
    }

    //メニューウィンドウの管理
    void Menu_Window()
    {
 

        if (Input.GetKeyDown(KeyCode.M))
        {
            switch(MenuFlag)
            {
                case false:
                    GameObject.Find("StateusWndowManeger").GetComponent<StatusWndowManeger>().GetStatus();
                    ItemBoxManager.GetComponent<ItemBoxManeger>().ItemOpen();
                    MenuWindow.SetActive(true);
                    Time.timeScale = 0;
                    MenuFlag = true;
                    break;
                case true:
                    //ItemBoxManager.GetComponent<ItemBoxManeger>().ItemClose();
                    MenuWindow.SetActive(false);
                    Time.timeScale = 1;
                    MenuFlag = false;
                    break;
            }
        }
    }

    void WeaponOn()
    {
        WeaponCollider.enabled = true;
        audioSource.PlayOneShot(AttackSE);
    }

    void WeaponOff()
    {
        WeaponCollider.enabled = false;
        PlayerAnimator.SetBool("attack", false);

    }

    void CanMove()
    {
        canMove = true;
        WalkAnim(true);
        RunAnim(true);
    }

    //ジャンプできるかの判定
    private bool IsGround()
    {
        Vector3 groundCheckPostion = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        return Physics.CheckSphere(groundCheckPostion, groundCheckRadium, groundLayers);
    }

    void RunAnim(bool _isRun)
    {
        if (_isRun)
        {
            PlayerAnimator.SetBool("run", true);
        }
        else if (!_isRun)
        {
            PlayerAnimator.SetBool("run", false);
        }

    }

    void WalkAnim(bool _isWalk)
    {
        if (_isWalk)
        {
            PlayerAnimator.SetBool("walk", true);
        }
        else if (!_isWalk)
        {
            PlayerAnimator.SetBool("walk", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            ItemBoxManager.GetComponent<ItemBoxManeger>().GetItem = other.gameObject.GetComponent<ItemManeger>().ItemNumber;
            ItemBoxManager.GetComponent<ItemBoxManeger>().ItemGet();
        }
    }
}
