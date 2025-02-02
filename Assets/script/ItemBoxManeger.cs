using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxManeger : MonoBehaviour
{
    [SerializeField] ItemSO itemSO;
    public int GetItem;
    private int[] itemQtyAry; //アイテムの配列の宣言
    private string itemOpenText; 
    [SerializeField] Text ItemText;
    [SerializeField] GameObject PlayerStatusManager;

    [SerializeField] Text Cointext;
    [SerializeField] Text Potiontext;
    [SerializeField] GameObject ItemImage_prefub;
    [SerializeField] GameObject ItemQty_prefab;
    [SerializeField] Transform ItenboxImage;
    [SerializeField] Transform ItemBoxText;
    private string itemType;

    private int MaxHP;
    private int currentHP;
    

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = PlayerStatusManager.GetComponent<PlayerStatesManeger>().MaxHP;;
        currentHP = PlayerStatusManager.GetComponent<PlayerStatesManeger>().currentHP;
        //アイテムの種類の数ItemSOから取得
        itemQtyAry = new int[itemSO.ItemList.Count];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(itemSO.ItemList[0].Itemtype);
    }

    public void ItemGet()
    {
        itemQtyAry[GetItem] = itemQtyAry[GetItem] + 1; //獲得アイテムのプラス
    }

    public void ItemUse(int ItemNuber)
    {
        MaxHP = PlayerStatusManager.GetComponent<PlayerStatesManeger>().MaxHP; ;
        currentHP = PlayerStatusManager.GetComponent<PlayerStatesManeger>().currentHP;
        itemType = itemSO.ItemList[ItemNuber].Itemtype.ToString();
        if (itemQtyAry[ItemNuber] > 0)
        {
            switch (itemType)
            {
                case "coin":
                    Debug.Log(itemType);
                    break;
                case "recovery":
                    if(MaxHP > currentHP)
                    {
                        PlayerStatusManager.GetComponent<PlayerStatesManeger>().currentHP += itemSO.ItemList[ItemNuber].ItemEffect;
                        itemQtyAry[ItemNuber]--;
                        ItemOpen();
                    }
                    else
                    {
                        Debug.Log("アイテムが使用できません");
                        return;
                    }

                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("NoItem");
        }
        
    }

    public void ItemOpen()
    {
        //Cointext.GetComponent<Text>().text = itemQtyAry[0].ToString();
        //Potiontext.GetComponent<Text>().text = itemQtyAry[1].ToString();

        //for (int i = 0; i < itemQtyAry.Length; i++)
        //{

        //    itemOpenText = itemOpenText + itemSO.ItemList[i].ItemName + " " + itemQtyAry[i].ToString() + "\n";
        //    ItemText.GetComponent<Text>().text = itemOpenText;

        //}

        foreach(Transform n in ItenboxImage)//アイテム画像載せ駆除
        {
            GameObject.Destroy(n.gameObject);
        }
        foreach (Transform n in ItemBoxText)//アイテム工数の削除
        {
            GameObject.Destroy (n.gameObject);
        }

        for (int i = 0; i < itemQtyAry.Length; i++)
        {
            if(itemQtyAry[i] > 0)//アイテム所持が0以上
            {
                var ItemNumber = i;
                var obj = Instantiate(ItemImage_prefub);  //アイテム画像の生成する
                obj.transform.SetParent(ItenboxImage); //アイテム画像をフォルダに移行
                obj.GetComponent<Image>().sprite = itemSO.ItemList[i].ItemImage;
                obj.GetComponent<Button>().onClick.AddListener(() => ItemUse(ItemNumber));

                var obj2 = Instantiate(ItemQty_prefab);
                obj2.transform.SetParent(ItemBoxText);
                obj2.GetComponent<Text>().text = itemQtyAry[i].ToString();
                obj2.GetComponent<Button>().onClick.AddListener(() => ItemUse(ItemNumber));


            }
        }

    }

    //public void ItemClose()
    //{

    //    itemOpenText = ""; // 取得した文字を削除
        
    //}
}
