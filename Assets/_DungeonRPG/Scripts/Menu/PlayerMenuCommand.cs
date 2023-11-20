using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMenuCommand : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject menuUI;

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (!menuUI.activeSelf)
            {
                //���j���[��Ԃɂ���
                gameManager.SetState(GameManager.State.Menu);
            }
            else
            {
                ExitMenu();
            }
            //���j���[UI�̃I���I�t
            menuUI.SetActive(!menuUI.activeSelf);
        }
    }
    public void ExitMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //�ʏ��Ԃɖ߂�
        gameManager.SetState(GameManager.State.InDungeon);
    }
}
