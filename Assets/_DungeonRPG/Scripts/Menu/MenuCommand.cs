using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuCommand : MonoBehaviour
{
    public enum MenuMode
    {
        Menu,//���j���[�������
        StatusCharacterSelect,//�X�e�[�^�X��\������L�����N�^�[�I�����
        Status,//�X�e�[�^�X���
        Item,//�A�C�e���ꗗ
        UseItemSelectCharacter,//�g�p����A�C�e�����g���L�����N�^�[�I�����
    }
    private MenuMode menuMode;

    [SerializeField]
    private PlayerMenuCommand playerMenuCommand;


    //���j���[���J�����Ƃ��ɍŏ��ɑI����ԂɂȂ�{�^��
    [SerializeField]
    private GameObject firstSelectButton;

    //���j���[�p�l��
    [SerializeField]
    private GameObject menuPanel;
    //�X�e�[�^�X�p�l��
    [SerializeField]
    private GameObject statusPanel;
    //�L�����N�^�[�I���p�l��
    [SerializeField]
    private GameObject selectCharacterPanel;
    //�ȈՃX�e�[�^�X�p�l���z��
    [SerializeField]
    private SimpleCharacterStatusPanel[] simpleCharacterStatusPanels;
    //���m�点�p�l��
    [SerializeField]
    private ItemNotificationPanel notificationPanel;

    //���m�点�m�F�{�^��
    [SerializeField]
    private GameObject notificationButton;

    //�A�C�e���ꗗ�p�l��
    [SerializeField]
    private GameObject itemPanel;
    //�A�C�e���p�l���{�^����\������ꏊ
    [SerializeField]
    private Transform content;
    //�A�C�e�����p�l��
    [SerializeField]
    private GameObject itemInformationPanel;
    //�A�C�e���ꗗ�̃p�l���̃X�N���[���Ǘ��X�N���v�g
    [SerializeField]
    private ScrollManager scrollManager;

    [SerializeField]
    private CanvasGroup menuPanelCanvasGroup;
    [SerializeField]
    private CanvasGroup selectCharacterPanelCanvasGroup;
    //�ȈՃX�e�[�^�X�p�l��Canvas Group
    [SerializeField]
    private CanvasGroup simpleStatusPanelCanvasGroup;
    [SerializeField]
    private CanvasGroup itemPanelCanvasGroup;

    //�X�e�[�^�X��ʃe�L�X�g
    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Text statusTitleText;
    [SerializeField]
    private Text statusParam1Text;
    [SerializeField]
    private Text statusParam2Text;

    //���S��Ԃ̃e�L�X�g�J���[
    [SerializeField]
    private Color32 deadColor;
    //���ɂ�����HP�e�L�X�g�J���[
    [SerializeField]
    private Color32 nearDeadColor;
    //����������HP�e�L�X�g�J���[
    [SerializeField]
    private Color32 halfDeadColor;
    //�ʏ��Ԃ̃e�L�X�g�J���[
    [SerializeField]
    private Color32 normalColor;

    //�A�C�e�����p�l���̃A�C�e����
    [SerializeField]
    private Text itemInoformationTitleText;
    //�A�C�e�����p�l���̃A�C�e�����
    [SerializeField]
    private Text itemInformationText;

    //�p�[�e�B�X�e�[�^�X
    [SerializeField]
    private PartyStatus partyStatus;

    [SerializeField]
    private GameObject characterButtonPrefab;
    [SerializeField]
    private GameObject useItemPanelButtonPrefab;

    //�A�C�e���{�^���ꗗ
    private List<GameObject> itemPanelButtonList= new List<GameObject>();

    //�Ō�ɑI�����Ă����Q�[���I�u�W�F�N�g���X�^�b�N
    private Stack<GameObject> selectedGameObjectStack = new Stack<GameObject>();


    private void OnEnable()
    {
        menuMode = MenuMode.Menu;
        statusPanel.SetActive(false);
        selectCharacterPanel.SetActive(false);
        itemPanel.SetActive(false);
        itemInformationPanel.SetActive(false);

        //�L�����N�^�[�I���{�^��������΂��ׂč폜
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

        //�ȈՃX�e�[�^�X��ʂ��X�V
        UpdateSimpleStatusPanel();
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Cancel"))
        {
            //���j���[��ʎ�
            if (menuMode == MenuMode.Menu)
            {
                playerMenuCommand.ExitMenu();
                gameObject.SetActive(false);
            }
            //�X�e�[�^�X�L�����N�^�[�I���܂��̓X�e�[�^�X�\����
            else if(menuMode == MenuMode.StatusCharacterSelect || menuMode == MenuMode.Status)
            {
                selectCharacterPanelCanvasGroup.interactable= false;
                selectCharacterPanel.SetActive(false);
                statusPanel.SetActive(false);
                //�L�����N�^�[�I���{�^��������΂��ׂč폜
                for (int i = selectCharacterPanel.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(selectCharacterPanel.transform.GetChild(i).gameObject);
                }
                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                menuPanelCanvasGroup.interactable = true;
                menuMode = MenuMode.Menu;

            }
            //�A�C�e���ꗗ�\����
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
            //�A�C�e�����g�������I����
            else if (menuMode == MenuMode.UseItemSelectCharacter)
            {
                simpleStatusPanelCanvasGroup.interactable = false;

                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                itemPanelCanvasGroup.interactable |= true;
                menuMode = MenuMode.Item;
            }
        }

        //�I���������ꂽ�Ƃ��i�}�E�X��UI�O���N���b�N�����Ƃ��j�͌��݂̃��[�h�ɂ���Ė������I��������
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
                    //�X�N���[������Ԑ擪��
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


            //�p�[�e�B�����o�[���̃{�^�����쐬
            foreach(var member in partyStatus.GetAllyStatus())
            {
                GameObject buttonObj = Instantiate(characterButtonPrefab, selectCharacterPanel.transform);
                buttonObj.GetComponentInChildren<Text>().text = member.CharacterName;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => ShowStatus(member));
            }
            //�K�w����ԍŌ�ɕ��בւ�
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

        //�L�����N�^�[��
        characterNameText.text = allyStatus.CharacterName;

        //�^�C�g���̕\��
        string text = "���x��\n";
        text += "HP\n";
        text += "MP\n";
        text += "�o���l\n";
        text += "��Ԉُ�\n";
        text += "STR\n";
        text += "VIT\n";
        text += "INT\n";
        text += "RES\n";
        text += "AGI\n";
        text += "��������\n";
        text += "�����h��\n";
        text += "�U����\n";
        text += "�h���\n";
        text += "���@�h���\n";
        statusTitleText.text = text;

        //���݂�HP��MP�̕\��
        text = "\n";
        text += allyStatus.Hp + "\n";
        text += allyStatus.Mp + "\n";
        statusParam1Text.text = text;


        //�X�e�[�^�X�p�����[�^�̕\��
        text = allyStatus.Level + "\n";
        text += allyStatus.MaxHp + "\n";
        text += allyStatus.MaxMp + "\n";
        text += allyStatus.EarnedExperience + "\n";
        if (allyStatus.IsPoisonState)
        {
            text += "�� ";
        }
        if (allyStatus.IsNumbnessState)
        {
            text += "Ⴢ� ";
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
        //�X�N���[������Ԑ擪��
        scrollManager.Reset();

        int itemPanelButtonNum = 0;
        //�A�C�e���̕������A�C�e���p�l���{�^�����쐬
        foreach(var item in partyStatus.GetItemDictionary().Keys)
        {
            ItemPanelButton button = content.GetChild(itemPanelButtonNum).GetComponent<ItemPanelButton>();
            button.gameObject.SetActive(true);
            button.NameText.text = item.KanjiName;
            button.SetParam(item);
            //�{�^���Ɋ��蓖�Ă��Ă���֐����폜
            button.ItemButton.onClick.RemoveAllListeners();
            //����񕜃A�C�e���̏ꍇ�A�C�e���g�p�֐����{�^���Ɋ��蓖��
            if (item.ItemType == Item.Type.HPRecovery ||
                item.ItemType == Item.Type.MPRecovery ||
                item.ItemType == Item.Type.PoisonRecovery ||
                item.ItemType == Item.Type.NumbnessRecovery)
            {
                button.ItemButton.onClick.AddListener(() => UseItem(item));
            }

            //�A�C�e������\��
            button.AmountText.text = partyStatus.GetItemNum(item).ToString();

            //�p�[�e�B�����o�[���������Ă��镐��h�����Ζ��O�̉���E��\������
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

            //�A�C�e���{�^�����X�g�ɒǉ�
            itemPanelButtonList.Add(button.gameObject);

            itemPanelButtonNum++;
        }

        //�A�C�e�����𒴂���A�C�e���p�l���{�^�����\��
        for (int i = itemPanelButtonNum;i<content.transform.childCount;i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }

        //�ŏ��̃A�C�e����I����Ԃɂ���
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
        //�ȈՃX�e�[�^�X�p�l���̃{�^���Ɋ֐����蓖��
        foreach(var member in partyStatus.GetAllyStatus())
        {
            var button = simpleCharacterStatusPanels[memberNum];
            button.CharacterButton.onClick.RemoveAllListeners();
            button.CharacterButton.onClick.AddListener(() => UseItemToCharacter(member, item));
            memberNum++;
        }
        //�A�C�e���g�p��L�����N�^�[�I����ʂɈڍs
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
                notificationPanel.PopupNotification(toChara.CharacterName + "�͌��C�ł�");
            }
            else
            {
                toChara.Hp = toChara.Hp + item.Amount;
                notificationPanel.PopupNotification(toChara.CharacterName + "��HP��" + item.Amount + "�񕜂��܂���");
                //�A�C�e���������炷
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
        }
        else if(item.ItemType == Item.Type.MPRecovery)
        {
            if (toChara.Mp == toChara.MaxMp)
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "��MP�͍ő�ł�");
            }
            else
            {
                toChara.Mp = toChara.Mp + item.Amount;
                notificationPanel.PopupNotification(toChara.CharacterName + "��MP��" + item.Amount + "�񕜂��܂���");
                //�A�C�e���������炷
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
        }
        else if (item.ItemType == Item.Type.PoisonRecovery)
        {
            if (toChara.IsPoisonState)
            {
                toChara.IsPoisonState = false;
                notificationPanel.PopupNotification(toChara.CharacterName + "�͓ł���񕜂��܂���");
                //�A�C�e���������炷
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
            else
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "�͓ŏ�Ԃł͂���܂���");
            }
        }
        else if (item.ItemType == Item.Type.NumbnessRecovery)
        {
            if (toChara.IsNumbnessState)
            {
                toChara.IsNumbnessState = false;
                notificationPanel.PopupNotification(toChara.CharacterName + "��Ⴢꂩ��񕜂��܂���");
                //�A�C�e���������炷
                partyStatus.SetItemNum(item, partyStatus.GetItemNum(item) - 1);
            }
            else
            {
                notificationPanel.PopupNotification(toChara.CharacterName + "��Ⴢ��Ԃł͂���܂���");
            }
        }
        
        //�A�C�e���{�^�����X�g����Y������A�C�e����T���Đ����X�V����
        var itemButton = itemPanelButtonList.Find(obj => obj.transform.Find("NameText").GetComponent<Text>().text == item.KanjiName);
        itemButton.transform.Find("AmountText").GetComponent<Text>().text=partyStatus.GetItemNum(item).ToString();

        //�ȈՃX�e�[�^�X��ʂ��X�V
        UpdateSimpleStatusPanel();

        //�A�C�e������0�Ȃ�A�C�e���{�^����PartyStatus����A�C�e�����폜
        if (partyStatus.GetItemNum(item) == 0)
        {
            //�A�C�e���p�l�����X�g����Y���̃A�C�e�����폜
            itemPanelButtonList.Remove(itemButton);
            //�A�C�e���p�l���{�^�����g���\��
            itemButton.SetActive(false);
            //�A�N�e�B�u�Ȏq�I�u�W�F�N�g�̐��ɉe�����Ȃ��悤�Ɉ�ԉ��Ɉړ�
            itemButton.transform.SetAsLastSibling();
            //ItemDictionary����폜
            partyStatus.GetItemDictionary().Remove(item);

            menuMode = MenuMode.Item;
            //�m�F�{�^���̕\��
            notificationPanel.ShowNotificationButton(item.KanjiName + "�͂Ȃ��Ȃ�܂���");
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
    //���̊m�F�{�^���������ꂽ��A�C�e���ꗗ�ɖ߂�
    public void ConfirmNotification()
    {
        notificationButton.SetActive(false);

        itemPanelCanvasGroup.interactable = true;
        //��C�ɃA�C�e���ꗗ�܂Ŗ߂����߁A�L�����N�^�[�I���p�l���Ɩ����Ȃ����A�C�e���{�^���̃I�u�W�F�N�g�o�^���X�L�b�v
        selectedGameObjectStack.Pop();
        selectedGameObjectStack.Pop();
        EventSystem.current.SetSelectedGameObject(content.GetChild(0).gameObject);
        scrollManager.Reset();
    }
    public void UpdateSimpleStatusPanel()
    {
        int memberNumber = 0;
        //�p�[�e�B�����o�[�X�e�[�^�X���ȈՃX�e�[�^�X�p�l���ɔ��f
        foreach(var member in partyStatus.GetAllyStatus())
        {
            var charaPanel = simpleCharacterStatusPanels[memberNumber];
            charaPanel.gameObject.SetActive(true);
            charaPanel.NameText.text = member.CharacterName;
            charaPanel.LevelText.text=member.Level.ToString();
            charaPanel.HpText.text=member.Hp+"/"+member.MaxHp;
            charaPanel.MpText.text=member.Mp+"/"+member.MaxMp;
            
            //�c��HP�ɉ����ăe�L�X�g�̐F��ς���
            //���S���
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
            //���ɂ���
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
            //��������
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
            //�����ȏ�
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
        //�p�[�e�B�����o�[�������Ȃ���Δ�\��
        for (int i = memberNumber;i<simpleCharacterStatusPanels.Length;i++)
        {
            simpleCharacterStatusPanels[i].gameObject.SetActive(false);
        }
    }
}
