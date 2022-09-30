using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerEscena3 : MonoBehaviour
{
    public TMP_Text cantidadTexto;

    public GameObject zombie;
    public int cantidad;
    public int vida_Zombie;

    public float time = 0;
    public float seg = 0;

    void Start()
    {
        cantidad=5;
        vida_Zombie=3;
        GenerarZombie();
        //PrintCantidadInScreen();
    }

    public void GenerarZombie(){
        float time=0;
        time+=Time.deltaTime;
        seg = Mathf.Floor(time % 60);
        Debug.Log("Debug desde el game"+seg);
        if(seg==2){
            var bulletPosition = transform.position+ new Vector3(-5, 0, 0);
            var gb = Instantiate(zombie, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<ZombieController>();
        }
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
