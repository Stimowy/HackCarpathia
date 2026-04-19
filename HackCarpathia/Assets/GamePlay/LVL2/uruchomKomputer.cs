using UnityEngine;

public class uruchomKomputer : MonoBehaviour
{
    [SerializeField] private GameObject pulpit;
    bool wlaczone = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WykonajLogikeKomputera();
        }
    }

    void WykonajLogikeKomputera()
    {
        popupMessage.triggerMessage("Uruchomiono fotoshop aby zmodyfikować zdjęcie");
        wlaczone = !wlaczone;
        pulpit.SetActive(wlaczone);
    }
}
