using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    public int WeponNumber;
    [SerializeField] WeponOS weponOS;

    int Wepondamage;
    void Start()
    {
        Wepondamage = weponOS.wepondamage[WeponNumber].Attack; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {

            Debug.Log("攻撃が当たりました(Wepon)");
        }
    }
}
