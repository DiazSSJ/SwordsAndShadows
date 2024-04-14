using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update

    // float totalTime = 0f;

    private static Main instance;

    private bool isStarted = false;
    public Transform orc;

    public float Timerun;
    // private Camera camera;
    public Light SpotLight;
    // public Transform RedMen01;
    // public Transform RedMen02;
    // private float movementDirection = 1f;
    // private float leftLimit = -2f;
    // private float rightLimit = 2f;

    public float cameraSpeed = 5f; // Velocidad de movimiento de la cámara
    public float horizontalSpeed = 2.0f; // Velocidad de rotación horizontal de la cámara
    public float verticalSpeed = 2.0f; // Velocidad de rotación vertical de la cámara
    private float rotationX = 0.0f;
    // private float rotationY = 0.0f;

    public float leftLimit = -2f;
    public float rightLimit = 2f;
    private float movementDirection = 1f;


    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void Start()
    {

    }

    void Update()
    {

        if (SpotLight != null)
        {
            SpotLight.color = Color.red;
        }


        Vector3 position = orc.position;
        position.x += Time.deltaTime * 20.0f * movementDirection;
        orc.position = position;

        if (position.x >= rightLimit || position.x <= leftLimit)
        {
            movementDirection *= -1f;
        }


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



    void OnDisable()
    {
        Debug.Log("Disable");
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
