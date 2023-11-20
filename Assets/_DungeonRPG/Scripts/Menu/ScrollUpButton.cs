using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollUpButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private ScrollManager scrollManager;


    //�{�^�����I�����ꂽ�Ƃ��Ɏ��s
    public void OnSelect(BaseEventData eventData)
    {
        scrollManager.ScrollUp(this.transform.localPosition.y);
    }
    //�{�^�����I���������ꂽ�Ƃ��Ɏ��s
    public void OnDeselect(BaseEventData eventData)
    {

    }
}
