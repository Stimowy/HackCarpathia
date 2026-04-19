using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class SocialMediaManager : MonoBehaviour
{
    [Header("UI Controls")]
    public Slider sliderAutentycznosc;
    public Slider sliderTrendy;
    public Image pasekTozsamosci; // Niebieski
    public Image pasekStresu;      // Czerwony

    [Header("Wartoœci bazowe")]
    [Range(0, 100)] public float tozsamosc = 100f;
    [Range(0, 100)] public float stres = 0f;

    void Update()
    {
        // Jeœli któregokolwiek elementu brakuje, przerwij wykonywanie, zamiast sypaæ b³êdami
        if (sliderAutentycznosc == null || sliderTrendy == null ||
            pasekTozsamosci == null || pasekStresu == null)
        {
            return;
        }

        ObliczStatystyki();
        AktualizujUI();
    }

    void ObliczStatystyki()
    {
        // 1. Zbyt wysokie trendy niszcz¹ to¿samoœæ (poddawanie siê presji)
        // Jeœli Trendy > Autentycznoœæ, to¿samoœæ spada
        if (sliderTrendy.value > sliderAutentycznosc.value)
        {
            tozsamosc -= (sliderTrendy.value - sliderAutentycznosc.value) * Time.deltaTime * 0.5f;
        }
        else
        {
            // Powolna regeneracja to¿samoœci, gdy jesteœmy autentyczni
            tozsamosc += 0.1f * Time.deltaTime;
        }

        // 2. Zbyt wysoka autentycznoœæ (brak filtrów, surowoœæ) zwiêksza stres
        // bo post dostanie mniej lajków/bêdzie oceniany negatywnie
        if (sliderAutentycznosc.value > 0.7f) // Progu 70% autentycznoœci
        {
            stres += (sliderAutentycznosc.value * 0.2f) * Time.deltaTime;
        }
        else
        {
            stres -= 0.1f * Time.deltaTime;
        }

        // Clampowanie wartoœci
        tozsamosc = Mathf.Clamp(tozsamosc, 0, 100);
        stres = Mathf.Clamp(stres, 0, 100);
        int intTozsamosc = (int)tozsamosc;
        int intStres = (int)stres;
    }

    void AktualizujUI()
    {
        // Zak³adamy, ¿e paski to Image z typem "Filled"
        pasekTozsamosci.fillAmount = tozsamosc / 100f;
        pasekStresu.fillAmount = stres / 100f;

        // Opcjonalnie: Zmiana koloru paska stresu na bardziej intensywny
        pasekStresu.color = Color.Lerp(Color.yellow, Color.red, stres / 100f);
    }

    public void PublikujPost()
    {
        int wynik_lvl2 = Random.Range( 0, 200 );
        popupMessage.triggerMessage("Wynik: " + wynik_lvl2);

        Invoke(nameof(odliczanie), 3f);
    }

    private void odliczanie()
    {
        SceneManager.LoadScene(4);
    }
}