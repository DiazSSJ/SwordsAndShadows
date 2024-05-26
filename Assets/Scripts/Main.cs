using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main instance;

    private bool isStarted = false;
    public Transform warrior;
    private Animator walkWarrior;
    public Light spotLight;
    public float moveSpeed = 5f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    private float rotationX = 0.0f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;
    public Vector3 cameraOffset = new Vector3(0, 8.580748f, -30.19307f); // Offset para la cámara en tercera persona
    public float followSpeed = 10f; // Velocidad de seguimiento de la cámara
    public float cameraSpeed = 5f;

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
        walkWarrior = warrior.GetComponent<Animator>();
        walkWarrior.enabled = false;

        // Posicionar inicialmente la cámara en la posición correcta
        Vector3 initialCameraPosition = warrior.position + cameraOffset;
        transform.position = initialCameraPosition;
    }

    void FixedUpdate()
    {
        MoveWarrior();
    }

    void Update()
    {
        MoveSpotLight();
        FollowWarrior();
        RotateCamera();
    }

    void MoveWarrior()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        Vector3 newPosition = warriorRigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        
        // Mantener la posición Y constante
        newPosition.y = warriorRigidbody.position.y;

        // Mover al guerrero usando la física
        warriorRigidbody.MovePosition(newPosition);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Activar la animación de caminar
            walkWarrior.enabled = true;
        }
        else
        {
            // Desactivar la animación de caminar
            walkWarrior.enabled = false;
        }
    }

    void FollowWarrior()
    {
        // Calcular la posición objetivo de la cámara en tercera persona
        Vector3 targetPosition = warrior.position + cameraOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
        // Rotación de la cámara con el mouse
        rotationX += Input.GetAxis("Mouse X") * horizontalSpeed;
        rotationX = Mathf.Clamp(rotationX, leftLimit, rightLimit); // Limitar la rotación en el eje X

        // Aplicar la rotación a la cámara
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
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
