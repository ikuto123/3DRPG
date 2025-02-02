using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ParticlHit : MonoBehaviour
{
    public bool isMagec = false;
    public int count;
    private void Start()
    {
        count = 0;
    }
    void OnParticleCollision(GameObject other)
    {
        // 衝突相手がプレイヤーか確認
        if (other.CompareTag("Player"))
        {
            isMagec = true;
            Invoke("MagicReSet", (float)0.5);
            count++;
            Debug.Log("count = "+ count);
        }
    }

    void MagicReSet()
    {
        isMagec = false;
    }


}


