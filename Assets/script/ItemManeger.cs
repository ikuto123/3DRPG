using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManeger : MonoBehaviour
{
    public int ItemNumber;

    //�R�C���֌W
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
            // �������Ԃ����炷
            CoinLifeTime -= Time.deltaTime;

            // �������Ԃ�0�ȉ��ɂȂ��������
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
            //�A�C�e������Ƀ|�b�v
            transform.position += Vector3.up * 1.5f;
        }
    }
}
