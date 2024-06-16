using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{

    public static ControladorSonido instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener esta instancia al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir instancias duplicadas
            Debug.LogError("Multiple instances of ControladorSonido singleton detected!");
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }
}
