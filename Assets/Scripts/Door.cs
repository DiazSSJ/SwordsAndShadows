using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public static Door instance;
    public GameObject puerta1;
    public GameObject puerta2;
    public GameObject puerta3;
    public GameObject puerta4;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI numberGemas;

    private static int cantidadGemas = 0;
    private static int totalGemasAcumuladas = 0;

    private bool resettingGemas = false;

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

    void Start()
    {
        UpdateLevelText();
        UpdateGemasText();
    }

    public static void IncrementarGemas()
    {
        cantidadGemas += 1;
        totalGemasAcumuladas += 1;
        instance.BorrarPuerta();
        instance.UpdateGemasText();
    }

    public void BorrarPuerta()
    {
        if (cantidadGemas == 4)
        {
            Destroy(puerta1);
            StartCoroutine(ResetLevelAfterDelay(4f));
            StartCoroutine(ResetGemasAfterDelay(4f));
        }
        if (cantidadGemas == 8)
        {
            Destroy(puerta2);
            StartCoroutine(ResetLevelAfterDelay(4f));
            StartCoroutine(ResetGemasAfterDelay(4f));
        }
        if (cantidadGemas == 12)
        {
            Destroy(puerta3);
            StartCoroutine(ResetLevelAfterDelay(4f));
            StartCoroutine(ResetGemasAfterDelay(4f));
        }
        if (cantidadGemas == 16)
        {
            Destroy(puerta4);
            levelText.text = "Juego terminado";
        }
    }

    private IEnumerator ResetGemasAfterDelay(float delay)
    {
        if (!resettingGemas)
        {
            resettingGemas = true;
            yield return new WaitForSeconds(delay);
            ResetTotalGemas();
            resettingGemas = false;
        }
    }

    private IEnumerator ResetLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (cantidadGemas == 4)
        {
            levelText.text = "Nivel 2";
        }
        else if (cantidadGemas == 8)
        {
            levelText.text = "Nivel 3";
        }
        else if (cantidadGemas == 12)
        {
            levelText.text = "Nivel 4";
        }
    }

    private void ResetTotalGemas()
    {
        totalGemasAcumuladas = 0;
        UpdateGemasText();
    }

    private void UpdateGemasText()
    {
        numberGemas.text = totalGemasAcumuladas.ToString();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Nivel 1";
    }
}