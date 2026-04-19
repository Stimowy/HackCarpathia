using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cycatyScript : MonoBehaviour
{
    [SerializeField] private int nastepnaScena = 4;
    [SerializeField] private TMP_Text textCytat;
    [SerializeField] private string cytat = "";

    private void Start()
    {
        StartCoroutine(pisownia(cytat));
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
        SceneManager.LoadScene(nastepnaScena);
    }
}
