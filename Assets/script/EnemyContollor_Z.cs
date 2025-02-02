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
        //�G���Ƃɂ΂������
        ChangeTime += Random.Range(-0.5f, 1.55f);
;       //�G���ǂ̕�����������
        SetRot = Random.Range(0, 360);
        //�����l�͎~�܂�����ԂŃX�|�[��
        WalkSet(false);

        player = GameObject.Find("Player"); //Unity�������I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
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

�@�@//�v���C���[��ǂ�������
    void Chase()
    {
        //Player�̂���������Z�b�g
        var _dir = Target.transform.position - transform.position;
        SetRot = Mathf.Atan2(_dir.x, _dir.z) * Mathf.Rad2Deg;

        DirSet();


        //�U���͈͂̔���
        float dis = Vector3.Distance(Target.transform.position, transform.position);
        if(dis > AtDistance)
        {
            //�v���C���[�Ɠ��������ɐݒ�
            float ChaceSpeed = playerContoroller.PlayerSpeed;
            //�Ǐ]
            this.transform.Translate(Vector3.forward * ChaceSpeed);
            RunSet(true);
            WalkSet(false);
            Debug.Log("�U���͈͊O�ł�");
        }
        else
        {
            //�U���J�n
            RunSet(false);
            Attack();
            Debug.Log("�U���͈͓��ł�");
            sphereCollider.enabled = false;
            Invoke("colliderON", 1f);
        }
    }

    //�v���C���[�����Ȃ����p�j
    void Search()
    {
        Timer += Time.deltaTime;
        
        RunSet(false);

        //��莞�Ԃŕ����Z�b�g
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

    //�����_���ȍU���p�^�[��
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

    //�v���C���[�̔���
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

    //�������
    void WalkSet(bool _isWalk)
    {
        if (_isWalk)
        {
            animator.SetBool("walk", true);
            Debug.Log("�����Ă܂�");

        }
        else if(!_isWalk)
        {
            animator.SetBool("walk", false);
            Debug.Log("�~�܂��Ă܂�");
        }
    }

    //������
    void RunSet(bool _isWalk)
    {
        if (_isWalk)
        {
            animator.SetBool("run", true);
            Debug.Log("�����Ă܂�");

        }
        else if (!_isWalk)
        {
            animator.SetBool("run", false);
            Debug.Log("�~�܂��Ă܂�");
        }
    }

    void DirSet()
    {
        //�Ȃ߂炩�ɉ�]
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







