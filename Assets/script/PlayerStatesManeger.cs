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

    //HP表記
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

        //武器情報の取得
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();
        Wepondamage = weponOS.wepondamage[wepon.WeponNumber].Attack;

        //enemyNumberの取得
        GameObject enemy = GameObject.Find("StatusManeger");
        statusManeger = enemy.GetComponent<StatusManeger>();
        enemyNumber = statusManeger.enemyNumber;

        //OSから値の初期化
        currentHP = playerStatesOS.HP;
        currentMP = playerStatesOS.MP;
        MaxHP = playerStatesOS.MaxHP;
        MaxMP = playerStatesOS.MaxMP;
        Attack = playerStatesOS.Attack + Wepondamage;
        Defance = playerStatesOS.Defence;

        //オブジェクトについているコライダーの取得
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
        //    Debug.Log("落雷に打たれた");
        //    if(count%3 == 0)
        //    {
        //        Damege();
        //    }

        //}

        PlayerStates();


        //死んだときの処理
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
            //ボックスコライダーをoff
            collider.enabled = false;
            //2秒後に当たり判定復活
            Invoke("ColliderReset", ResetTime);
            Debug.Log("攻撃が当たりました");
        }
    }


    //HPの数値表記
    void PlayerStates()
    {
        HPText.GetComponent<Text>().text = "HP :" + currentHP.ToString();
        MPText.GetComponent<Text>().text = "MP :" + currentMP.ToString();
    }
    void Damege()
    {
        AudioSource.PlayOneShot(HitSE);

        //敵に攻撃を食らった時の処理
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
