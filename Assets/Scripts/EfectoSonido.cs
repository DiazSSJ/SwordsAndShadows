using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{

    // private AudioSource audioSource;

    [SerializeField] private AudioClip sonidoGema;

    // private void Start()
    // {
    //     audioSource = GetComponent<AudioSource>();
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ha tocado la gema.");
            ControladorSonido.instance.EjecutarSonido(sonidoGema);
            Destroy(gameObject);
        }

        Debug.Log("sonidooooooooo");
    }
}
