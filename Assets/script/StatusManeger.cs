using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Player���U�������G�̊Ǘ�
public class StatusManeger : MonoBehaviour
{

    [SerializeField] EnemyStatesOS enemyStatesOS;
    [SerializeField] PlayerStatesOS playerStatesOS;
    [SerializeField] WeponOS weponOS;

    public GameObject Main;
    
    public int enemyNumber;
    int enemyHP;

    int damage;
    int Wepondamage;

    public Image HPGage;
    public GameObject Effct;
    public AudioSource AudioSource;
    public AudioClip HitSE;
    public float ResetTime = 0; 

    public string Tagname;

    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        //�E�F�|��Number�̎擾
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();

        enemyHP = enemyStatesOS.EnemyStatusList[enemyNumber].HP;
        Wepondamage = weponOS.wepondamage[wepon.WeponNumber].Attack;
        //�I�u�W�F�N�g�ɂ��Ă���R���C�_�[�̎擾
        collider = GetComponent<Collider>();
    }
    void Update()
    {
        WeponNumber();

        //���񂾂Ƃ�
        if (enemyHP <= 0)
        {
            enemyHP = 0;
            var effect = Instantiate(Effct);
            effect.transform.position = transform.position;
            Destroy(effect, 5);
            Destroy(Main);
        }

        float percent =  (float)enemyHP / enemyStatesOS.EnemyStatusList[enemyNumber].MaxHP;
        HPGage.fillAmount = percent;

    }
    //�U�����������������̏���
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tagname)
        {
            Damege();
            //�{�b�N�X�R���C�_�[��off
            collider.enabled = false;
            //2�b��ɓ����蔻�蕜��
            Invoke("ColliderReset", ResetTime);
            Debug.Log("�U����������܂���");
        }
    }

    void Damege()
    {
        AudioSource.PlayOneShot(HitSE);
        
        //����_���[�W�ƃv���[���[�_���[�W�̉��Z,�h��͂�����
        damage = (int)(playerStatesOS.Attack + Wepondamage)/2 - enemyStatesOS.EnemyStatusList[enemyNumber].Defence / 4;

        //�h��͂����������ۂɓG���񕜂��Ȃ��悤��
        if(damage > 0)
        {
            enemyHP -= damage;
        }
    }

    void ColliderReset()
    {
        collider.enabled = true;    
    }

    //����̃i���o�[�Q��
    void WeponNumber()
    {
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();
        Debug.Log(wepon.WeponNumber);
    }
}
