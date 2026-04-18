using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class popupMessage : MonoBehaviour
{
    // Wywo³anie funkcji w innych skryptach:
    // - wywo³anie wiadomosci bez limitu czasu - popupMessage.triggerMessage(treœæ wiadomoœci, d³ugoœæ wiadomoœci, d³ugoœæ zanikania wiadomoœci);
    // - wy³¹czenie wiadomoœci - popupMessage.closeMessage();

    [SerializeField] private static GameObject panelPopup;
    [SerializeField] private static TMP_Text messagePopup;

    private static float alphaPanelDefult = 0.66666f;
    private static popupMessage instance;

    private void Awake()
    {
        instance = this;
        panelPopup = GameObject.Find("popupPanel");
        messagePopup = GameObject.Find("popupText").GetComponent<TMP_Text>();
        panelPopup.SetActive(false);
        Image imgPanel = panelPopup.GetComponent<Image>();
        Color colorPanel = imgPanel.color;
        colorPanel.a = alphaPanelDefult;
        imgPanel.color = colorPanel;
    }

    public static void triggerMessage(string message) // funkcja wypisuje wiadomosc bez limitu czasowego
    {
        panelPopup.SetActive(true);
        messagePopup.text = message;
        setSizeMessage(message);
    }

    public static void timeMessage(string message, float timeSchow, float timeOpacity) // funkcja wypisuje wiadomosc z limitem czasu okreœlonym w trigerze
    {
        panelPopup.SetActive(true);
        messagePopup.text = message;
        setSizeMessage(message);

        instance.StartCoroutine(instance.PopupRoutine(timeOpacity, timeSchow));
    }

    private IEnumerator PopupRoutine(float fadeSpeed, float displayDuration) // funkcja zmienia przezroczystoœæ panelu, a nastêpnie po czasie wy³¹cza wiadomoœæ
    {
        Image imgPanel = panelPopup.GetComponent<Image>();
        TMP_Text colorPopup = messagePopup.GetComponent<TMP_Text>();
        Color colorPanel = imgPanel.color;
        Color textColor = messagePopup.color;
        colorPanel.a = 0f;
        textColor.a = 0f;
        imgPanel.color = colorPanel;


        while (colorPanel.a < alphaPanelDefult)
        {
            colorPanel.a += Time.deltaTime / fadeSpeed;
            textColor.a += Time.deltaTime / fadeSpeed;
            imgPanel.color = colorPanel;
            colorPopup.color = textColor;
            yield return new WaitForFixedUpdate();
        }

        colorPanel.a = alphaPanelDefult;

        yield return new WaitForSeconds(displayDuration);

        while (colorPanel.a > 0.0f)
        {
            colorPanel.a -= Time.deltaTime / fadeSpeed;
            textColor.a -= Time.deltaTime / fadeSpeed;
            imgPanel.color = colorPanel;
            colorPopup.color = textColor;
            yield return new WaitForFixedUpdate();
        }
        
        closeMessage();
    }

    public static void closeMessage() // funkcja wy³¹cza wiadomosc
    {
        panelPopup.SetActive(false);
        messagePopup.text = "";

        Image imgPanel = panelPopup.GetComponent<Image>();
        TMP_Text colorPopup = messagePopup.GetComponent<TMP_Text>();
        Color colorPanel = imgPanel.color;
        Color textColor = messagePopup.color;
        colorPanel.a = alphaPanelDefult;
        textColor.a = 1f;
        imgPanel.color = colorPanel;
        colorPopup.color = textColor;
    }

    private static void setSizeMessage(string message) // funkcja zmienia wielkoœæ wiadomoœci 
    {
        int size = message.Length;
        if (size > 14)
        {
            int x = 300;
            int y = 50;
            int posY = 40;
            RectTransform rt = panelPopup.GetComponent<RectTransform>();

            if (size > 50)
            {
                x = 984;
                int z = (int)Math.Ceiling((decimal)size / 50);
                y += 24 * z;
                posY += 10 * z;
            }
            else
            {
                size -= 14;
                x += size * 19;
            }

            rt.sizeDelta = new Vector2(x, y);
            rt.position = new Vector3 (rt.position.x, posY, 0);
        }
    }
}
