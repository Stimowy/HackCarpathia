using Unity.VisualScripting;
using UnityEngine;

public class MoveTriger : MonoBehaviour
{
    [Header("przypisania objektów")]
    [SerializeField] private GameObject dom;
    [SerializeField] private GameObject szkola;

    [Header("pozycja gracza")]
    [SerializeField] private Vector3 pozGracza;
    [SerializeField] private Vector3 pozCam;
    [SerializeField] private Vector3 rotCam;

    [Header("przypisania do teleportacji")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;
    private Camera camera;

    private bool isPlayerInTrigger = false;

    private void Awake()
    {
        camera = cam.GetComponent<Camera>();
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.position = pozGracza;
                camera.transform.position = pozCam;
                camera.transform.rotation = Quaternion.Euler(rotCam);
                dom.SetActive(false);
                szkola.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        popupMessage.triggerMessage("naciśnij E, aby przejść do szkoły");
        isPlayerInTrigger=true;
    }

    private void OnTriggerExit(Collider other)
    {
        popupMessage.closeMessage();
        isPlayerInTrigger = false;
    }
}
