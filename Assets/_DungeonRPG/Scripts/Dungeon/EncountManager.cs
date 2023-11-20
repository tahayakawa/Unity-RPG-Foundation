using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EncountManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject battleUI;
    [SerializeField]
    private Transform player;
    //戦闘画面遷移時のフェードに使用するマテリアル
    [SerializeField]
    private Material material;
    [SerializeField]
    private float fadeSpeed = 1;
    [SerializeField]
    private AudioSource audioSource;

    private Vector3 firstPosition;
    private void Start()
    {
        firstPosition=player.position;
    }
    private void Update()
    {
        float distance=Vector3.Distance(firstPosition, player.position);
        if (distance > 5)
        {
            audioSource.Play();
            firstPosition = player.position;
            StartCoroutine(FadeMapToBattle(0.1f));
        }
    }
    public IEnumerator FadeMapToBattle(float destinationAmount)
    {
        gameManager.SetState(GameManager.State.Battle);

        while (Mathf.Abs(material.GetFloat("_Amount") - destinationAmount) > 0.01f)
        {
            material.SetFloat("_Amount",Mathf.Lerp(material.GetFloat("_Amount"),destinationAmount, fadeSpeed*Time.deltaTime));
            yield return null;
        }
        material.SetFloat("_Amount", destinationAmount);

        battleUI.SetActive(true);
    }
    public IEnumerator FadeMapToBattle()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //通常状態に戻す
        gameManager.SetState(GameManager.State.InDungeon);
        firstPosition = player.position;

        while (Mathf.Abs(material.GetFloat("_Amount")) < 0.01f)
        {
            material.SetFloat("_Amount", Mathf.Lerp(material.GetFloat("_Amount"), 0, fadeSpeed * Time.deltaTime));
            yield return null;
        }
        material.SetFloat("_Amount", 0);
    }
}
