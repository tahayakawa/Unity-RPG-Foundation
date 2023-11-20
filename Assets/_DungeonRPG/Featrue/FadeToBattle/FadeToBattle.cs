using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBattle : MonoBehaviour
{
    [SerializeField]
    private Material material;
    //フェードamountの到達地
    private float destinationAmount;

    private void Start()
    {
        material.SetFloat("_Amount", 0f);
    }

    //カメラに取り付けられると呼ばれる
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
