//Author: Alyona Karmazin
//This script is designed for Google VR SDK and its Daydream controller.
//The script should be attached to the player. And it allows to use touchpad to move forward and backward and turn left or right. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class move_forward_onclick : MonoBehaviour
{
    public float speedForward = 4f; //speed to move forward
    public float speedBack = 2f;    //speed to move backward(better to keep a little slower than forward)
    public bool canRotate = true;
    
    // Update is called once per frame
    void Update()
    {
        if (GvrControllerInput.ClickButton) //detect click
        {
            if (GvrControllerInput.IsTouching && canRotate) //".IsTouching" needs to prevent taking last touchpad touching position
            {
                Vector2 touchPos = GvrControllerInput.TouchPos; 
    //center part of the daydream contreller Y axis
                if (GvrControllerInput.TouchPos.x > 0.3 && GvrControllerInput.TouchPos.x < 0.7)
                {
    //top part of the daydream contreller
                    if (GvrControllerInput.TouchPos.y < 0.3)
                    {
                        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
                        forward.y = 0;
                        this.transform.position += forward * Time.deltaTime * speedForward; 
                    }
    //bottom part of the daydream contreller
                    if (GvrControllerInput.TouchPos.y > 0.7)
                    {
                        Vector3 back = Camera.main.transform.TransformDirection(Vector3.back);
                        back.y = 0;
                        this.transform.position += back * Time.deltaTime * speedBack;
                    }
                }
    //center part of the daydream contreller X axis
                if (GvrControllerInput.TouchPos.y > 0.3 && GvrControllerInput.TouchPos.y < 0.7)
                {
    //left part of the daydream contreller
                    if (GvrControllerInput.TouchPos.x < 0.3)
                    {
                        canRotate = false;
                        StartCoroutine(WaitForSecLeft());
                    }
    //right part of the daydream contreller
                    if (GvrControllerInput.TouchPos.x > 0.7)
                    {
                        canRotate = false;
                        StartCoroutine(WaitForSecRight());
                    }
                }
            }
        }
        
    }
    //delay of 0.5 seconds to prevent double rotation
    IEnumerator WaitForSecLeft()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.Rotate(0, -30, 0);
        canRotate = true;
    }
    IEnumerator WaitForSecRight()
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.Rotate(0, 30, 0);
        canRotate = true;
    }
}
