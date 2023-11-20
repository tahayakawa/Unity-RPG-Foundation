using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollUpAndDownButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private ScrollManager scrollManager;
    //自分自身のTransform
    [SerializeField]
    private Transform buttonTrans;
    //アイテム一覧画面に表示されるボタンの限界
    [SerializeField]
    private int buttonLimit=20;

    //ボタンが選択されたときに実行
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
    //ボタンが選択解除されたときに実行
    public void OnDeselect(BaseEventData eventData)
    {

    }
}
