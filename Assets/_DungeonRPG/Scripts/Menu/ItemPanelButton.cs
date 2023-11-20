using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelButton : MonoBehaviour,ISelectHandler
{
    [SerializeField]
    private Text equipText;
    public Text EquipText=> equipText;
    [SerializeField]
    private Text nameText;
    public Text NameText=> nameText;
    [SerializeField]
    private Text amountText;
    public Text AmountText=> amountText;
    [SerializeField]
    private Button itemButton;
    public Button ItemButton => itemButton;


    private Item item;
    //�A�C�e�����p�l���̃A�C�e�����\���e�L�X�g
    [SerializeField]
    private Text itemTitleText;
    //�A�C�e�����p�l���̃A�C�e�����\���e�L�X�g
    [SerializeField]
    private Text itemInformationText;

    //�{�^�����I�����ꂽ�Ƃ��Ɏ��s
    public void OnSelect(BaseEventData eventData)
    {
        ShowItemInformation();
    }
    public void ShowItemInformation()
    {
        itemTitleText.text = item.KanjiName;
        itemInformationText.text = item.Information;
    }
    //�f�[�^�̃Z�b�g
    public void SetParam(Item item)
    {
        this.item = item;
    }
}
