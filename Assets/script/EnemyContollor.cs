using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyContollor : MonoBehaviour
{

    float Timer;
    public float ChangeTime;
    public float EnemySpeed;

    GameObject Target;
    public Animator PlayerAnimator;
    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        var speed = Vector3.zero;
        speed.z = EnemySpeed;
        var rot = transform.eulerAngles;


        if (Target)
        {
            transform.LookAt(Target.transform);
            rot = transform.eulerAngles;

        }
        else
        {
            Timer += Time.deltaTime;
            if (ChangeTime <= Timer)
            {
                float rand = Random.Range(0, 360);
                rot.y = rand;
                Timer = 0;
            }
        }

        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        this.transform.Translate(speed);

    }

    //サークルコライダー内に入った場合に追従
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Target = other.gameObject;

        }
    }

    //サークルコライダーから出たら追従をやめる。
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Target = null;
        }
    }
}
