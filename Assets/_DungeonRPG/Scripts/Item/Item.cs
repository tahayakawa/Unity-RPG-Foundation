using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        HPRecovery,
        MPRecovery,
        PoisonRecovery,
        NumbnessRecovery,
        WeaponAll,
        WeaponSword,
        WeaponSpear,
        ShieldSmall,
        ShieldBig,
        ArmorAll,
        ArmorHeavy,
        ArmorLight,
        Material,
        Valuables
    }

    //　アイテムの種類
    [SerializeField]
    private Type itemType = Type.HPRecovery;
    public Type ItemType
    {
        get { return itemType; }
    }
    //　アイテムの漢字名
    [SerializeField]
    private string kanjiName = "";
    public string KanjiName
    {
        get { return kanjiName; }
    }
    //　アイテムの平仮名名
    [SerializeField]
    private string hiraganaName = "";
    public string HiraganaName
    {
        get { return hiraganaName; }
    }
    //　アイテム情報
    [SerializeField]
    private string information = "";
    public string Information
    {
        get { return information; }
    }
    //　アイテムのパラメータ
    [SerializeField]
    private int amount = 0;
    public int Amount
    {
        get { return amount; }
    }
}