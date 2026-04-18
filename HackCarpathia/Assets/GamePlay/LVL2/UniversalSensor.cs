// gracz musi miec tag Player

using UnityEngine;
using UnityEngine.Events;

public class UniversalSensor : MonoBehaviour
{
    [Header("Ustawienia Triggera")]
    [Tooltip("Wpisz Tag obiektu, który ma aktywowaæ trigger (np. Player)")]
    public string targetTag = "Player";

    [Header("Wydarzenia")]
    public UnityEvent onEnter; // Co ma siê staæ po wejœciu
    public UnityEvent onExit;  // Co ma siê staæ po wyjœciu (opcjonalnie)

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy czy to co wesz³o w trigger ma odpowiedni Tag
        if (other.CompareTag(targetTag))
        {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            onExit.Invoke();
        }
    }
}