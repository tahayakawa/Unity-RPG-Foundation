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

    //　アイテムと個数のDictionary
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

    //　アイテムが登録された順番のItemDictionaryを返す
    public ItemDictionary GetItemDictionary()
    {
        return itemDictionary;
    }
    //　平仮名の名前でソートしたItemDictionaryを返す
    public IOrderedEnumerable<KeyValuePair<Item, int>> GetSortItemDictionary()
    {
        return itemDictionary.OrderBy(item => item.Key.HiraganaName);
    }
    public int SetItemNum(Item tempItem, int num)
    {
        return itemDictionary[tempItem] = num;
    }
    //　アイテムの数を返す
    public int GetItemNum(Item item)
    {
        return itemDictionary[item];
    }
}
