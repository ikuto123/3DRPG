using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    [SerializeField] Text talkTaxt;
    [SerializeField] GameObject TalkWindow;
    [SerializeField] EventSO eventSO;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    [SerializeField] Button ExitButtan;
    [SerializeField] GameObject VartualCamera;
    [SerializeField] GameObject Player;

    public int NPCNumber;
    private bool talkFlag = false;
    public bool NPCFlag = false;
    private int progressFlag = 0;
    private string currentText;

    public bool ShopFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ExitButtan.gameObject.SetActive(false);
            NPCFlag = true;
            //Debug.Log("会話の発生");
            switch (talkFlag)
            {
                case false:
                    TalkWindow.SetActive(true);
                    EventProgress();
                    talkTaxt.GetComponent<Text>().text = currentText;
                    talkFlag = true;   
                    break;
                case true:
                    //TalkWindow.SetActive(false);
                    //talkTaxt.GetComponent<Text>().text = "";
                    talkFlag = false;
                    break;
            }
        
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //ボタンとプレイヤーの表示のリセット
            Player.SetActive(true);
            YesButton.gameObject.SetActive(true);
            NoButton.gameObject.SetActive(true);
            //会話を0番に戻し、テキストウィンドウを消す
            progressFlag　= 0;
            NPCFlag = false;
            TalkWindow.SetActive(false);
            VartualCamera.SetActive(false);
        }
    }

    void EventProgress()
    {
        currentText = eventSO.eventList[progressFlag].Word;
        //progressFlag++;
    }

    public void ClickEventButton(int Button)
    {
        switch (Button)
        {
            case 0:
                //Yes番目の会話に移動
                progressFlag = eventSO.eventList[progressFlag].Yes;
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //カメラの視点移動機能をON
                VartualCamera.SetActive(true);
                //プレイヤーを非表示
                Player.SetActive(false);
                //ボタンをなくす
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                //退出ボタンの表示
                ExitButtan.gameObject.SetActive(true);
                //shopフラグのON
                ShopFlag = true;
                break;
            case 1:
                //NOが選択されたとき、スクリプタブルオブジェクトのno番の会話に飛ぶ
                progressFlag = eventSO.eventList[progressFlag].No;
                //no番目の会話を取得、表示
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //ボタンテキストをなくす
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                break;
            case 2:
                //NOが選択されたとき、スクリプタブルオブジェクトのno番の会話に飛ぶ
                progressFlag = eventSO.eventList[progressFlag].No;
                //no番目の会話を取得、表示
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //プレイヤーを表示
                Player.SetActive(true);
                ExitButtan.gameObject.SetActive(false);
                //shopフラグのOFF
                ShopFlag = false;

                break;
             

        }
    }
}

