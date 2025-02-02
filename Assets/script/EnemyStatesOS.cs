using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemyStatesOS : ScriptableObject
{
    public List<EnemyStatus> EnemyStatusList = new List<EnemyStatus>();

    [System.Serializable]
    public class EnemyStatus
    {
        [SerializeField] int enemyNumber;
        [SerializeField] string enemyName;
        //[TextArea]
        //[SerializeField] string discription;
        [SerializeField] int hP;
        [SerializeField] int Maxhp;
        [SerializeField] int mP;
        [SerializeField] int attack;
        [SerializeField] int defence;
        //[SerializeField] Type type;

        //public enum Type
        //{
        //    nomal,
        //    fire,
        //    water,
        //}
        public int HP { get => hP; }
        public int MP { get => mP; }
        public int MaxHP { get => Maxhp; }
        public int Attack { get => attack; }
        public int Defence { get => defence; }
    }



}
