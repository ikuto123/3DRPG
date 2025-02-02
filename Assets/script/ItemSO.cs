using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public List<Item> ItemList = new List<Item>();

    [System.Serializable]
    public class Item
    {
        [SerializeField] int itemNumber;
        [SerializeField] itemType type;
        [SerializeField] string itemName;
        [TextArea]
        [SerializeField] string description;
        [SerializeField] int effect;
        [SerializeField] Sprite itemimage;
        [SerializeField] int Itemlevel;

        public enum itemType
        {
            coin,
            weapon,
            armour,
            tool,
            recovery,
            other,
        }

        public string ItemName { get => itemName; }
        public itemType Itemtype{ get => type; }
        public int ItemEffect {  get => effect;}
        public Sprite ItemImage { get => itemimage; }
        public int ItemLevel { get => Itemlevel; }

    }
}
