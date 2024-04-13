using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaInicio : MonoBehaviour
{

    public GameObject canvasObject;
    public GameObject canvasInfo;

    void Awake()
    {

    }

    void Start()
    {
        canvasInfo.SetActive(false);
    }

    void Update()
    {

    }

    public void HideCanvas()
    {
        if (canvasObject != null)
            canvasObject.SetActive(false);
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
