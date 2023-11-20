using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanelButton : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    [SerializeField]
    private Image selectedImage;
    //選択された時のSE
    [SerializeField]
    private AudioSource audioSource;

    private void OnEnable()
    {
        //アクティブになったとき自身がEventSystemで選択されていたら
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            selectedImage.enabled = true;
            audioSource.Play();
        }
        else
        {
            selectedImage.enabled = false;
        }
    }
    //ボタンが選択されたときに実行
    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
        audioSource.Play();
    }
    //ボタンが選択解除されたときに実行
    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;

    }
}
