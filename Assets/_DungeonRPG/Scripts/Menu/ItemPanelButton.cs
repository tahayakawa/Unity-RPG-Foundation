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
    //アイテム情報パネルのアイテム名表示テキスト
    [SerializeField]
    private Text itemTitleText;
    //アイテム情報パネルのアイテム情報表示テキスト
    [SerializeField]
    private Text itemInformationText;

    //ボタンが選択されたときに実行
    public void OnSelect(BaseEventData eventData)
    {
        ShowItemInformation();
    }
    public void ShowItemInformation()
    {
        itemTitleText.text = item.KanjiName;
        itemInformationText.text = item.Information;
    }
    //データのセット
    public void SetParam(Item item)
    {
        this.item = item;
    }
}
