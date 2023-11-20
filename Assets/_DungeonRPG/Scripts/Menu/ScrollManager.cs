using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Text pageText;

    //ページ番号
    private int pageNumber = 1;

    //アイテム一覧のスクロールデフォルト値
    private Vector3 defaultScrollValue;


    private void Awake()
    {
        defaultScrollValue = content.localPosition;
    }

    //下にスクロール
    public void ScrollDown(float buttonHeight)
    {
        float preHeight = content.localPosition.y;

        content.localPosition = new Vector3(content.localPosition.x, -buttonHeight - 28+300, content.localPosition.z);

        //高さの変更があれば
        if(preHeight!= content.localPosition.y)
        {
            pageNumber++;
            pageText.text = "ページ" + pageNumber;
        }
    }
    //上にスクロール
    public void ScrollUp(float buttonHeight)
    {
        float preHeight = content.localPosition.y;

        content.localPosition = new Vector3(content.localPosition.x, -buttonHeight - 568+300, content.localPosition.z);

        //高さの変更があれば
        if (preHeight != content.localPosition.y)
        {
            pageNumber--;
            pageText.text = "ページ" + pageNumber;
        }
    }
    public void Reset()
    {
        content.localPosition = defaultScrollValue;
        pageNumber = 1;
        pageText.text = "ページ" + pageNumber;
    }
}
