using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManeger : MonoBehaviour
{
    public int ItemNumber;

    //コイン関係
    float CoinSpeed = 100f;
    float CoinLifeTime = 0.5f;

    bool isGet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGet)
        {
            transform.Rotate(Vector3.up * CoinSpeed * 10f * Time.deltaTime, Space.World);
            // 生存時間を減らす
            CoinLifeTime -= Time.deltaTime;

            // 生存時間が0以下になったら消滅
            if (CoinLifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isGet = true;

            GetComponent<AudioSource>().Play();
            //アイテムを上にポップ
            transform.position += Vector3.up * 1.5f;
        }
    }
}
