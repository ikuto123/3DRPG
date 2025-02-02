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
            //Debug.Log("��b�̔���");
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
            //�{�^���ƃv���C���[�̕\���̃��Z�b�g
            Player.SetActive(true);
            YesButton.gameObject.SetActive(true);
            NoButton.gameObject.SetActive(true);
            //��b��0�Ԃɖ߂��A�e�L�X�g�E�B���h�E������
            progressFlag�@= 0;
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
                //Yes�Ԗڂ̉�b�Ɉړ�
                progressFlag = eventSO.eventList[progressFlag].Yes;
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //�J�����̎��_�ړ��@�\��ON
                VartualCamera.SetActive(true);
                //�v���C���[���\��
                Player.SetActive(false);
                //�{�^�����Ȃ���
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                //�ޏo�{�^���̕\��
                ExitButtan.gameObject.SetActive(true);
                //shop�t���O��ON
                ShopFlag = true;
                break;
            case 1:
                //NO���I�����ꂽ�Ƃ��A�X�N���v�^�u���I�u�W�F�N�g��no�Ԃ̉�b�ɔ��
                progressFlag = eventSO.eventList[progressFlag].No;
                //no�Ԗڂ̉�b���擾�A�\��
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //�{�^���e�L�X�g���Ȃ���
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                break;
            case 2:
                //NO���I�����ꂽ�Ƃ��A�X�N���v�^�u���I�u�W�F�N�g��no�Ԃ̉�b�ɔ��
                progressFlag = eventSO.eventList[progressFlag].No;
                //no�Ԗڂ̉�b���擾�A�\��
                currentText = eventSO.eventList[progressFlag].Word;
                talkTaxt.GetComponent<Text>().text = currentText;
                //�v���C���[��\��
                Player.SetActive(true);
                ExitButtan.gameObject.SetActive(false);
                //shop�t���O��OFF
                ShopFlag = false;

                break;
             

        }
    }
}

