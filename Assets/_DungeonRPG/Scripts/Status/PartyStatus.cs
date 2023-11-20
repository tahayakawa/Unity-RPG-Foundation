using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="PartyStatus",menuName ="CreatePartyStatus")]
public class PartyStatus : ScriptableObject
{
    [SerializeField]
    private int money = 0;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    [SerializeField]
    private List<AllyStatus> partyMembers = null;

    public void SetAllyStatus(AllyStatus allyStatus)
    {
        if(!partyMembers.Contains(allyStatus))
        {
            partyMembers.Add(allyStatus);
        }
    }
    public List<AllyStatus > GetAllyStatus()
    {
        return partyMembers;
    }

    //�@�A�C�e���ƌ���Dictionary
    [SerializeField]
    private ItemDictionary itemDictionary = null;

    public void CreateItemDictionary(ItemDictionary itemDictionary)
    {
        this.itemDictionary = itemDictionary;
    }

    public void SetItemDictionary(Item item, int num = 0)
    {
        itemDictionary.Add(item, num);
    }

    //�@�A�C�e�����o�^���ꂽ���Ԃ�ItemDictionary��Ԃ�
    public ItemDictionary GetItemDictionary()
    {
        return itemDictionary;
    }
    //�@�������̖��O�Ń\�[�g����ItemDictionary��Ԃ�
    public IOrderedEnumerable<KeyValuePair<Item, int>> GetSortItemDictionary()
    {
        return itemDictionary.OrderBy(item => item.Key.HiraganaName);
    }
    public int SetItemNum(Item tempItem, int num)
    {
        return itemDictionary[tempItem] = num;
    }
    //�@�A�C�e���̐���Ԃ�
    public int GetItemNum(Item item)
    {
        return itemDictionary[item];
    }
}
