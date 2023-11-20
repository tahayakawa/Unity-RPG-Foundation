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
                //メニュー状態にする
                gameManager.SetState(GameManager.State.Menu);
            }
            else
            {
                ExitMenu();
            }
            //メニューUIのオンオフ
            menuUI.SetActive(!menuUI.activeSelf);
        }
    }
    public void ExitMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //通常状態に戻す
        gameManager.SetState(GameManager.State.InDungeon);
    }
}
