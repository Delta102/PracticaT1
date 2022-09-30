using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NinjaScript : MonoBehaviour
{
SpriteRenderer sr;
    Rigidbody2D rb;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    //Vector3 lastPosition;
    public AudioClip bulletAudio;
    public float time = 0;
    public float seg = 0;

    private GameObject temp;
    public GameObject Kunai;
    public GameObject zombie;

    private GameManager_Escena2 gameManager;
    private GameManagerEscena3 gameManager2;

    //Variables Animaciones
    int pQuieto = 0;
    int pCaminar = 1;
    int pDisparo=2;

    //Variables velocidades;
    int cVelocity = 0;
    int rVelocity=8;
    int corVelocity = 10;
    int salVelocity = 10;

    [SerializeField]
    float velocityPlaneo;
    float gravity;
    //CAMBIO
    public SpriteRenderer srCharacter;
    public Sprite[] sprites;
    private int next = 1;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity=rb.gravityScale;
        gameManager = FindObjectOfType<GameManager_Escena2>();
        gameManager2 = FindObjectOfType<GameManagerEscena3>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int[] segundos= new int[2];
        time+=Time.deltaTime;
        seg = Mathf.Floor(time % 60);

        if(seg >=2 && seg<=4){
            var bulletPosition = new Vector3(6.325341f, -3.192008f,0);
            var gb = Instantiate(zombie, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<ZombieController>();
            if(zombie.transform.localScale.x==-1)
                zombie.transform.localScale=new Vector3(1,1,0);
            if(zombie.transform.localScale.x==1)
                zombie.transform.localScale=new Vector3(-1,1,0);
            time=0;
            seg=0;
        }
        //time = 0;
        //seg = 0;
        //disparoKunai();
        Renderizado();
        Caminar();
        Salto();
        CheckGround();
        /*CambiarAnimacion(pQuieto);
        rb.velocity = new Vector2(0, rb.velocity.y);*/

        // <<!!!Caminar¡¡¡>> //
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(cVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-cVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        //<<!!!Correr¡¡¡>>//
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(corVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(-corVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }
        //<<!!!Disparo¡¡¡<<//
        if(Input.GetKey(KeyCode.X)){
            sr.color=Color.blue;
            time+=Time.deltaTime;
            seg = Mathf.Floor(time % 60);
            CambiarAnimacion(pDisparo);
        }

        if(Input.GetKey(KeyCode.R) && rb.velocity.y<=0){
            rb.gravityScale=0;
            rb.velocity=new Vector2(rb.velocity.x, -velocityPlaneo);
        }
        else{
            rb.gravityScale=gravity;
        }

        Debug.Log("Tiempo Inicial: " + seg);
        if (Input.GetKeyUp(KeyCode.X))
        {
            //Bala Pequeña
            if (seg >= 0 && seg<2)
            {
                if (sr.flipX == false)
                    disparo(3, true, 1);
                if (sr.flipX == true)
                    disparo(-3, false, 1);
                time = 0;
                seg = 0;
            }
            //Bala Mediana
            if (seg >= 2 && seg<4)
            {
                if (sr.flipX == false)
                    disparo(3, true, 2);

                if (sr.flipX == true)
                    disparo(-3, false, 2);
                time = 0;
                seg = 0;
            }

            //Bala Grande
            if (seg >= 4)
            {
                if (sr.flipX == false)
                    disparo(3, true, 3);

                if (sr.flipX == true)
                    disparo(-3, false, 3);

                time = 0;
                seg = 0;
            }
        }
    }

    public void ChangeCharacter(){
        srCharacter.sprite = sprites[next];
        next++;
        if(next == sprites.Length )
            next = 0;
    }

    void Caminar(){
        rb.velocity=new Vector2(cVelocity, rb.velocity.y);
    }
    public void StopWalk(){
        cVelocity=0;
        CambiarAnimacion(pQuieto);
    }
    public void CaminarLeftRight(bool temp){
        CambiarAnimacion(pCaminar);
        if(temp)
            cVelocity=rVelocity;
        if(!temp)
            cVelocity=-rVelocity;
    }

    //Renderizado Izquierda-Derecha

    private void Renderizado()
    {
        if (rb.velocity.x < 0)
            sr.flipX = true;

        if (rb.velocity.x > 0)
            sr.flipX = false;
    }
    public void Salto()
    {
        animator.SetFloat("jumpVelocity", rb.velocity.y);//jumpVelocity es el nombre del bool del animator (estado
        //Saltar
        if (!cl.IsTouchingLayers(LayerMask.GetMask("Suelo")))
        {
            Debug.Log("Está en el piso");
            return;
        }
        //si se ejecuta este if es porque es falso(esta en el piso) y saldra del metodo saltar 
        if (Input.GetKeyDown(KeyCode.Space))
        {//SALTO
            rb.velocity = new Vector2(rb.velocity.x, salVelocity);
            Debug.Log("Al menos entra al método con la tecla");
        }
    }

    public void Salto2s()
    {
        animator.SetFloat("jumpVelocity", rb.velocity.y);//jumpVelocity es el nombre del bool del animator (estado
        //Saltar
        if (!cl.IsTouchingLayers(LayerMask.GetMask("Suelo")))
        {
            Debug.Log("Está en el piso");
            return;
        }
        //si se ejecuta este if es porque es falso(esta en el piso) y saldra del metodo saltar 
        rb.velocity = new Vector2(rb.velocity.x, salVelocity);
        /*if (Input.GetKeyDown(KeyCode.Space))
        {//SALTO
            rb.velocity = new Vector2(rb.velocity.x, salVelocity);
            Debug.Log("Al menos entra al método con la tecla");
        }*/
    }

    private void disparo(int a, bool t, int tipo)
    {/*
        if(tipo==1)
            temp=bullet;
        if(tipo==2)
            temp=bulletm;
        if(tipo==3)
            temp=bulletg;

            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(temp, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletControllerEscene3>();
            controller.SetDirection(t);
            controller.SetTipoBala(tipo);*/
    }
    public void disparoKunai()
    {
        if (sr.flipX == false){
            var bulletPosition = transform.position + new Vector3(5, 0, 0);
            var gb = Instantiate(Kunai, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        
        if (sr.flipX == true){
            var bulletPosition = transform.position + new Vector3(-5, 0, 0);
            var gb = Instantiate(Kunai, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
    }

    private void CheckGround()
    {
        if (cl.IsTouchingLayers(LayerMask.GetMask("Suelo")))
        {
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
        }
    }


    private void CambiarAnimacion(int estado)
    {
        animator.SetInteger("Estado", estado);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MonedaTipo1")
        {
            Destroy(other.gameObject);
            gameManager.ganarMonedaTipo1(10);
        }
        if (other.gameObject.tag == "MonedaTipo2")
        {
            Destroy(other.gameObject);
            gameManager.ganarMonedaTipo2(20);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //var asd=transform.position.x;
            //lastPosition=transform.position;
            //gameManager.guardarPosicion(lastPosition);
            gameManager.SaveGame();
            Debug.Log("CheckPoint asd");
        }
    }
}
