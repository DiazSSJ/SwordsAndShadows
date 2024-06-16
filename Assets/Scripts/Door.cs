using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door instance;
    public GameObject puerta1;
    public GameObject puerta2;
    public GameObject puerta3;
    public GameObject puerta4;

    private static int cantidadGemas = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances of Door singleton detected!");
        }
    }

    public static void IncrementarGemas()
    {
        cantidadGemas += 1;
        instance.BorrarPuerta();
    }

    public void BorrarPuerta()
    {
        if (cantidadGemas == 4)
        {
            Destroy(puerta1);
        }
        if (cantidadGemas == 8)
        {
            Destroy(puerta2);
        }
        if (cantidadGemas == 12)
        {
            Destroy(puerta3);
        }
        if (cantidadGemas == 16)
        {
            Destroy(puerta4);
        }
    }
}
