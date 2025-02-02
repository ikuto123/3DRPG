using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateEnemy : MonoBehaviour
{

    public GameObject Enemy1;
    public GameObject Enemy2;

    public Transform EnemyPlace1;
    public Transform EnemyPlace2;

    public int Count;
    public int MaxCount;

    float TimerCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MaxCount <= Count)
        {
            return;
        }


        TimerCount += Time.deltaTime;
        if (TimerCount > 5)
        {
            Instantiate(Enemy1, EnemyPlace1.position, Quaternion.identity);
            Count++;

            Instantiate(Enemy2, EnemyPlace2.position, Quaternion.identity);
            Count++;


            TimerCount = 0;
           
        }
    }
}
