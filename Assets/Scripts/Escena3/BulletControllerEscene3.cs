using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerEscene3 : MonoBehaviour
{
    Collider2D cl;
    int vida=3;
    float velocity=15;
    int temp=0;
    Rigidbody2D rb;
    float realVelocity;
    private GameManagerEscena3 gameManager;
    private ZombieController zombieController;
    // Start is called before the first frame update
    void Start()
    {
        cl=GetComponent<Collider2D>();
        rb=GetComponent<Rigidbody2D>();
        gameManager=FindObjectOfType<GameManagerEscena3>();
        zombieController=FindObjectOfType<ZombieController>();
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
    public void OnCollisionEnter2D(Collision2D other){
        Destroy(this.gameObject);
    }
}
