using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CanvaInicio : MonoBehaviour
{


    private static CanvaInicio instance;
    public GameObject CanvasInicio;
    public GameObject CanvasInfo;
    public GameObject CanvasGame;

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
        if (CanvasInfo != null)
        {
            CanvasInfo.SetActive(true);
            CanvasInicio.SetActive(false);
        }

    }

    public static CanvaInicio GetInstance()
    {
        return instance == null ? instance = new CanvaInicio() : instance;
    }


}
