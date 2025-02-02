using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class EnemyContollor_Z : MonoBehaviour
{
    public bool Boss = false;

    float Timer;
    bool isWalk;
    float SetRot;
    float rotSpeed = 4;
    float attackCount = 0;
    
    public float ChangeTime;
    public float EnemySpeed;

    public float AtDistance = 1f;
    GameObject Target;
    public Animator animator;
    public Rigidbody rb;
    public Collider WeaponColl;
    public StatusManeger StatusManeger;
    public GameObject Magic;
    public Transform MagicPos;

    PlayerContoroller playerContoroller;

    SphereCollider sphereCollider;

    GameObject player;

    // Start is called before the first frame update
    private void Start()
    {

        sphereCollider = GetComponent<SphereCollider>();
        //敵ごとにばらつかせる
        ChangeTime += Random.Range(-0.5f, 1.55f);
;       //敵がどの方向を向くか
        SetRot = Random.Range(0, 360);
        //初期値は止まった状態でスポーン
        WalkSet(false);

        player = GameObject.Find("Player"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
        playerContoroller = player.GetComponent<PlayerContoroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            Chase();
        }
        else
        {
            Search();
        }
    }    

　　//プレイヤーを追いかける
    void Chase()
    {
        //Playerのいる方向をセット
        var _dir = Target.transform.position - transform.position;
        SetRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;

        DirSet();


        //攻撃範囲の判定
        float dis = Vector3.Distance(Target.transform.position, transform.position);
        if(dis > AtDistance)
        {
            //プレイヤーと同じ速さに設定
            float ChaceSpeed = playerContoroller.PlayerSpeed;
            //追従
            this.transform.Translate(Vector3.forward * ChaceSpeed);
            RunSet(true);
            WalkSet(false);
            Debug.Log("攻撃範囲外です");
        }
        else
        {
            //攻撃開始
            RunSet(false);
            Attack();
            Debug.Log("攻撃範囲内です");
            sphereCollider.enabled = false;
            Invoke("colliderON", 1f);
        }
    }

    //プレイヤーがいない時徘徊
    void Search()
    {
        Timer += Time.deltaTime;
        
        RunSet(false);

        //一定時間で方向セット
        if (ChangeTime <= Timer)
        {
            Timer = 0;
            SetRot = Random.Range(0, 360);
            WalkSet(false);
        }
        else if((ChangeTime / 2) <= Timer)
        {
            this.transform.Translate(Vector3.forward * EnemySpeed);
            WalkSet(true);
        }
        else
        {
            DirSet();
        }
    }

    //ランダムな攻撃パターン
    void Attack()
    {
        attackCount += Time.deltaTime;
        if (attackCount >= 1)
        {
            attackCount = 0;

            var _rand = Random.Range(0, 3);
            
            if (_rand == 0)
            {
                animator.SetBool("attack0", true);
            }
            else if (_rand == 1)
            {
                animator.SetBool("attack1", true);
            }
            else if (_rand == 2)
            {
                animator.SetBool("attack2", true);
            }
        }
    }

    //プレイヤーの判定
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
         {
            Target = other.gameObject;
         }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Target = null;
        }
    }

    //歩き状態
    void WalkSet(bool _isWalk)
    {
        if (_isWalk)
        {
            animator.SetBool("walk", true);
            Debug.Log("歩いてます");

        }
        else if(!_isWalk)
        {
            animator.SetBool("walk", false);
            Debug.Log("止まってます");
        }
    }

    //走り状態
    void RunSet(bool _isWalk)
    {
        if (_isWalk)
        {
            animator.SetBool("run", true);
            Debug.Log("走ってます");

        }
        else if (!_isWalk)
        {
            animator.SetBool("run", false);
            Debug.Log("止まってます");
        }
    }

    void DirSet()
    {
        //なめらかに回転
        var _angle = Mathf.LerpAngle(transform.eulerAngles.y, SetRot, rotSpeed * Time.deltaTime);
        var _rot = Vector3.zero;
        _rot.y = _angle;
        transform.eulerAngles = _rot;
    }

    void colliderON()
    {
        sphereCollider.enabled = true;
    }
}







