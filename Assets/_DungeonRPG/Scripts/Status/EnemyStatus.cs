using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="EnemyStatus",menuName ="CreateEnemyStatus")]
public class EnemyStatus : CharacterStatus
{
    //倒したときの経験値
    [SerializeField]
    private int experience = 10;
    public int Experience => experience;

    //倒したときのお金
    [SerializeField]
    private int money = 10;
    public int Money => money;

    //ドロップアイテムとドロップ率（パーセント）
    [SerializeField]
    private ItemDictionary dropItemDictionary = null;

    public ItemDictionary GetDropItemDictionary()
    {
        return dropItemDictionary;
    }
    public int GetDropItemRate(Item item)
    {
        return dropItemDictionary[item];
    }
}
