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

    //�y�[�W�ԍ�
    private int pageNumber = 1;

    //�A�C�e���ꗗ�̃X�N���[���f�t�H���g�l
    private Vector3 defaultScrollValue;


    private void Awake()
    {
        defaultScrollValue = content.localPosition;
    }

    //���ɃX�N���[��
    public void ScrollDown(float buttonHeight)
    {
        float preHeight = content.localPosition.y;

        content.localPosition = new Vector3(content.localPosition.x, -buttonHeight - 28+300, content.localPosition.z);

        //�����̕ύX�������
        if(preHeight!= content.localPosition.y)
        {
            pageNumber++;
            pageText.text = "�y�[�W" + pageNumber;
        }
    }
    //��ɃX�N���[��
    public void ScrollUp(float buttonHeight)
    {
        float preHeight = content.localPosition.y;

        content.localPosition = new Vector3(content.localPosition.x, -buttonHeight - 568+300, content.localPosition.z);

        //�����̕ύX�������
        if (preHeight != content.localPosition.y)
        {
            pageNumber--;
            pageText.text = "�y�[�W" + pageNumber;
        }
    }
    public void Reset()
    {
        content.localPosition = defaultScrollValue;
        pageNumber = 1;
        pageText.text = "�y�[�W" + pageNumber;
    }
}
