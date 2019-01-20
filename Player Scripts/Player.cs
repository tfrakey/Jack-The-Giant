using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public variables
    public float speed = 8.0f;
    public float maxVelocity = 4.0f;

    //Private variables
    private Rigidbody2D myBody;
    private Animator anim;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Is called every few frames
    void FixedUpdate()
    {
        PlayerMoveKeyboard();
    }

    void PlayerMoveKeyboard()
    {
        float forceX = 0;
        float vel = Mathf.Abs(myBody.velocity.x); //Returns an absolute value which is always positive
        float h = Input.GetAxisRaw("Horizontal"); //Returns -1 for left, 0 for nothing, and 1 for right
        Vector3 temp = transform.localScale;

        if(h>0)
        {
            if (vel < maxVelocity)
                forceX = speed;

            anim.SetBool("Walk", true);
            temp.x = 1.3f;
            transform.localScale = temp; //Player faces right
        }
        else if (h< 0)
        {
            if (vel < maxVelocity)
                forceX = -speed;

            anim.SetBool("Walk", true);
            temp.x = -1.3f;
            transform.localScale = temp; // Player faces left
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        myBody.AddForce(new Vector2(forceX, 0)); 

    }
}
