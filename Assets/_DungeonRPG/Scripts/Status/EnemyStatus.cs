using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="EnemyStatus",menuName ="CreateEnemyStatus")]
public class EnemyStatus : CharacterStatus
{
    //�|�����Ƃ��̌o���l
    [SerializeField]
    private int experience = 10;
    public int Experience => experience;

    //�|�����Ƃ��̂���
    [SerializeField]
    private int money = 10;
    public int Money => money;

    //�h���b�v�A�C�e���ƃh���b�v���i�p�[�Z���g�j
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
