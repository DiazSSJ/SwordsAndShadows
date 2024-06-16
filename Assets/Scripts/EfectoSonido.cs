using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoGema;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ha tocado la gema.");
            ControladorSonido.instance.EjecutarSonido(sonidoGema);
            Destroy(gameObject);
            Door.IncrementarGemas();
        }

        Debug.Log("sonidooooooooo");
    }
}
