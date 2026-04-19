using UnityEngine;

public class mikrofalaTriger : MonoBehaviour
{
    private bool wObsz = false;
    [SerializeField] private GameObject stol;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = true;
            popupMessage.triggerMessage("Wciœnij E, aby podgrzaæ jedzenie w mikrofali.");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = false;
            popupMessage.triggerMessage("Podgrzej jedzenie w mikrofali.");
        }
    }

    private void Update()
    {
        if (wObsz && Input.GetKeyDown(KeyCode.E))
        {
            wObsz = false;
            this.gameObject.SetActive(false);
            popupMessage.triggerMessage("Zjedz jedzenie przy stole.");
            stol.gameObject.SetActive(true);
        }
    }
}
