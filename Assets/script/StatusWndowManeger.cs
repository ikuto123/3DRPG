using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusWndowManeger : MonoBehaviour
{
    [SerializeField] PlayerStatesOS playerStatesOS;

    //プレイヤーステータスのテキスト
    [SerializeField] Text hpValue;
    [SerializeField] Text MaxHP_value;
    [SerializeField] Text mpValue;
    [SerializeField] Text MaxMP_value;
    [SerializeField] Text Attak_value;
    [SerializeField] Text Defance_value;

    //アイテムのテキスト
    [SerializeField] Text CoinValue;
    [SerializeField] Text PotionValue;

    GameObject playerStatusManager;
    GameObject player;

    

    // Start is called before the first frame update
    void Start()
    {
        //hpValue.GetComponent<Text>().text = playerStatesOS.HP.ToString();
        playerStatusManager = GameObject.Find("PlayerStatusManeger");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatusManager == null || player == null)
        {
            return;
        }
        //hpValue.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().currentHP.ToString();

    }
    public void GetStatus()
    {
        hpValue.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().currentHP.ToString();
        MaxHP_value.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().MaxHP.ToString();
        mpValue.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().currentMP.ToString();
        MaxMP_value.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().MaxMP.ToString();
        Attak_value.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().Attack.ToString();
        Defance_value.GetComponent<Text>().text = GameObject.Find("PlayerStatusManeger").GetComponent<PlayerStatesManeger>().Defance.ToString();
        //CoinValue.GetComponent<Text>().text = GameObject.Find("Player").GetComponent<PlayerContoroller>().coin.ToString();
        //PotionValue.GetComponent<Text>().text = GameObject.Find("Player").GetComponent<PlayerContoroller>().potion.ToString();
    }
}
