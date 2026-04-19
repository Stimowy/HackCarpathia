using UnityEngine;

public class wylaczKomputer : MonoBehaviour
{
    [SerializeField] private GameObject pulpit;
    public void wylacz_komputer()
    {
        pulpit.SetActive(false);
    }
}
