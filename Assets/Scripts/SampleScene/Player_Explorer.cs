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
    //Vector3 lastPosition;
    public AudioClip bulletAudio;
    public float time = 0;
    public float seg = 0;

    public GameObject bullet;
    public GameObject bulletm;
    public GameObject bulletg;

    private GameManager_Escena2 gameManager;

    //Variables Animaciones
    int pQuieto = 0;
    int pCaminar = 1;

    //Variables velocidades;
    int cVelocity = 5;
    int corVelocity = 10;
    int salVelocity = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager_Escena2>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time = 0;
        seg = 0;
        Renderizado();
        Salto();
        CheckGround();
        CambiarAnimacion(pQuieto);
        rb.velocity = new Vector2(0, rb.velocity.y);

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
        if (Input.GetKeyUp(KeyCode.X))
        {
            //Se crea la bala de acuerdo a la posicion del player
            time += Time.time;
            seg = time % 60;
            seg = Mathf.Round(seg);

            Debug.Log("Tiempo Inicial: " + seg);
            //Bala Pequeña
            if (seg == 1)
            {
                if (sr.flipX == false)
                    disparo(3);
                if (sr.flipX == true)
                    disparo(-3);

                time = 0;
                seg = 0;
            }
            //Bala Mediana
            if (seg == 3)
            {
                Debug.Log("Tiempo: " + seg);
                if (sr.flipX == false)
                    disparo2(3);

                if (sr.flipX == true)
                    disparo2(-3);

                time = 0;
                seg = 0;
            }

            //Bala Grande
            if (seg == 5)
            {
                Debug.Log("Tiempo: " + seg);
                if (sr.flipX == false)
                    disparo3(3);

                if (sr.flipX == true)
                    disparo3(-3);

                time = 0;
                seg = 0;
            }
        }
    }

    //Renderizado Izquierda-Derecha

    private void Renderizado()
    {
        if (rb.velocity.x < 0)
            sr.flipX = true;

        if (rb.velocity.x > 0)
            sr.flipX = false;
    }

    private void Salto()
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

    private void disparo(int a)
    {
        if (a > 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        if (a < 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
    }
    private void disparo2(int a)
    {
        if (a > 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bulletm, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        if (a < 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bulletm, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
    }
    private void disparo3(int a)
    {
        if (a > 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bulletg, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        if (a < 0)
        {
            var bulletPosition = transform.position + new Vector3(a, 0, 0);
            var gb = Instantiate(bulletg, bulletPosition, Quaternion.identity) as GameObject;
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