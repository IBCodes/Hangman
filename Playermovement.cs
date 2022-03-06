using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    public CharacterController controller;
    //inisalising the speed and gravity the speed
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;
     //checking for the ground 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // a check to see if the player is registered on the ground 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //slowing the player down when they are close to or on the ground
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        //taking th input from the already made unity keybord controls
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //making a vector called move tht is in the direction the player is facing and to the right of where the player is facing
        Vector3 move = transform.right * x + transform.forward * z;
        //making the controler move
        controller.Move(move * speed * Time.deltaTime);
        //gravity by the law changeofy = 1/2*gravity*time^2
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //in order to stop the player when in a conversation

    }

}
