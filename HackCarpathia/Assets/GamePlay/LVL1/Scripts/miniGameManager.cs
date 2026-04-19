using System.Collections;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniGameManager : MonoBehaviour
{
    private string cytat = "Dorosłość to moment, w którym uświadamiasz sobie, że świat rzadko mówi do Ciebie wprost. Egzaminy, zasady i społeczne oczekiwania to tylko \"szyfr\". Twoim zadaniem nie jest tylko bezmyślne wypełnianie rubryk, ale zrozumienie mechanizmów, które nimi sterują.";
    [Header("cytat zakończenia")]
    [SerializeField] private TMP_Text textCytat;

    [Header("przypisania objektów")]
    [SerializeField] private GameObject stol;
    [SerializeField] private GameObject kartka;

    public string wynik;
    public TMP_Text textWynik;
    private string poprawneHaslo = "zrozum przeszlosc";

    private void Start()
    {
        popupMessage.timeMessage("dowiedz sie jaką wartość ma klucz i rozszyfruj hasło, aby wpisać hasło napisz je na klawiaturzę", 5f, 2f);
        stol.SetActive(true);
        kartka.SetActive(false);
    }

    private void Update()
    {
        string input = Input.inputString;

        if (!string.IsNullOrEmpty(input))
        {
            foreach (char c in input)
            {
                if (c == '\b')
                {
                    if (wynik.Length > 0)
                    {
                        wynik = wynik[..^1];
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    sprawdz(wynik);
                }
                else
                {
                    wynik += c;
                }
            }

            if (textWynik != null)
            {
                textWynik.text = wynik;
            }
        }
    }

    private void sprawdz(string haslo)
    {
        if (haslo.ToLower() == poprawneHaslo)
        {
            stol.SetActive(false);
            kartka.SetActive(true);
            StartCoroutine(pisownia(cytat));
        }
        else
        {
            popupMessage.timeMessage("niepoprawna odpowiedź, spróbuj ponownie", 4f, 2f);
        }
    }

    private IEnumerator pisownia(string text)
    {
        int min = 0;
        int max = text.Length;

        while (min < max)
        {
            textCytat.text = text[..min];
            min++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }
}
