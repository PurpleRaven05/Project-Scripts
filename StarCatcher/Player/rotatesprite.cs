using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatesprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float rotationSpeed;
    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateSprite();
    }
    
    void RotateSprite(){
        if((rb.velocity.x <0 && rb.velocity.y >0)||(rb.velocity.x >0 && rb.velocity.y <0)){
            transform.Rotate(0f, 0f, -(rb.velocity.x-rb.velocity.y)/2*0.025f, Space.World);
        }
        else{
            transform.Rotate(0f, 0f, -(rb.velocity.x+rb.velocity.y)/2*0.025f, Space.World);
        }

    }
    
}
