using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Playerが攻撃した敵の管理
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
        //ウェポンNumberの取得
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();

        enemyHP = enemyStatesOS.EnemyStatusList[enemyNumber].HP;
        Wepondamage = weponOS.wepondamage[wepon.WeponNumber].Attack;
        //オブジェクトについているコライダーの取得
        collider = GetComponent<Collider>();
    }
    void Update()
    {
        WeponNumber();

        //死んだとき
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
    //攻撃があったった時の処理
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

    void Damege()
    {
        AudioSource.PlayOneShot(HitSE);
        
        //武器ダメージとプレーヤーダメージの加算,防御力を引く
        damage = (int)(playerStatesOS.Attack + Wepondamage)/2 - enemyStatesOS.EnemyStatusList[enemyNumber].Defence / 4;

        //防御力が高かった際に敵が回復しないように
        if(damage > 0)
        {
            enemyHP -= damage;
        }
    }

    void ColliderReset()
    {
        collider.enabled = true;    
    }

    //武器のナンバー参照
    void WeponNumber()
    {
        Wepon wepon;
        GameObject obj = GameObject.Find("wepon");
        wepon = obj.GetComponent<Wepon>();
        Debug.Log(wepon.WeponNumber);
    }
}
