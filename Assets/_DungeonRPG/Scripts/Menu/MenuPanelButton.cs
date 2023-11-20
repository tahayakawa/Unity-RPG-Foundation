using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPanelButton : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    [SerializeField]
    private Image selectedImage;
    //�I�����ꂽ����SE
    [SerializeField]
    private AudioSource audioSource;

    private void OnEnable()
    {
        //�A�N�e�B�u�ɂȂ����Ƃ����g��EventSystem�őI������Ă�����
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
    //�{�^�����I�����ꂽ�Ƃ��Ɏ��s
    public void OnSelect(BaseEventData eventData)
    {
        selectedImage.enabled = true;
        audioSource.Play();
    }
    //�{�^�����I���������ꂽ�Ƃ��Ɏ��s
    public void OnDeselect(BaseEventData eventData)
    {
        selectedImage.enabled = false;

    }
}
