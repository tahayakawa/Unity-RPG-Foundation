using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuCommand : MonoBehaviour
{
    public enum MenuMode
    {
        Menu,//メニュー初期状態
        StatusCharacterSelect,//ステータスを表示するキャラクター選択画面
        Status,//ステータス画面
        Item,//アイテム一覧
        UseItemSelectCharacter,//使用するアイテムを使うキャラクター選択画面
    }
    private MenuMode menuMode;

    [SerializeField]
    private PlayerMenuCommand playerMenuCommand;


    //メニューを開いたときに最初に選択状態になるボタン
    [SerializeField]
    private GameObject firstSelectButton;

    //メニューパネル
    [SerializeField]
    private GameObject menuPanel;
    //ステータスパネル
    [SerializeField]
    private GameObject statusPanel;
    //キャラクター選択パネル
    [SerializeField]
    private GameObject selectCharacterPanel;
    //簡易ステータスパネル配列
    [SerializeField]
    private SimpleCharacterStatusPanel[] simpleCharacterStatusPanels;
    //お知らせパネル
    [SerializeField]
    private ItemNotificationPanel notificationPanel;

    //お知らせ確認ボタン
    [SerializeField]
    private GameObject notificationButton;

    //アイテム一覧パネル
    [SerializeField]
    private GameObject itemPanel;
    //アイテムパネルボタンを表示する場所
    [SerializeField]
    private Transform content;
    //アイテム情報パネル
    [SerializeField]
    private GameObject itemInformationPanel;
    //アイテム一覧のパネルのスクロール管理スクリプト
    [SerializeField]
    private ScrollManager scrollManager;

    [SerializeField]
    private CanvasGroup menuPanelCanvasGroup;
    [SerializeField]
    private CanvasGroup selectCharacterPanelCanvasGroup;
    //簡易ステータスパネルCanvas Group
    [SerializeField]
    private CanvasGroup simpleStatusPanelCanvasGroup;
    [SerializeField]
    private CanvasGroup itemPanelCanvasGroup;

    //ステータス画面テキスト
    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Text statusTitleText;
    [SerializeField]
    private Text statusParam1Text;
    [SerializeField]
    private Text statusParam2Text;

    //死亡状態のテキストカラー
    [SerializeField]
    private Color32 deadColor;
    //死にかけのHPテキストカラー
    [SerializeField]
    private Color32 nearDeadColor;
    //半分未満のHPテキストカラー
    [SerializeField]
    private Color32 halfDeadColor;
    //通常状態のテキストカラー
    [SerializeField]
    private Color32 normalColor;

    //アイテム情報パネルのアイテム名
    [SerializeField]
    private Text itemInoformationTitleText;
    //アイテム情報パネルのアイテム情報
    [SerializeField]
    private Text itemInformationText;

    //パーティステータス
    [SerializeField]
    private PartyStatus partyStatus;

    [SerializeField]
    private GameObject characterButtonPrefab;
    [SerializeField]
    private GameObject useItemPanelButtonPrefab;

    //アイテムボタン一覧
    private List<GameObject> itemPanelButtonList= new List<GameObject>();

    //最後に選択していたゲームオブジェクトをスタック
    private Stack<GameObject> selectedGameObjectStack = new Stack<GameObject>();


    private void OnEnable()
    {
        menuMode = MenuMode.Menu;
        statusPanel.SetActive(false);
        selectCharacterPanel.SetActive(false);
        itemPanel.SetActive(false);
        itemInformationPanel.SetActive(false);

        //キャラクター選択ボタンがあればすべて削除
        for(int i=selectCharacterPanel.transform.childCount-1; i>=0; i--)
        {
            Destroy(selectCharacterPanel.transform.GetChild(i).gameObject);
        }

        selectedGameObjectStack.Clear();
        itemPanelButtonList.Clear();

        menuPanelCanvasGroup.interactable = true;
        selectCharacterPanelCanvasGroup.interactable = false;
        itemPanelCanvasGroup.interactable = false;
        simpleStatusPanelCanvasGroup.interactable = false;

        EventSystem.current.SetSelectedGameObject(firstSelectButton);

        //簡易ステータス画面を更新
        UpdateSimpleStatusPanel();
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            //メニュー画面時
            if (menuMode == MenuMode.Menu)
            {
                playerMenuCommand.ExitMenu();
                gameObject.SetActive(false);
            }
            //ステータスキャラクター選択またはステータス表示時
            else if(menuMode == MenuMode.StatusCharacterSelect || menuMode == MenuMode.Status)
            {
                selectCharacterPanelCanvasGroup.interactable= false;
                selectCharacterPanel.SetActive(false);
                statusPanel.SetActive(false);
                //キャラクター選択ボタンがあればすべて削除
                for (int i = selectCharacterPanel.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(selectCharacterPanel.transform.GetChild(i).gameObject);
                }
                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                menuPanelCanvasGroup.interactable = true;
                menuMode = MenuMode.Menu;

            }
            //アイテム一覧表示時
            else if (menuMode == MenuMode.Item)
            {
                itemPanelCanvasGroup.interactable= false;
                itemPanel.SetActive(false);
                itemInformationPanel.SetActive(false);
                itemPanelButtonList.Clear();

                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                menuPanelCanvasGroup.interactable = true;
                menuMode=MenuMode.Menu;
            }
            //アイテムを使う相手を選択時
            else if (menuMode == MenuMode.UseItemSelectCharacter)
            {
                simpleStatusPanelCanvasGroup.interactable = false;

                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                itemPanelCanvasGroup.interactable |= true;
                menuMode = MenuMode.Item;
            }
        }

        //選択解除されたとき（マウスでUI外をクリックしたとき）は現在のモードによって無理やり選択させる
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            switch (menuMode)
            {
                case MenuMode.Menu:
                    EventSystem.current.SetSelectedGameObject(firstSelectButton);
                    break;
                case MenuMode.StatusCharacterSelect:
                    EventSystem.current.SetSelectedGameObject(selectCharacterPanel.transform.GetChild(0).gameObject);
                    break;
                case MenuMode.Status:
                    EventSystem.current.SetSelectedGameObject(selectCharacterPanel.transform.GetChild(0).gameObject);
                    break;
                case MenuMode.Item:
                    EventSystem.current.SetSelectedGameObject(content.GetChild(0).gameObject);
                    //スクロールを一番先頭に
                    scrollManager.Reset();
                    break;
                case MenuMode.UseItemSelectCharacter:
                    EventSystem.current.SetSelectedGameObject(simpleCharacterStatusPanels[0].gameObject);
                    break;
            }
        }
    }
    public void SelectCommand(string command)
    {
        if(command == "Status")
        {
            menuMode = MenuMode.StatusCharacterSelect;
            menuPanelCanvasGroup.interactable = false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);


            //パーティメンバー分のボタンを作成
            foreach(var member in partyStatus.GetAllyStatus())
            {
                GameObject buttonObj = Instantiate(characterButtonPrefab, selectCharacterPanel.transform);
                buttonObj.GetComponentInChildren<Text>().text = member.CharacterName;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => ShowStatus(member));
            }
            //階層を一番最後に並べ替え
            selectCharacterPanel.SetActive(true);
            selectCharacterPanelCanvasGroup.interactable = true;
            EventSystem.current.SetSelectedGameObject(selectCharacterPanel.transform.GetChild(0).gameObject);
        }
        else if (command == "Item")
        {
            menuMode= MenuMode.Item;
            menuPanelCanvasGroup.interactable= false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);

            CreateItemPanelButton();
        }
    }
    
    public void ShowStatus(AllyStatus allyStatus)
    {
        menuMode = MenuMode.Status;
        statusPanel.SetActive(true);

        //キャラクター名
        characterNameText.text = allyStatus.CharacterName;

        //タイトルの表示
        string text = "レベル\n";
        text += "HP\n";
        text += "MP\n";
        text += "経験値\n";
        text += "状態異常\n";
        text += "STR\n";
        text += "VIT\n";
        text += "INT\n";
        text += "RES\n";
        text += "AGI\n";
        text += "装備武器\n";
        text += "装備防具\n";
        text += "攻撃力\n";
        text += "防御力\n";
        text += "魔法防御力\n";
        statusTitleText.text = text;

        //現在のHPとMPの表示
        text = "\n";
        text += allyStatus.Hp + "\n";
        text += allyStatus.Mp + "\n";
        statusParam1Text.text = text;


        //ステータスパラメータの表示
        text = allyStatus.Level + "\n";
        text += allyStatus.MaxHp + "\n";
        text += allyStatus.MaxMp + "\n";
        text += allyStatus.EarnedExperience + "\n";
        if (allyStatus.IsPoisonState)
        {
            text += "毒 ";
        }
        if (allyStatus.IsNumbnessState)
        {
            text += "痺れ ";
        }
        text += "\n";

        text += allyStatus.Strength + "\n";
        text += allyStatus.Vitality + "\n";
        text += allyStatus.Intelligence + "\n";
        text += allyStatus.Resist + "\n";
        text += allyStatus.Agility + "\n";

        text += allyStatus?.EquipWeapon?.KanjiName ?? "";
        text+= "\n";
        text += allyStatus?.EquipArmor?.KanjiName ?? "";
        text += "\n";

        text += allyStatus.GetAttack() + "\n";
        text += allyStatus.GetDefense() + "\n";
        text += allyStatus.GetMagicDefense() + "\n";
        statusParam2Text.text = text;
    }
    public void CreateItemPanelButton()
    {
        itemPanel.SetActive(true);
        itemPanelCanvasGroup.interactable = true;
        itemInformationPanel.SetActive(true);
        //スクロールを一番先頭に
        scrollManager.Reset();

        int itemPanelButtonNum = 0;
        //アイテムの分だけアイテムパネルボタンを作成
        foreach(var item in partyStatus.GetItemDictionary().Keys)
        {
            ItemPanelButton button = content.GetChild(itemPanelButtonNum).GetComponent<ItemPanelButton>();
            button.gameObject.SetActive(true);
            button.NameText.text = item.KanjiName;
            button.SetParam(item);
            //ボタンに割り当てられている関数を削除
            button.ItemButton.onClick.RemoveAllListeners();
            //消費回復アイテムの場合アイテム使用関数をボタンに割り当て
            if (item.ItemType == Item.Type.HPRecovery ||
                item.ItemType == Item.Type.MPRecovery ||
                item.ItemType == Item.Type.PoisonRecovery ||
                item.ItemType == Item.Type.NumbnessRecovery)
            {
                button.ItemButton.onClick.AddListener(() => UseItem(item));
            }

            //アイテム数を表示
            button.AmountText.text = partyStatus.GetItemNum(item).ToString();

            //パーティメンバーが装備している武器防具があれば名前の横にEを表示する
            foreach(var member in partyStatus.GetAllyStatus())
            {
                if (member.EquipWeapon == item)
                {
                    button.EquipText.text = "E";
                }
                else if (member.EquipArmor == item)
                {
                    button.EquipText.text = "E";
                }
            }

            //アイテムボタンリストに追加
            itemPanelButtonList.Add(button.gameObject);

            itemPanelButtonNum++;
        }

        //アイテム数を超えるアイテムパネルボタンを非表示
        for (int i = itemPanelButtonNum;i<content.transform.childCount;i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }

        //最初のアイテムを選択状態にする
        if(itemPanelButtonList.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(content.GetChild(0).gameObject);
        }
    }
    public void UseItem(Item item)
    {
        itemPanelCanvasGroup.interactable = false;
        simpleStatusPanelCanvasGroup.interactable = true;
        selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);

        int memberNum = 0;
        //簡易ステータスパネルのボタンに関数割り当て
        foreach(var member in partyStatus.GetAllyStatus())
        {
            var button = simpleCharacterStatusPanels[memberNum];
            button.CharacterButton.onClick.RemoveAllListeners();
            button.CharacterButton.onClick.AddListener(() => UseItemToCharacter(member, item));
            memberNum++;
        }
        //アイテム使用先キャラクター選択画面に移行
        menuMode = MenuMode.UseItemSelectCharacter;
        EventSystem.current.SetSelectedGameObject(simpleCharacterStatusPanels[0].gameObject);
        simpleStatusPanelCanvasGroup.interactable = true;
        Input.ResetInputAxes();
    }
    public void UseItemToCharacter(AllyStatus toChara,Item item)
    {
        simpleStatusPanelCanvasGroup.interactable= false;
        selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);

        if (item.ItemType == Item.Type.HPRecovery)
        {
            if (toChara.Hp == toChara.MaxHp)
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "は元気です");
            }
            else
            {
                toChara.Hp = toChara.Hp + item.Amount;
                notificationPanel.PopupNotification(toChara.CharacterName + "はHPを" + item.Amount + "回復しました");
                //アイテム数を減らす
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
        }
        else if(item.ItemType == Item.Type.MPRecovery)
        {
            if (toChara.Mp == toChara.MaxMp)
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "のMPは最大です");
            }
            else
            {
                toChara.Mp = toChara.Mp + item.Amount;
                notificationPanel.PopupNotification(toChara.CharacterName + "はMPを" + item.Amount + "回復しました");
                //アイテム数を減らす
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
        }
        else if (item.ItemType == Item.Type.PoisonRecovery)
        {
            if (toChara.IsPoisonState)
            {
                toChara.IsPoisonState = false;
                notificationPanel.PopupNotification(toChara.CharacterName + "は毒から回復しました");
                //アイテム数を減らす
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
            else
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "は毒状態ではありません");
            }
        }
        else if (item.ItemType == Item.Type.NumbnessRecovery)
        {
            if (toChara.IsNumbnessState)
            {
                toChara.IsNumbnessState = false;
                notificationPanel.PopupNotification(toChara.CharacterName + "は痺れから回復しました");
                //アイテム数を減らす
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
            else
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "は痺れ状態ではありません");
            }
        }
        
        //アイテムボタンリストから該当するアイテムを探して数を更新する
        var itemButton = itemPanelButtonList.Find(obj => obj.transform.Find("NameText").GetComponent<Text>().text == item.KanjiName);
        itemButton.transform.Find("AmountText").GetComponent<Text>().text=partyStatus.GetItemNum(item).ToString();

        //簡易ステータス画面を更新
        UpdateSimpleStatusPanel();

        //アイテム数が0ならアイテムボタンとPartyStatusからアイテムを削除
        if (partyStatus.GetItemNum(item) == 0)
        {
            //アイテムパネルリストから該当のアイテムを削除
            itemPanelButtonList.Remove(itemButton);
            //アイテムパネルボタン自身を非表示
            itemButton.SetActive(false);
            //アクティブな子オブジェクトの数に影響しないように一番下に移動
            itemButton.transform.SetAsLastSibling();
            //ItemDictionaryから削除
            partyStatus.GetItemDictionary().Remove(item);

            menuMode = MenuMode.Item;
            //確認ボタンの表示
            notificationPanel.ShowNotificationButton(item.KanjiName + "はなくなりました");
            EventSystem.current.SetSelectedGameObject(notificationButton);

            Input.ResetInputAxes();
        }
        else
        {
            menuMode = MenuMode.UseItemSelectCharacter;
            simpleStatusPanelCanvasGroup.interactable = true;
            EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
            Input.ResetInputAxes();
        }
    }
    //この確認ボタンが押されたらアイテム一覧に戻る
    public void ConfirmNotification()
    {
        notificationButton.SetActive(false);

        itemPanelCanvasGroup.interactable = true;
        //一気にアイテム一覧まで戻すため、キャラクター選択パネルと無くなったアイテムボタンのオブジェクト登録をスキップ
        selectedGameObjectStack.Pop();
        selectedGameObjectStack.Pop();
        EventSystem.current.SetSelectedGameObject(content.GetChild(0).gameObject);
        scrollManager.Reset();
    }
    public void UpdateSimpleStatusPanel()
    {
        int memberNumber = 0;
        //パーティメンバーステータスを簡易ステータスパネルに反映
        foreach(var member in partyStatus.GetAllyStatus())
        {
            var charaPanel = simpleCharacterStatusPanels[memberNumber];
            charaPanel.gameObject.SetActive(true);
            charaPanel.NameText.text = member.CharacterName;
            charaPanel.LevelText.text=member.Level.ToString();
            charaPanel.HpText.text=member.Hp+"/"+member.MaxHp;
            charaPanel.MpText.text=member.Mp+"/"+member.MaxMp;
            
            //残りHPに応じてテキストの色を変える
            //死亡状態
            if (member.Hp == 0)
            {
                charaPanel.NameText.color = deadColor;
                charaPanel.LevelText.color = deadColor;
                charaPanel.HpText.color = deadColor;
                charaPanel.MpText.color = deadColor;
                charaPanel.LevelTitle.color = deadColor;
                charaPanel.HpTitle.color = deadColor;
                charaPanel.MpTitle.color = deadColor;
            }
            //死にかけ
            else if ((member.Hp / (float)member.MaxHp) < 0.1f)
            {
                charaPanel.NameText.color = normalColor;
                charaPanel.LevelText.color = normalColor;
                charaPanel.HpText.color = nearDeadColor;
                charaPanel.MpText.color = normalColor;
                charaPanel.LevelTitle.color = normalColor;
                charaPanel.HpTitle.color = normalColor;
                charaPanel.MpTitle.color = normalColor;
            }
            //半分未満
            else if ((member.Hp / (float)member.MaxHp) < 0.5f)
            {
                charaPanel.NameText.color = normalColor;
                charaPanel.LevelText.color = normalColor;
                charaPanel.HpText.color = halfDeadColor;
                charaPanel.MpText.color = normalColor;
                charaPanel.LevelTitle.color = normalColor;
                charaPanel.HpTitle.color = normalColor;
                charaPanel.MpTitle.color = normalColor;
            }
            //半分以上
            else
            {
                charaPanel.NameText.color = normalColor;
                charaPanel.LevelText.color = normalColor;
                charaPanel.HpText.color = normalColor;
                charaPanel.MpText.color = normalColor;
                charaPanel.LevelTitle.color = normalColor;
                charaPanel.HpTitle.color = normalColor;
                charaPanel.MpTitle.color = normalColor;
            }

            memberNumber++;
        }
        //パーティメンバー数が少なければ非表示
        for (int i = memberNumber;i<simpleCharacterStatusPanels.Length;i++)
        {
            simpleCharacterStatusPanels[i].gameObject.SetActive(false);
        }
    }
}
