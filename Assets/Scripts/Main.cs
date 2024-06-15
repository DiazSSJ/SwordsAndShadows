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
    public float horizontalSpeed = 20.0f;
    public float rotationSpeed = 100f; // Velocidad de rotación
    public float verticalSpeed = 2.0f;
    private float rotationX = 0.0f;
    public Vector3 cameraOffset = new Vector3(0, 8.580748f, -40.19307f); // Offset para la cámara en tercera persona
    public float followSpeed = 8f; // Velocidad de seguimiento de la cámara
    public float cameraSpeed = 5f;
    public float lateralMoveDistance = 1.0f; // Distancia de movimiento lateral
    public float leftLimit = -45f; // Límite izquierdo de rotación en grados
    public float rightLimit = 45f; // Límite derecho de rotación en grados

    private Rigidbody warriorRigidbody;
    private float currentRotationY = 0f;

    public AudioClip baileSonido;

    public Collider handCollider;

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
        handCollider.enabled = false;
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        MoveSpotLight();
        FollowWarrior();
        MoveWarrior();
        //RotateWarrior();
        CheckLateralMovement();
    }

    void MoveWarrior()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        bool walkingPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"); // Activar con "w", "a", "s" o "d"

        float currentSpeed = moveSpeed;
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
            warriorAnimator.SetBool("IsIdle", false);
        }
        if (!walkingPressed && speed <= 0)
        {
            warriorAnimator.SetBool("IsIdle", true);
            warriorAnimator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(warriorRigidbody.velocity.y) < 0.001f)
        {
            // Activar la animación de salto
            warriorAnimator.SetTrigger("Jump");
            // Aplicar fuerza hacia arriba para el salto
            warriorRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            warriorAnimator.SetBool("IsIdle", false);
        }

        if (Input.GetMouseButtonDown(1)) // Detectar clic izquierdo
        {
            // Activar la animación de ataque
            handCollider.enabled = true;
            warriorAnimator.SetTrigger("attack");
            warriorAnimator.SetBool("IsIdle", false);
            warriorAnimator.SetBool("IsWalking", false);

        }


        if (Input.GetKey("f"))
        {
            ControladorSonido.instance.EjecutarSonido(baileSonido);
            // Activar la animación de twerk
            warriorAnimator.SetTrigger("dancing");
            warriorAnimator.SetBool("IsIdle", false);
            warriorAnimator.SetBool("IsWalking", false);
        }

        // Rotación del guerrero con límites
        if (Input.GetKey(KeyCode.A))
        {
            currentRotationY -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentRotationY += rotationSpeed * Time.deltaTime;
        }

        // Limitar la rotación
        currentRotationY = Mathf.Clamp(currentRotationY, leftLimit, rightLimit);

        // Aplicar la rotación limitada al guerrero
        warrior.rotation = Quaternion.Euler(0, currentRotationY, 0);
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

        // Sincronizar la rotación de la cámara con la del guerrero
        Quaternion targetRotation = Quaternion.Euler(0, currentRotationY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
    }


    void MoveSpotLight()
    {
        // Mover la posición de la luz para que siga al guerrero
        Vector3 warriorPosition = warrior.position;
        Vector3 lightPosition = new Vector3(warriorPosition.x, spotLight.transform.position.y, warriorPosition.z);
        spotLight.transform.position = lightPosition;
    }

    public void SetIsStarted(bool isStarted)
    {
        this.isStarted = isStarted;
    }

    public static Main GetInstance()
    {
        return instance == null ? instance = new Main() : instance;
    }
}