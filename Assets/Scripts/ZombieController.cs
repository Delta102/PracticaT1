using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida=3;
    Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public int Vida(){
        return vida;
    }

    void PerderVida(int a){
        vida-=a;
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity=new Vector2(-10, rb.velocity.y);
    }
    

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="bala")
            PerderVida(1);
        if(other.gameObject.tag=="bala2")
            PerderVida(2);
        if(other.gameObject.tag=="bala3")
            PerderVida(3);
        
        if(vida<=0)
            Destroy(this.gameObject);
    }
}
