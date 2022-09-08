using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManagerController : MonoBehaviour
{
    public TMP_Text cantidadTexto;

    public int cantidad;

    void Start()
    {
        cantidad=5;
        PrintCantidadInScreen();
    }

    public int Cantidad()
    {
        return cantidad;
    }

    public void PerderBalas()
    {
        cantidad -= 1;
        PrintCantidadInScreen();
    }

    private void PrintCantidadInScreen()
    {
        cantidadTexto.text = "Cantidad: " + cantidad;
    }
}