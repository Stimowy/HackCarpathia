using UnityEngine;

public class LodowkaTrigger : MonoBehaviour
{
    private bool wObsz = false;
    [SerializeField] private GameObject mikrofala;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = true;
            popupMessage.triggerMessage("Wciœnij E, aby wyci¹gn¹æ jedzenie z lodówki.");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = false;
            popupMessage.triggerMessage("IdŸ do kuchni coœ zjeœæ.");
        }
    }

    private void Update()
    {
        if (wObsz && Input.GetKeyDown(KeyCode.E))
        {
            wObsz = false;
            this.gameObject.SetActive(false);
            popupMessage.triggerMessage("Podgrzej jedzenie w mikrofali.");
            mikrofala.gameObject.SetActive(true);
        }
    }
}
