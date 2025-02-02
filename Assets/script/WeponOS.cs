using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeponOS : ScriptableObject
{
    public List<WeponDamage> wepondamage = new List<WeponDamage>();

    [System.Serializable]
    public class WeponDamage
    {
        [SerializeField] string weponName;

        [SerializeField] int attack;

        public int Attack { get => attack; }
        
    }



}