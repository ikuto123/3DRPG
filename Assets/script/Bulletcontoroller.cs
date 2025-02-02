using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletcontoroller : MonoBehaviour
{

    GameObject Target;
    public string TargetName;
    public Vector3 Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Speed);
    }
}
