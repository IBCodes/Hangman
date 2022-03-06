using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    // inisalising the x rotation around the y axis so our player can look up and down    
    float xRotation = 0f;
    public Vector2 turn;
    //the speed of the mouse if i have time i will set up a settings menu so the player can change this
    public float sensitivity = 50;
    //insisalizing the body in order rotate with the mose x axis control
    public Vector3 deltaMove;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
         //taken from the pre programed varable 
        turn.x += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        turn.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //Rotation was flipped when i tried to do +=
        xRotation -= turn.y;
        //this is to stop the player from being able to look all the way around and will clamp them between straignt up and straight down
        //this is also the reason i used a quaternion eular instead of how i done it with the x axis 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, turn.x, 0);
    }
}
