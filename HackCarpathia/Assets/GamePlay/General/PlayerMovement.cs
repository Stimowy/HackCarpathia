using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // zmienna przechowuj¹ca kamere bêd¹ca na g³owie gracza
    private GameObject playerCamera;
    private Vector3 playerMovement;
    private Rigidbody rb;
    private float movementSpeed = 2.0f;
    [SerializeField] float playerWalkSpeed = 2.0f;
    [SerializeField] float playerRunSpeed = 4.0f;

    private void Awake()
    {
        // kamera musi mieæ nazwê playerCamera 
        playerCamera = GameObject.Find("playerCamera");
        rb = this.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        playerMovement = (transform.right * inputX + transform.forward * inputZ).normalized;
        // szybkoœæ chodzenia
        if (Input.GetKeyDown(KeyCode.LeftShift)) { 
            movementSpeed = playerRunSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("dupa");
            movementSpeed = playerWalkSpeed;
        }
        Debug.Log(movementSpeed);
    }
    private void FixedUpdate()
    {
        if (playerCamera.GetComponent<CinemachineCamera>().IsLive)
        {
            //zmiana obrotu postaci
            Transform playerTransform = this.transform;
            Transform cameraTransform = playerCamera.transform;
            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, cameraTransform.eulerAngles.y, playerTransform.eulerAngles.z);

            movePlayer();
        }
    }

    //skrypt zmieniaj¹cy pozycje gracza
    private void movePlayer()
    {
        rb.MovePosition(rb.position + playerMovement * movementSpeed * Time.deltaTime);
    }
}
