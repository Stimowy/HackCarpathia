using UnityEngine;
using UnityEngine.UI; // Dla paska postêpu
using TMPro; // Dla tekstu

public class FactionTrustManager : MonoBehaviour
{
    public static FactionTrustManager Instance; // £atwy dostêp

    [Header("Statystyki Frakcji")]
    [SerializeField, Range(-100, 100)] private int zaufanieLewo = 0;
    [SerializeField, Range(-100, 100)] private int zaufaniePrawo = 0;

    [Header("UI do podpiêcia")]
    [SerializeField] private Slider pasekLewo; // Paski postêpu (Fill amount od 0 do 1)
    [SerializeField] private Slider pasekPrawo;

    void Awake()
    {
        Instance = this;
        AktualizujUI();
    }

    public void ZmienZaufanie(igControler.Frakcja frakcja, int zmiana)
    {
        if (frakcja == igControler.Frakcja.Lewo) zaufanieLewo = Mathf.Clamp(zaufanieLewo + zmiana, -100, 100);
        if (frakcja == igControler.Frakcja.Prawo) zaufaniePrawo = Mathf.Clamp(zaufaniePrawo + zmiana, -100, 100);

        // Jeœli post to Fake, to kara za pomy³kê idzie do OBU frakcji
        if (frakcja == igControler.Frakcja.Fake)
        {
            zaufanieLewo -= 15; // Przyk³adowa sta³a kara
            zaufaniePrawo -= 15;
        }

        AktualizujUI();
        Debug.Log($"Zaufanie L: {zaufanieLewo}, P: {zaufaniePrawo}");
    }

    private void AktualizujUI()
    {
        // Zamieniamy zakres -100 to 100 na zakres 0 to 1 dla slidera
        if (pasekLewo != null) pasekLewo.value = (zaufanieLewo + 100) / 200f;
        if (pasekPrawo != null) pasekPrawo.value = (zaufaniePrawo + 100) / 200f;
    }
}