using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CanvaInicio : MonoBehaviour
{


    private static CanvaInicio instance;
    public Canvas CanvasInicio;
    public Canvas CanvasInfo;
    public Canvas CanvasGame;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void HideCanvas()
    {

        CanvasInicio.gameObject.SetActive(false);
        CanvasGame.gameObject.SetActive(true);
        Main.GetInstance().SetIsStarted(false);
    }

    public void ShowInfo()
    {
        CanvasInicio.gameObject.SetActive(false);
        CanvasInfo.gameObject.SetActive(true);
        Debug.Log("ANDO POR ACÁMK");

    }
    // public void HideInfo()
    // {
    //     Debug.Log("Inicio");
    //     CanvasInfo.gameObject.SetActive(false);
    //     CanvasInicio.gameObject.SetActive(true);

    // }

    public static CanvaInicio GetInstance()
    {
        return instance == null ? instance = new CanvaInicio() : instance;
    }


}
