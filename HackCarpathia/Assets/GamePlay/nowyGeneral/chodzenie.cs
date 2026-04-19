using UnityEngine;

public class chodzenie : MonoBehaviour
{
    [Header("Ustawienia Ruchu")]
    public float speed = 5f;
    public float turnSpeed = 10f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Transform camTransform;
    private float verticalVelocity;

    void Start()
    {
        CameraControler.Instance.setCamera("KameraPokoj");
        controller = GetComponent<CharacterController>();

        // Pobieramy kamerê - upewnij siê, ¿e Main Camera ma tag "MainCamera"!
        if (Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Nie znaleziono Main Camera! SprawdŸ tagi w Inspektorze.");
        }
    }

    void Update()
    {
        if (!controller.enabled) { 
            return;
        }
        // 1. Pobieranie Inputu (Strza³ki / WSAD)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(h, 0, v).normalized;

        Vector3 moveDir = Vector3.zero;

        // 2. Obliczanie kierunku ruchu wzglêdem kamery
        if (moveInput.magnitude >= 0.1f)
        {
            Vector3 camForward = camTransform.forward;
            Vector3 camRight = camTransform.right;

            camForward.y = 0; // Zerujemy Y, ¿eby nie biegaæ w górê/dó³
            camRight.y = 0;

            moveDir = (camForward.normalized * v + camRight.normalized * h).normalized;

            // 3. P³ynny Obrót
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // 4. Obs³uga Grawitacji
        if (controller.isGrounded && verticalVelocity < 0)
        {
            // Lekki docisk do ziemi, gdy postaæ stoi
            verticalVelocity = -2f;
        }
        else
        {
            // Spadanie
            verticalVelocity += gravity * Time.deltaTime;
        }

        // 5. Finalny Ruch (Poziomy + Pionowy)
        Vector3 finalVelocity = moveDir * speed;
        finalVelocity.y = verticalVelocity;

        controller.Move(finalVelocity * Time.deltaTime);
    }
}
