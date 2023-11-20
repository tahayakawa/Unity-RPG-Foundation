using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCharacterStatusPanel : MonoBehaviour
{
    [SerializeField]
    private Button characterButton;
    public Button CharacterButton => characterButton;
    [SerializeField]
    private Text nameText;
    public Text NameText => nameText;

    [SerializeField]
    private Text levelTitle;
    public Text LevelTitle => levelTitle;
    [SerializeField]
    private Text levelText;
    public Text LevelText => levelText;

    [SerializeField]
    private Text hpTitle;
    public Text HpTitle => hpTitle;
    [SerializeField]
    private Text hpText;
    public Text HpText => hpText;

    [SerializeField]
    private Text mpTitle;
    public Text MpTitle => mpTitle;
    [SerializeField]
    private Text mpText;
    public Text MpText => mpText;

}
