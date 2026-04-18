using UnityEngine;
using UnityEngine.UI;

public class SocialMediaGame : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject editingPanel;

    [Header("Sliders")]
    public Slider sliderAutentycznosc;
    public Slider sliderTrendy;

    [Header("Status Bars (Images with Fill Amount)")]
    public Image pasekTozsamosci;
    public Image pasekStresu;

    private float tozsamosc = 1.0f;
    private float stres = 0.0f;

    public void PublikujPost()
    {
        float autentycznosc = sliderAutentycznosc.value;
        float trendy = sliderTrendy.value;

        // LOGIKA:
        // Wysokie trendy = strata to¿samoœci
        tozsamosc -= trendy * 0.3f;

        // Niska autentycznoœæ = wiêcej stresu (bo post jest udawany)
        // LUB: Wysoka autentycznoœæ przy niskich trendach = ma³o lajków = stres
        if (autentycznosc > 0.7f && trendy < 0.3f)
        {
            stres += 0.2f; // Hejt lub brak zainteresowania boli
        }

        AktualizujUI();
        editingPanel.SetActive(false); // Zamknij panel po publikacji
        Debug.Log("Opublikowano! To¿samoœæ: " + tozsamosc + " Stres: " + stres);
    }

    void AktualizujUI()
    {
        pasekTozsamosci.fillAmount = Mathf.Clamp01(tozsamosc);
        pasekStresu.fillAmount = Mathf.Clamp01(stres);
    }
}