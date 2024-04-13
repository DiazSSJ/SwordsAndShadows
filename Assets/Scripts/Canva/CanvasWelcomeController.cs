using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaInicio : MonoBehaviour
{

    public GameObject canvasObject;
    public GameObject canvasInfo;
    public GameObject canvasGame;

    void Awake()
    {

    }

    void Start()
    {
        canvasInfo.SetActive(false);
        canvasGame.SetActive(false);
    }

    void Update()
    {

    }

    public void HideCanvas()
    {
        if (canvasObject != null)
            canvasObject.SetActive(false);
        canvasGame.SetActive(true);
    }
    public void ShowInfo()
    {
        if (canvasInfo != null)
        {
            canvasInfo.SetActive(true);
            canvasObject.SetActive(false);
        }

    }


}
