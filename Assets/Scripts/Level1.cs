using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public GameObject warrior;
    public GameObject vida1;
    public GameObject vida2;
    public GameObject vida3;

    public int collisionCount = 0; // Variable para contar las colisiones
    public int collisionsPerLife = 5;

    void Start()
    {
        // Inicializa las vidas
        if (vida1 == null || vida2 == null || vida3 == null)
        {
            Debug.LogError("Asegúrate de asignar todas las vidas en el Inspector.");
        }
    }

    // Método que se llama cuando el colisionador de este objeto empieza a tocar otro colisionador
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que tocó tiene la etiqueta "Enemigo"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Incrementa el contador de colisiones
            collisionCount++;
            Debug.Log("el warrior ha tocado al enemigo" + collisionCount + "veces");

            // Desactiva las vidas en función del contador de colisiones
            if (collisionCount == 5)
            {
                vida1.SetActive(false);
            }
            else if (collisionCount == 10)
            {
                vida2.SetActive(false);
            }
            else if (collisionCount == 15)
            {
                vida3.SetActive(false);
                // Desactivar el Warrior si no quedan vidas
                warrior.SetActive(false);
                Debug.Log("El Warrior ha perdido todas sus vidas y ha sido desactivado.");
            }
        }
    }
}