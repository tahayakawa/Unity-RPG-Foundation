using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBattle : MonoBehaviour
{
    [SerializeField]
    private Material material;
    //�t�F�[�hamount�̓��B�n
    private float destinationAmount;

    private void Start()
    {
        material.SetFloat("_Amount", 0f);
    }

    //�J�����Ɏ��t������ƌĂ΂��
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
