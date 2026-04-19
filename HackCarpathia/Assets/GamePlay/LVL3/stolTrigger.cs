using UnityEngine;

public class stolTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject telefonPulpit;
    private bool telefon = false;
    private CharacterController cc;
    private bool wObsz = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = true;
            popupMessage.triggerMessage("Wciœnij E, aby usi¹œæ przy stole.");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wObsz = false;
            popupMessage.triggerMessage("Zjedz jedzenie przy stole.");
        }
    }

    private void Update()
    {
        if (telefon && Input.GetKeyDown(KeyCode.E))
        {
            popupMessage.triggerMessage("Uruchom aplikacje TikGram.");
            telefonPulpit.SetActive(true);
        }

        if (wObsz && Input.GetKeyDown(KeyCode.E))
        {
            wObsz = false;
            this.GetComponent<BoxCollider>().enabled = false;
            popupMessage.triggerMessage("Jajco");
            CameraControler.Instance.setCamera("KameraTable");
            player.SetActive(false);
            player2.SetActive(true);
            popupMessage.triggerMessage("Wyci¹gnij telefon klikaj¹c E, aby zabiæ czas.");
            telefon = true;
        }
        
    }
}
