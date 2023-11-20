using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNotificationPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject itemNotificationPanel;
    [SerializeField]
    private Text notificationText;
    [SerializeField]
    private Button notificationButton;
    [SerializeField]
    private Text notificationButtonText;
    private Coroutine coroutine = null;

    public void PopupNotification(string text)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine=StartCoroutine(EnableNotificationBrieflyCor(text));
    }
    public void ShowNotificationButton(string text)
    {
        notificationButtonText.text = text;
        notificationButton.gameObject.SetActive(true);
    }
    private IEnumerator EnableNotificationBrieflyCor(string text)
    {
        itemNotificationPanel.SetActive(false);
        yield return new WaitForEndOfFrame();

        notificationText.text = text;
        itemNotificationPanel.SetActive(true);

        yield return new WaitForSeconds(1);

        itemNotificationPanel.SetActive(false);
        coroutine = null;
    }
}
