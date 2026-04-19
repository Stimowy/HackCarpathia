using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTriger : MonoBehaviour
{
    [Header("przypisania objektów")]
    [SerializeField] private GameObject dom;
    [SerializeField] private GameObject szkola;

    [Header("pozycja gracza")]
    [SerializeField] private Vector3 pozGracz;
    [SerializeField] private Vector3 pozCam;
    [SerializeField] private Vector3 rotCam;

    [Header("przypisania do teleportacji")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;

    private bool isPlayerInTrigger = false;

    private void Start()
    {
        dom.SetActive(true);
        szkola.SetActive(false);
        popupMessage.timeMessage("wyjdź z domu, aby pójść do szkoły na ważny sprawdzian", 4f, 2f);
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cam.transform.position = pozCam;
                cam.transform.rotation = Quaternion.Euler(rotCam);

                player.transform.position = pozGracz;
                Physics.SyncTransforms();

                dom.SetActive(false);
                szkola.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        popupMessage.triggerMessage("naciśnij E, aby przejść do szkoły");
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        popupMessage.closeMessage();
        isPlayerInTrigger = false;
    }
}
