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

    //�@�A�C�e���̎��
    [SerializeField]
    private Type itemType = Type.HPRecovery;
    public Type ItemType
    {
        get { return itemType; }
    }
    //�@�A�C�e���̊�����
    [SerializeField]
    private string kanjiName = "";
    public string KanjiName
    {
        get { return kanjiName; }
    }
    //�@�A�C�e���̕�������
    [SerializeField]
    private string hiraganaName = "";
    public string HiraganaName
    {
        get { return hiraganaName; }
    }
    //�@�A�C�e�����
    [SerializeField]
    private string information = "";
    public string Information
    {
        get { return information; }
    }
    //�@�A�C�e���̃p�����[�^
    [SerializeField]
    private int amount = 0;
    public int Amount
    {
        get { return amount; }
    }
}