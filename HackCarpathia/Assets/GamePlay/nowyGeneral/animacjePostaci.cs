using UnityEngine;

public class animacjePostaci : MonoBehaviour
{
    public static animacjePostaci Instance;
    private Animator animator;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        animator = GetComponentInChildren<Animator>();
    }

    // Ustawiamy chodzenie
    public void SetWalking(bool status)
    {
        // U¿ywamy "Walking" z du¿ej litery, tak jak na Twoim screenie
        animator.SetBool("Walking", status);
    }

    // Ustawiamy trzymanie (Holding)
    public void SetHolding(bool status)
    {
        animator.SetBool("Holding", status);
    }

    // Aktywujemy podnoszenie
    public void TriggerPickingUp()
    {
        animator.SetTrigger("PickingUp");
    }
}
