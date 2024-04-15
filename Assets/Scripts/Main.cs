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
    public float cameraSpeed = 5f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    private float rotationX = 0.0f;
    public float leftLimit = -2f;
    public float rightLimit = 2f;






    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        walkWarrior = warrior.GetComponent<Animator>();
        walkWarrior.enabled = false;
    }

    void Update()
    {
        MoveWarrior();
        MoveSpotLight();
        RotateCamera();
    }

    void MoveWarrior()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        warrior.position += moveDirection * cameraSpeed * Time.deltaTime;


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

    void RotateCamera()
    {

        //Rotacion de la cámara con las teclas
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection);
        transform.position += moveDirection * cameraSpeed * Time.deltaTime;

        // Rotación de la cámara con el mouse
        rotationX += Input.GetAxis("Mouse X") * horizontalSpeed;
        // rotationY += Input.GetAxis("Mouse Y") * verticalSpeed;
        // rotationY = Mathf.Clamp(rotationY, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        // transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
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
