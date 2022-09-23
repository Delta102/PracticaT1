using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerEscena3 : MonoBehaviour
{
    public TMP_Text cantidadTexto;

    public int cantidad;
    public int vida_Zombie;

    void Start()
    {
        cantidad=5;
        vida_Zombie=3;
        //PrintCantidadInScreen();
    }

    public int Cantidad()
    {
        return cantidad;
    }

    public int Vida_Zombie()
    {
        return vida_Zombie;
    }

    public void PerderVida(int a)
    {
        vida_Zombie -= a;
        //PrintCantidadInScreen();
    }

    public int tipoBala(int b)
    {
        return b;
        //PrintCantidadInScreen();
    }

    public void PerderBalas()
    {
        cantidad -= 1;
        //PrintCantidadInScreen();
    }
}
