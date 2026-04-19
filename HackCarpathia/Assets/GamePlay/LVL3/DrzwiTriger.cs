using UnityEngine;

public class DrzwiTriger : MonoBehaviour
{
    [SerializeField] private GameObject gracz;
    [SerializeField] private Animator animatorDrzwi;
    CharacterController cc;
    private void Awake()
    {
        popupMessage.triggerMessage("IdŸ do kuchni coœ zjeœæ.");
        cc = gracz.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animatorDrzwi.SetBool("czyOtwarte", true);
            popupMessage.triggerMessage("Naciœnij E, aby przejœæ do kuchni.");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animatorDrzwi.SetBool("czyOtwarte", false);
            popupMessage.triggerMessage("IdŸ do kuchni coœ zjeœæ.");
        }
    }

    private void Update()
    {
  
        if (animatorDrzwi.GetBool("czyOtwarte") && Input.GetKeyDown(KeyCode.E))
        {
            animatorDrzwi.SetBool("czyOtwarte", false);
            cc.enabled = false; // Wy³¹czamy fizykê kontrolera
            gracz.transform.position = new Vector3(-27.71f, 2.33f, 34.59f); ; // Przenosimy
            cc.enabled = true; // W³¹czamy z powrotem
            CameraControler.Instance.setCamera("kameraKuchnia");
            popupMessage.triggerMessage("WeŸ jedzenie z lodówki.");
        }
    }
}
