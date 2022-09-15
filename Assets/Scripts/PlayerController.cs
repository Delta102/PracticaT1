using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spr;
    Rigidbody2D rgb;
    Animator animator;
    Vector3 lastCheckpointPosition;
    Vector3 lastCheckpointPosition2;

    public GameObject bullet;
    public GameManagerController gameManager;

    //Variables para animaciones
    /*int aQuieto=0;
    int aCaminar=1;
    int aCorrer=2;*/
    int aJump=3;
    int aAtaque=4;
    //Variables para funciones
    int jumpForce=45;
    /*int velocityCaminar=5;
    int velocityCorrer=12;*/
    void Start()
    {
        spr=GetComponent<SpriteRenderer>();
        rgb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
         rgb.velocity=new Vector2(5,0);

        if(Input.GetKeyUp(KeyCode.F) && gameManager.Cantidad()>0){
            //Se crea la bala de acuerdo a la posicion del player
            if(spr.flipX==false){
                disparo(3);
                gameManager.PerderBalas();
            }
                
            if(spr.flipX==true){
                disparo(-3);
                gameManager.PerderBalas();
            }
        }


        //Animación-Caminar
        /*if(Input.GetKey(KeyCode.LeftArrow)){
            rgb.velocity=new Vector2(-velocityCaminar,rgb.velocity.y);
            spr.flipX=true;
            CambiarAnimacion(aCaminar);
        }
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
            rgb.velocity=new Vector2(-1,rgb.velocity.y);

        if(Input.GetKey(KeyCode.RightArrow)){
            rgb.velocity=new Vector2(velocityCaminar,rgb.velocity.y);
            spr.flipX=false;
            CambiarAnimacion(aCaminar);
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
            rgb.velocity=new Vector2(-1,rgb.velocity.y);

        //Animacion-Correr
        if((Input.GetKey(KeyCode.LeftArrow))&&(Input.GetKey(KeyCode.X))){
            rgb.velocity=new Vector2(-velocityCorrer,rgb.velocity.y);
            spr.flipX=true;
            CambiarAnimacion(aCorrer);
        }
        else if(Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.X)){
            rgb.velocity=new Vector2(velocityCorrer,rgb.velocity.y);
            spr.flipX=false;
            CambiarAnimacion(aCorrer);
        }*/

        //Animacion-Salto
        if(Input.GetKeyUp(KeyCode.Space)){
            rgb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            CambiarAnimacion(aJump);
        }
         
        if(Input.GetKeyDown(KeyCode.Z)){
            CambiarAnimacion(aAtaque);
        }

        //Animacion ataque
        
    }

    void disparo(int a){
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

    void CambiarAnimacion(int animation){
        animator.SetInteger("Estado",animation);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag =="DarkHole")//para colisionar con el piso de fondo
        {
            if(lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
            if(lastCheckpointPosition2 !=null){
                transform.position=lastCheckpointPosition2;
            }
        }

        if(other.gameObject.tag=="Enemy"){
            Debug.Log("f");        
        }

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="CheckPoint"){
            lastCheckpointPosition = transform.position;
        }
        /*else if(other.gameObject.tag=="CheckPoint"){
            lastCheckpointPosition2=transform.position;
        }*/
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Collider1"){
            lastCheckpointPosition = transform.position;
        }
        else if(other.gameObject.name=="Collider2"){
            lastCheckpointPosition2=transform.position;
        }
    }
}