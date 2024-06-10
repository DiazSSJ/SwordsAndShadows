using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main instance;

    private bool isStarted = false;
    public Transform warrior;
    private Animator warriorAnimator;
    public Light spotLight;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f; // Fuerza del salto
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    private float rotationX = 0.0f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;
    public Vector3 cameraOffset = new Vector3(0, 8.580748f, -30.19307f); // Offset para la cámara en tercera persona
    public float followSpeed = 8f; // Velocidad de seguimiento de la cámara
    public float cameraSpeed = 5f;
    public float lateralMoveDistance = 1.0f; // Distancia de movimiento lateral

    private Rigidbody warriorRigidbody;

    void Awake()
    {
        instance = this;
        warriorRigidbody = warrior.GetComponent<Rigidbody>();

        // Si el guerrero no tiene un Rigidbody, añadir uno
        if (warriorRigidbody == null)
        {
            warriorRigidbody = warrior.gameObject.AddComponent<Rigidbody>();
        }

        // Activar la gravedad para el guerrero
        warriorRigidbody.useGravity = true;

        // Hacer que el Rigidbody no sea cinemático para usar la física
        warriorRigidbody.isKinematic = false;
    }

    void Start()
    {
        warriorAnimator = warrior.GetComponent<Animator>();

        // Posicionar inicialmente la cámara en la posición correcta
        Vector3 initialCameraPosition = warrior.position + cameraOffset;
        transform.position = initialCameraPosition;
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        MoveSpotLight();
        FollowWarrior();
        MoveWarrior();
        // RotateCamera();
        CheckLateralMovement();
    }

    void MoveWarrior()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftControl); // Cambiado de LeftShift a LeftControl
        bool walkingPressed = Input.GetKey("w");

        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        moveDirection = transform.TransformDirection(moveDirection);
        Vector3 newPosition = warriorRigidbody.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

        newPosition.y = warriorRigidbody.position.y;

        // Mover al guerrero usando la física
        warriorRigidbody.MovePosition(newPosition);

        // Calcular la velocidad para el parámetro Speed
        float speed = moveDirection.magnitude * currentSpeed;
        warriorAnimator.SetFloat("Speed", speed);

        // Mantener la posición Y constante
        newPosition.y = warriorRigidbody.position.y;

        // Mover al guerrero usando la física
        warriorRigidbody.MovePosition(newPosition);

        if (walkingPressed)
        {
            warriorAnimator.SetBool("IsWalking", true);
            warriorAnimator.SetBool("IsRunning", false);
            warriorAnimator.SetBool("IsIdle", false);
        }
        if (isRunning && walkingPressed)
        {
            warriorAnimator.SetBool("IsRunning", true);
            warriorAnimator.SetBool("IsWalking", false);
            warriorAnimator.SetBool("IsIdle", false);

        }
        if (!isRunning && !walkingPressed && speed <= 0)
        {
            warriorAnimator.SetBool("IsIdle", true);
            warriorAnimator.SetBool("IsWalking", false);
            warriorAnimator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(warriorRigidbody.velocity.y) < 0.001f)
        {
            // Activar la animación de salto
            warriorAnimator.SetTrigger("Jump");
            // Aplicar fuerza hacia arriba para el salto
            warriorRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            warriorAnimator.SetBool("IsIdle", false);
        }
    }

    void CheckLateralMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 leftMove = new Vector3(-lateralMoveDistance, 0, 0);
            warrior.position += leftMove;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 rightMove = new Vector3(lateralMoveDistance, 0, 0);
            warrior.position += rightMove;
        }
    }

    void FollowWarrior()
    {
        // Calcular la posición objetivo de la cámara en tercera persona
        Vector3 targetPosition = warrior.position + cameraOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    // void RotateCamera()
    // {
    //     // Rotación de la cámara con el mouse
    //     rotationX += Input.GetAxis("Mouse X") * horizontalSpeed;
    //     rotationX = Mathf.Clamp(rotationX, leftLimit, rightLimit); // Limitar la rotación en el eje X

    //     // Aplicar la rotación a la cámara
    //     transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
    // }

    void MoveSpotLight()
    {
        // Mover la posición de la luz para que siga al guerrero
        Vector3 warriorPosition = warrior.position;
        Vector3 lightPosition = new Vector3(warriorPosition.x, spotLight.transform.position.y, warriorPosition.z);
        spotLight.transform.position = lightPosition;
    }

    // void CheckJump()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(warriorRigidbody.velocity.y) < 0.001f)
    //     {
    //         // Activar la animación de salto
    //         warriorAnimator.SetTrigger("Jump");
    //         // Aplicar fuerza hacia arriba para el salto
    //         warriorRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     }
    // }

    public void SetIsStarted(bool isStarted)
    {
        this.isStarted = isStarted;
    }

    public static Main GetInstance()
    {
        return instance == null ? instance = new Main() : instance;
    }
}