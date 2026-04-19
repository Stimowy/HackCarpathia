using UnityEngine;

public class uruchomKomputer : MonoBehaviour
{
    [SerializeField] private GameObject pulpit;
    bool wlaczone = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("dziala");
            WykonajLogikeKomputera();
        }
    }

    void WykonajLogikeKomputera()
    {
        wlaczone = !wlaczone;
        pulpit.SetActive(wlaczone);
    }
}
