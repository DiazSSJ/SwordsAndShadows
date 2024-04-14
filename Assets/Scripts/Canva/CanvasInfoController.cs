using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInfoController : MonoBehaviour

{

    private static CanvasInfoController instance;

    public Canvas CanvasInicio;
    public Canvas CanvasInfo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HideInfo()
    {
        Debug.Log("Inicio");
        CanvasInfo.gameObject.SetActive(false);
        CanvasInicio.gameObject.SetActive(true);

    }
    public static CanvasInfoController GetInstance()
    {
        return instance == null ? instance = new CanvasInfoController() : instance;
    }

}
