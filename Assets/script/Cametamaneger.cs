using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cametamaneger : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 PlayerPos;
    private float speed = 500f;
    private float MouseInputX;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(Player == null)
        {
            return;
        }
        PlayerPos = Player.transform.position;
        MouseInputX = Input.GetAxis("Mouse X");
        transform.RotateAround(PlayerPos, Vector3.up, MouseInputX * Time.deltaTime * speed);
    }
}
