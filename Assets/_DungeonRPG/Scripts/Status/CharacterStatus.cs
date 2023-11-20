using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
    //�@�L�����N�^�[�̖��O
    [SerializeField]
    private string characterName = "";
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
    //�@�ŏ�Ԃ��ǂ���
    [SerializeField]
    private bool isPoisonState = false;
    public bool IsPoisonState
    {
        get { return isPoisonState; }
        set { isPoisonState = value; }
    }
    //�@Ⴢ��Ԃ��ǂ���
    [SerializeField]
    private bool isNumbnessState = false;
    public bool IsNumbnessState
    {
        get { return isNumbnessState; }
        set { isNumbnessState = value; }
    }

    //�@�L�����N�^�[�̃��x��
    [SerializeField]
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    //�@�ő�HP
    [SerializeField]
    private int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    //�@HP
    [SerializeField]
    private int hp = 100;
    public int Hp
    {
        get { return hp; }
        set { hp = Mathf.Max(0, Mathf.Min(MaxHp, value)); }
    }
    //�@�ő�MP
    [SerializeField]
    private int maxMp = 50;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }
    //�@MP
    [SerializeField]
    private int mp = 50;
    public int Mp
    {
        get { return mp; }
        set { mp = Mathf.Max(0, Mathf.Min(MaxMp, value)); }
    }

    //�@��
    [SerializeField]
    private int strength = 10;
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    // ������
    [SerializeField]
    private int vitality = 10;
    public int Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    //�@�m��
    [SerializeField]
    private int intelligence = 10;
    public int Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    // ��R��
    [SerializeField]
    private int resist = 10;
    public int Resist
    {
        get { return resist; }
        set { resist = value; }
    }
    //�@�f����
    [SerializeField]
    private int agility = 10;
    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }
}
