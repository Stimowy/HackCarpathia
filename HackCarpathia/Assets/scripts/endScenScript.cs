using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScenScript : MonoBehaviour
{
    private string cytat = "Dorosłość to nie moment, w którym labirynt znika, ale chwila, w której przestajesz się w nim gubić. Nie musisz dopasowywać się do świata, który ktoś dla Ciebie narysował. Masz prawo wziąć ołówek i nakreślić własne horyzonty. Jesteś odpowiedzią, której szukałeś w każdym szyfrze.";

    [SerializeField] private TMP_Text textCytat;

    private void Start()
    {
        StartCoroutine(cytatKoniec(cytat));
    }

    private IEnumerator cytatKoniec(string c)
    {
        int min = 0;
        int max = c.Length;

        while (min < max)
        {
            textCytat.text = c[..min];
            min++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
