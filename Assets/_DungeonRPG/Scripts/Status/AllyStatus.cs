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

    //　獲得経験値
    [SerializeField]
    private int earnedExperience = 0;
    public int EarnedExperience
    {
        get { return earnedExperience; }
        set { earnedExperience = value;}
    }
    //　装備している武器
    [SerializeField]
    private Item equipWeapon = null;
    public Item EquipWeapon
    {
        get { return equipWeapon; }
        set { equipWeapon = value; }
    }
    //　装備している鎧
    [SerializeField]
    private Item equipArmor = null;
    public Item EquipArmor
    {
        get { return equipArmor; }
        set { equipArmor = value; }
    }

    // 攻撃力
    public int GetAttack()
    {
        int attack = Strength + EquipWeapon?.Amount ?? 0;
        return attack;
    }
    // 防御力
    public int GetDefense()
    {
        int defense = Vitality + EquipArmor?.Amount ?? 0;
        return defense;
    }
    //　魔法防御力
    public int GetMagicDefense()
    {
        int magicDefense = (Vitality + Resist) / 2 + EquipArmor?.Amount ?? 0;
        return magicDefense;
    }
}