using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerEscene3 : MonoBehaviour
{
    int vida=3;
    float velocity=15;
    int temp=0;
    Rigidbody2D rb;
    float realVelocity;
    private GameManagerEscena3 gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        gameManager=FindObjectOfType<GameManagerEscena3>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity=new Vector2(realVelocity,0);
        Destroy(this.gameObject, 5); //Después de 5 segundos y si el objeto no colisiona con nada el objeto desaparece para evitar consumo de memoria
    }

    public void SetDirection(bool temp){
        if(temp==true)
            realVelocity=velocity;
        else if(temp==false)
            realVelocity-=velocity;
    }

    public void SetTipoBala(int b){
        temp=b;
    }

    private void OnCollisionEnter2D (Collision2D other){
        Destroy(this.gameObject);
            //gameManager.ganarPuntaje(10);
        Debug.Log("Temp=" +temp);
        if(temp==1)
            gameManager.PerderVida(1);
        if(temp==2)
            gameManager.PerderVida(2);
        if(temp==3)
            gameManager.PerderVida(3);
        if(gameManager.vida_Zombie<=0)
            Destroy(other.gameObject);
    }
}
