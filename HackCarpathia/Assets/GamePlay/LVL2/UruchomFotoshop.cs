using UnityEngine;

public class UruchomFotoshop : MonoBehaviour
{
    [SerializeField] private GameObject m_Fotoshop;
    bool wlaczone = false;
    public void pokaz_usun()
    {
        wlaczone = !wlaczone;
        m_Fotoshop.SetActive(wlaczone);
    }

}
