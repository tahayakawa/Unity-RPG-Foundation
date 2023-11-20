using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "AllyStatus", menuName = "CreateAllyStatus")]
public class AllyStatus : CharacterStatus
{

    //�@�l���o���l
    [SerializeField]
    private int earnedExperience = 0;
    public int EarnedExperience
    {
        get { return earnedExperience; }
        set { earnedExperience = value;}
    }
    //�@�������Ă��镐��
    [SerializeField]
    private Item equipWeapon = null;
    public Item EquipWeapon
    {
        get { return equipWeapon; }
        set { equipWeapon = value; }
    }
    //�@�������Ă���Z
    [SerializeField]
    private Item equipArmor = null;
    public Item EquipArmor
    {
        get { return equipArmor; }
        set { equipArmor = value; }
    }

    // �U����
    public int GetAttack()
    {
        int attack = Strength + EquipWeapon?.Amount ?? 0;
        return attack;
    }
    // �h���
    public int GetDefense()
    {
        int defense = Vitality + EquipArmor?.Amount ?? 0;
        return defense;
    }
    //�@���@�h���
    public int GetMagicDefense()
    {
        int magicDefense = (Vitality + Resist) / 2 + EquipArmor?.Amount ?? 0;
        return magicDefense;
    }
}