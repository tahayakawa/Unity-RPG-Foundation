using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollUpAndDownButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private ScrollManager scrollManager;
    //�������g��Transform
    [SerializeField]
    private Transform buttonTrans;
    //�A�C�e���ꗗ��ʂɕ\�������{�^���̌��E
    [SerializeField]
    private int buttonLimit=20;

    //�{�^�����I�����ꂽ�Ƃ��Ɏ��s
    public void OnSelect(BaseEventData eventData)
    {
        if(buttonTrans.GetSiblingIndex()%buttonLimit==0|| buttonTrans.GetSiblingIndex() % buttonLimit == 1)
        {
            scrollManager.ScrollDown(this.transform.localPosition.y);
        }
        else if(buttonTrans.GetSiblingIndex() % buttonLimit == buttonLimit-2 || buttonTrans.GetSiblingIndex() % buttonLimit == buttonLimit - 1)
        {
            scrollManager.ScrollUp(this.transform.localPosition.y);
        }
    }
    //�{�^�����I���������ꂽ�Ƃ��Ɏ��s
    public void OnDeselect(BaseEventData eventData)
    {

    }
}
