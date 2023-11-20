using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
    //　キャラクターの名前
    [SerializeField]
    private string characterName = "";
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
    //　毒状態かどうか
    [SerializeField]
    private bool isPoisonState = false;
    public bool IsPoisonState
    {
        get { return isPoisonState; }
        set { isPoisonState = value; }
    }
    //　痺れ状態かどうか
    [SerializeField]
    private bool isNumbnessState = false;
    public bool IsNumbnessState
    {
        get { return isNumbnessState; }
        set { isNumbnessState = value; }
    }

    //　キャラクターのレベル
    [SerializeField]
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    //　最大HP
    [SerializeField]
    private int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    //　HP
    [SerializeField]
    private int hp = 100;
    public int Hp
    {
        get { return hp; }
        set { hp = Mathf.Max(0, Mathf.Min(MaxHp, value)); }
    }
    //　最大MP
    [SerializeField]
    private int maxMp = 50;
    public int MaxMp
    {
        get { return maxMp; }
        set { maxMp = value; }
    }
    //　MP
    [SerializeField]
    private int mp = 50;
    public int Mp
    {
        get { return mp; }
        set { mp = Mathf.Max(0, Mathf.Min(MaxMp, value)); }
    }

    //　力
    [SerializeField]
    private int strength = 10;
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    // 生命力
    [SerializeField]
    private int vitality = 10;
    public int Vitality
    {
        get { return vitality; }
        set { vitality = value; }
    }
    //　知力
    [SerializeField]
    private int intelligence = 10;
    public int Intelligence
    {
        get { return intelligence; }
        set { intelligence = value; }
    }
    // 抵抗力
    [SerializeField]
    private int resist = 10;
    public int Resist
    {
        get { return resist; }
        set { resist = value; }
    }
    //　素早さ
    [SerializeField]
    private int agility = 10;
    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }
}
