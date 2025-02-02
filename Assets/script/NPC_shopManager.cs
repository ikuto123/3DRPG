using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_shopManager : MonoBehaviour
{
    [SerializeField] GameObject Shop_Menu;
    bool shopFlag;
    [SerializeField] ItemSO itemSO;
    //ショップアイテムの取得
    [SerializeField] GameObject ShopItemName_prefab;
    [SerializeField] Transform ShopItemText;
    public int shopLevel;
    float displayTime;

    public RectTransform myGridLayoutGroup;

    private int[] itemQtyAry;
    // Start is called before the first frame update
    void Start()
    {
        itemQtyAry = new int[itemSO.ItemList.Count];
    }

    // Update is called once per frame
    void Update()
    {

        ShopWindow();
    }

    void ShopWindow()
    {
        NPCManager npcManager = GetComponent<NPCManager>();
        shopFlag = npcManager.ShopFlag;
        switch (shopFlag){
            case true:
                displayTime += Time.deltaTime;
                if (displayTime >= 3 && !Shop_Menu.activeSelf)
                {
                    Shop_Menu.SetActive(true);
                    shopItem();
                }
                break;
            case false:
                Shop_Menu.SetActive(false);
                displayTime = 0;

                break;

        }
    }



    void shopItem()
    {

        foreach (Transform n in ShopItemText)//アイテム個数の削除
        {
            GameObject.Destroy(n.gameObject);

        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myGridLayoutGroup);
        for (int i = 0; i < itemQtyAry.Length; i++)
        {

            if (itemSO.ItemList[i].ItemLevel > shopLevel)
            {
                return;
            }
            var ItemNumber = i;

            var obj = Instantiate(ShopItemName_prefab);
            obj.transform.SetParent(ShopItemText);
            obj.GetComponent<Text>().text = itemSO.ItemList[i].ItemName; 
            obj.GetComponent<Button>().onClick.AddListener(() => ItemBuy());

        }
    }

    void ItemBuy()
    {
        Debug.Log("Itemが押されました");
    }

}
