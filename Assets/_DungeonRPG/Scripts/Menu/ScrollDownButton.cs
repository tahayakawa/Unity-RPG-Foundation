using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollDownButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private ScrollManager scrollManager;

    //ボタンが選択されたときに実行
    public void OnSelect(BaseEventData eventData)
    {
        scrollManager.ScrollDown(this.transform.localPosition.y);
    }
    //ボタンが選択解除されたときに実行
    public void OnDeselect(BaseEventData eventData)
    {

    }
}
