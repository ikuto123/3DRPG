using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;
using static WeponOS;

public class PlayerStatesManeger : MonoBehaviour
{
    public GameObject Main;

    GameObject lightning;
    ParticlHit part;

    [SerializeField] PlayerStatesOS playerStatesOS;
    [SerializeField] WeponOS weponOS;
    [SerializeField] EnemyStatesOS enemyStatesOS;

    StatusManeger statusManeger;

    int damage;
    int Wepondamage;
    int enemyNumber;

    //HP�\�L
    [SerializeField] Text HPText;
    [SerializeField] Text MPText;
    public int currentHP;
    public int currentMP;
    public int MaxHP;
    public int MaxMP;
    public int Attack;
    public int Defance;

    public Image HPGage;
    public Image MPGage;
    public GameObject Effct;
    public AudioSource AudioSource;
    public AudioClip HitSE;
    public float ResetTime = 0;

    bool isMagic;

    public string Tagname;

    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        //lightning = GameObject.Find("Lightning");
        //part = lightning.GetComponent<ParticlHit>();

        //������̎擾
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();
        Wepondamage = weponOS.wepondamage[wepon.WeponNumber].Attack;

        //enemyNumber�̎擾
        GameObject enemy = GameObject.Find("StatusManeger");
        statusManeger = enemy.GetComponent<StatusManeger>();
        enemyNumber = statusManeger.enemyNumber;

        //OS����l�̏�����
        currentHP = playerStatesOS.HP;
        currentMP = playerStatesOS.MP;
        MaxHP = playerStatesOS.MaxHP;
        MaxMP = playerStatesOS.MaxMP;
        Attack = playerStatesOS.Attack + Wepondamage;
        Defance = playerStatesOS.Defence;

        //�I�u�W�F�N�g�ɂ��Ă���R���C�_�[�̎擾
        collider = GetComponent<Collider>();
        HPText.GetComponent<Text>().text = "HP :" + currentHP.ToString();
        MPText.GetComponent<Text>().text = "MP :" + currentMP.ToString();
    }
    void Update()
    {
        //isMagic = part.isMagec;
        //int count = part.count;
        //Debug.Log("IsMAGEC" + isMagic);
        //if (isMagic)
        //{
        //    Debug.Log("�����ɑł��ꂽ");
        //    if(count%3 == 0)
        //    {
        //        Damege();
        //    }

        //}

        PlayerStates();


        //���񂾂Ƃ��̏���
        if (currentHP <= 0)
        {
            currentHP = 0;
            var effect = Instantiate(Effct);
            effect.transform.position = transform.position;
            Destroy(effect, 5);
            Destroy(Main);
        }

        float perent_MP = (float)currentMP / MaxMP;
        MPGage.fillAmount = perent_MP;

        float percent_HP =  (float)currentHP / MaxHP;
        HPGage.fillAmount = percent_HP;

    }
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


    //HP�̐��l�\�L
    void PlayerStates()
    {
        HPText.GetComponent<Text>().text = "HP :" + currentHP.ToString();
        MPText.GetComponent<Text>().text = "MP :" + currentMP.ToString();
    }
    void Damege()
    {
        AudioSource.PlayOneShot(HitSE);

        //�G�ɍU����H��������̏���
        damage = (int)(enemyStatesOS.EnemyStatusList[enemyNumber].Attack + Wepondamage) / 2 - playerStatesOS.Defence / 4;

        if (damage > 0)
        {
            currentHP -= damage;
        }
    }



    void ColliderReset()
    {
        collider.enabled = true;    
    }
}
