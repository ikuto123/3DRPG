using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStatesOS : ScriptableObject
{

    [SerializeField] int hP;
    [SerializeField] int Maxhp;
    [SerializeField] int mP;
    [SerializeField] int Maxmp;
    [SerializeField] int attack;
    [SerializeField] int defence;

    public int HP { get => hP; }
    public int MP { get => mP; }
    public int MaxHP { get => Maxhp; }
    public int MaxMP { get => Maxmp; }
    public int Attack { get => attack; }
    public int Defence { get => defence; }

}
