using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Explorer : MonoBehaviour
{
    //Componentes del Player
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    public AudioClip bulletAudio;

    public GameObject bullet;

    private GameManager_Escena2 gameManager;

    //Variables Animaciones
    int pQuieto=0;
    int pCaminar=1;

    //Variables velocidades;
    int cVelocity=5;
    int corVelocity=10;
    int salVelocity=10;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        gameManager=FindObjectOfType<GameManager_Escena2>();
        animator=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
        cl=GetComponent<Collider2D>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Renderizado();
        Salto();
        CheckGround();
        CambiarAnimacion(pQuieto);
        rb.velocity=new Vector2(0,rb.velocity.y);

        // <<!!!Caminar¡¡¡>> //
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity= new Vector2(cVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity=new Vector2(-cVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        //<<!!!Correr¡¡¡>>//
        if(Input.GetKey(KeyCode.RightArrow)&& Input.GetKey(KeyCode.LeftShift)){
            rb.velocity= new Vector2(corVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        if(Input.GetKey(KeyCode.LeftArrow)&& Input.GetKey(KeyCode.LeftShift)){
            rb.velocity=new Vector2(-corVelocity, rb.velocity.y);
            CambiarAnimacion(pCaminar);
        }

        //<<!!!Disparo¡¡¡<<//
        if(Input.GetKeyUp(KeyCode.X)){
            //Se crea la bala de acuerdo a la posicion del player
            if(sr.flipX==false){
                disparo(3);
                
            }
            if(sr.flipX==true){
                disparo(-3);
            }
            audioSource.PlayOneShot(bulletAudio);
        }

    }

    //Renderizado Izquierda-Derecha

    private void Renderizado(){
        if(rb.velocity.x<0)
            sr.flipX=true;

        if(rb.velocity.x>0)
            sr.flipX=false;
    }

    private void Salto(){
        animator.SetFloat("jumpVelocity", rb.velocity.y);//jumpVelocity es el nombre del bool del animator (estado
        //Saltar
        if(!cl.IsTouchingLayers(LayerMask.GetMask("Suelo"))){
            Debug.Log("Está en el piso");
            return;
        }
        //si se ejecuta este if es porque es falso(esta en el piso) y saldra del metodo saltar 
        
        if (Input.GetKeyDown(KeyCode.Space)){//SALTO
            rb.velocity = new Vector2(rb.velocity.x, salVelocity);
            Debug.Log("Al menos entra al método con la tecla");
        }
    }

    private void disparo(int a){
        if(a>0){
            var bulletPosition=transform.position+new Vector3(a, 0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        if(a<0){
            var bulletPosition=transform.position+new Vector3(a, 0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
    }

    private void CheckGround()
    {
        if(cl.IsTouchingLayers(LayerMask.GetMask("Suelo")))
        {
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
        }
    }


    private void CambiarAnimacion(int estado){
        animator.SetInteger("Estado",estado);
    }

    private void OnCollisionEnter2D (Collision2D other){
        if(other.gameObject.tag=="MonedaTipo1"){
            Destroy(other.gameObject);
            gameManager.ganarMonedaTipo1(10);
        }
        if(other.gameObject.tag=="MonedaTipo2"){
            Destroy(other.gameObject);
            gameManager.ganarMonedaTipo2(20);
        }
        if(other.gameObject.tag=="CheckPoint"){
            gameManager.SaveGame();
            Debug.Log("CheckPoint");
        }
    }
}