using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spr;
    Rigidbody2D rgb;
    Animator animator;
    //Variables para animaciones
    int aQuieto=0;
    int aCaminar=1;
    int aCorrer=2;
    int aJump=3;
    int aAtaque=4;
    //Variables para funciones
    int jumpForce=15;
    int velocityCaminar=7;
    int velocityCorrer=12;
    void Start()
    {
        spr=GetComponent<SpriteRenderer>();
        rgb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CambiarAnimacion(aQuieto);
        rgb.velocity=new Vector2(0,rgb.velocity.y);
        //Animación-Caminar
        if(Input.GetKey(KeyCode.LeftArrow)){
            rgb.velocity=new Vector2(-velocityCaminar,rgb.velocity.y);
            spr.flipX=true;
            CambiarAnimacion(aCaminar);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
            rgb.velocity=new Vector2(-1,rgb.velocity.y);
        if(Input.GetKey(KeyCode.RightArrow)){
            rgb.velocity=new Vector2(velocityCaminar,rgb.velocity.y);
            spr.flipX=false;
            CambiarAnimacion(aCaminar);
        }
        if(Input.GetKeyUp(KeyCode.RightArrow))
            rgb.velocity=new Vector2(-1,rgb.velocity.y);

        //Animacion-Correr
        if((Input.GetKey(KeyCode.LeftArrow))&&(Input.GetKey(KeyCode.X))){
            rgb.velocity=new Vector2(-velocityCorrer,rgb.velocity.y);
            spr.flipX=true;
            CambiarAnimacion(aCorrer);
        }
        if(Input.GetKey(KeyCode.RightArrow)&&Input.GetKey(KeyCode.X)){
            rgb.velocity=new Vector2(velocityCorrer,rgb.velocity.y);
            spr.flipX=false;
            CambiarAnimacion(aCorrer);
        }

        //Animacion-Salto
        if(Input.GetKeyUp(KeyCode.Space)){
            rgb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            CambiarAnimacion(aJump);
        }

        //Animacion ataque
        if(Input.GetKeyUp(KeyCode.Z)){
            CambiarAnimacion(aAtaque);
        }
    }

    void CambiarAnimacion(int animation){
        animator.SetInteger("Estado",animation);
    }
}
