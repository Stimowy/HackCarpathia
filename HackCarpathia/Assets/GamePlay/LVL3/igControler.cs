using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class igControler : MonoBehaviour
{
    // Enum musi byæ publiczny dla ScriptableObjecta
    public enum Frakcja { Lewo, Prawo, Fake }

    [Header("G³ówne Referencje UI")]
    [SerializeField] private List<PostData> wszystkiePosty;
    [SerializeField] private Image wyswietlaczZdjecia;
    [SerializeField] private TextMeshProUGUI licznikPunktowText; // Przeci¹gnij tu TMPro z punktami
    [SerializeField] private Slider pasekCzasu; // Przeci¹gnij tu Slider czasu

    [Header("Zasoby Sceny")]
    [SerializeField] private GameObject instagram;
    [SerializeField] private GameObject pulpit;
    //[SerializeField] private popupMessage popups; // Referencja do skryptu popup

    // --- LOGIKA MECHANIKI ---
    [Header("Ustawienia Czasu")]
    [SerializeField] private float czasStartowyNaPost = 6.0f; // Tyle ma gracz na pierwsze zdjêcie
    [SerializeField] private float czasKoncowyNaPost = 1.5f; // Minimalny czas na reakcjê
    private float aktualnyCzasNaPost;
    private float timer;
    private bool czasOdmierzany = false;

    // Statystyki
    private int aktualnyIndeks = 0;
    private int zdobytePunkty = 0;

    // --- FUNKCJE STARTOWE ---

    public void TikGramStart()
    {
        pulpit.SetActive(false);
        instagram.SetActive(true);
        aktualnyIndeks = 0; // Reset
        zdobytePunkty = 0;
        AktualizujLicznikUI();

        // Pêtla trudnoœci zaczyna siê od czasu startowego
        aktualnyCzasNaPost = czasStartowyNaPost;

        popupMessage.triggerMessage("Reaguj szybko, by utrzymaæ neutralnoœæ.");

        NastepnyPost(); // Wyœwietl pierwszy post i w³¹cz czas
    }

    void Update()
    {
        // Mechanika odliczania czasu
        if (czasOdmierzany)
        {
            timer += Time.deltaTime;

            // UI paska czasu (Fill amount 0 do 1)
            if (pasekCzasu != null) pasekCzasu.value = 1.0f - (timer / aktualnyCzasNaPost);

            if (timer >= aktualnyCzasNaPost)
            {
                // KONIEC CZASU! Kara za brak reakcji
                KoniecCzasuKara();
            }
        }
    }

    private void NastepnyPost()
    {
        // Sprawdzamy czy nie koniec listy
        if (aktualnyIndeks >= wszystkiePosty.Count)
        {
            Debug.Log("Koniec zdjêæ na dziœ!");
            KoniecRozgrywki();
            return;
        }

        // --- Poka¿ Dane ---
        PostData dane = wszystkiePosty[aktualnyIndeks];
        if (wyswietlaczZdjecia != null) wyswietlaczZdjecia.sprite = dane.zdjecie;

        // --- TODO: Tu w kolejnym kroku dodamy Filtry z Trudnoœci ---

        // --- Uruchom Czas ---
        timer = 0;
        czasOdmierzany = true;

        Debug.Log($"Pokazujê post {aktualnyIndeks}. Frakcja: {dane.przynaleznosc}, Fake: {dane.toJestFake}");
    }

    // --- KLIKNIÊCIA UI (O¿ywione!) ---

    public void like()
    {
        if (!czasOdmierzany) return; // Zapobiega klikaniu po czasie
        PostData dane = wszystkiePosty[aktualnyIndeks];

        // LOGIKA RYZEK-NAGRODA (LIKE)
        // Like daje ma³o punktów, ale minimalny plus do zaufania frakcji

        int punkty = dane.punktyZaLike;

        // Jeœli post to Fake, Like karze minus do punktów
        if (dane.toJestFake) punkty = -5;

        zdobytePunkty += punkty;
        AktualizujLicznikUI();

        // Plus do zaufania (tylko u Lewo/Prawo, Fake nie ma 'w³asnego' zaufania)
        if (dane.przynaleznosc != Frakcja.Fake)
            FactionTrustManager.Instance.ZmienZaufanie(dane.przynaleznosc, 5);

        FinalizujReakcje();
    }

    public void Shere()
    {
        if (!czasOdmierzany) return;
        PostData dane = wszystkiePosty[aktualnyIndeks];

        // LOGIKA RYZEK-NAGRODA (SHARE)
        // Share daje du¿o punktów, ale potê¿ny plus do jednej frakcji I MINUS do przeciwnej

        int punkty = dane.punktyZaShare;
        int zmianaZaufania = 20; // Silna zmiana

        // UDostêpnienie FAKE: Powa¿na kara do punktów I katastrofa dla relacji OBU
        if (dane.toJestFake)
        {
            punkty = -30;
            zmianaZaufania = -15; // Zaufanie spada u OBU w TrustManagerze
            popupMessage.triggerMessage("Udostêpni³eœ dezinformacjê!");
        }

        zdobytePunkty += punkty;
        AktualizujLicznikUI();

        if (dane.przynaleznosc != Frakcja.Fake)
        {
            // Powa¿na zmiana relacji
            FactionTrustManager.Instance.ZmienZaufanie(dane.przynaleznosc, zmianaZaufania);

            // Share karze przeciwne frakcje (prosta polaryzacja)
            if (dane.przynaleznosc == Frakcja.Lewo) FactionTrustManager.Instance.ZmienZaufanie(Frakcja.Prawo, -zmianaZaufania);
            if (dane.przynaleznosc == Frakcja.Prawo) FactionTrustManager.Instance.ZmienZaufanie(Frakcja.Lewo, -zmianaZaufania);
        }
        else
        {
            // Jeœli to by³ Fake, karamy obie
            FactionTrustManager.Instance.ZmienZaufanie(Frakcja.Fake, zmianaZaufania);
        }

        FinalizujReakcje();
    }

    public void Skip()
    {
        if (!czasOdmierzany) return;
        PostData dane = wszystkiePosty[aktualnyIndeks];

        // LOGIKA RYZEK-NAGRODA (SKIP)
        // Skip nie daje punktów, ale karze minus do zaufania u pominiêtej frakcji 
        // (uwa¿a, ¿e j¹ ignorujesz)

        if (dane.przynaleznosc != Frakcja.Fake)
        {
            FactionTrustManager.Instance.ZmienZaufanie(dane.przynaleznosc, -10);
        }

        FinalizujReakcje();
    }

    // --- Funkcje Pomocnicze ---

    private void FinalizujReakcje()
    {
        czasOdmierzany = false;
        if (pasekCzasu != null) pasekCzasu.value = 1; // Zresetuj UI czasu

        aktualnyIndeks++;

        // --- MECHANIKA SKRACANIA CZASU (TRUDNOŒÆ) ---
        // Z ka¿dym zdjêciem czas skraca siê o 5%, a¿ osi¹gnie minimum
        aktualnyCzasNaPost = Mathf.Max(aktualnyCzasNaPost * 0.95f, czasKoncowyNaPost);

        // Poka¿ kolejne zdjêcie
        NastepnyPost();
    }

    private void KoniecCzasuKara()
    {
        // Jeœli gracz œpi: minus do punktów, minus do relacji obu frakcji
        zdobytePunkty -= 5;
        AktualizujLicznikUI();

        // Kara do obu zaufania (nierzadko œpisz)
        FactionTrustManager.Instance.ZmienZaufanie(Frakcja.Fake, -10);

        popupMessage.triggerMessage("Zaspa³eœ! Dezinformacja siê szerzy.");

        FinalizujReakcje();
    }

    private void AktualizujLicznikUI()
    {
        if (licznikPunktowText != null) licznikPunktowText.text = "Punkty: " + zdobytePunkty.ToString();
    }

    private void KoniecRozgrywki()
    {
        Debug.Log($"KONIEC. Wynik: {zdobytePunkty}");
        instagram.SetActive(false);
        pulpit.SetActive(true);
        popupMessage.triggerMessage("Wylogowano. Twój wynik: " + zdobytePunkty);
    }
}