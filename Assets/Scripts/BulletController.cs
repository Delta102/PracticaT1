using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BulletController : MonoBehaviour
{
    float velocity=15;
    Rigidbody2D rb;
    float realVelocity;
    private GameManager_Escena2 gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        gameManager=FindObjectOfType<GameManager_Escena2>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity=new Vector2(realVelocity,0);
        Destroy(this.gameObject, 5); //Después de 5 segundos y si el objeto no colisiona con nada el objeto desaparece para evitar consumo de memoria
    }

    public void SetRightDirection(){
        realVelocity=velocity;
    }

    public void SetLeftDirection(){
        realVelocity=-velocity;
    }

    /*public void SetScoreText(Text scoreText){
        this.scoreTexT=scoreTexT;
    }*/

    private void OnCollisionEnter2D (Collision2D other){
        Destroy(this.gameObject);
        if(other.gameObject.tag=="Enemy"){
            Destroy(other.gameObject);
            gameManager.ganarPuntaje(10);
        }
    }
}