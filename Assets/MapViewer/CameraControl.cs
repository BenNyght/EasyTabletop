using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.  
    Made simple to use (drag and drop, done) for regular keyboard layout  
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/


    float mainSpeed = 10; //regular speed
    float shiftAdd = 50; //multiplied by how long shift is held.  Basically running
    float maxShift = 20; //Maximum speed when holdin gshift
    private float totalRun = 1.0f;

    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool panning = false;

    void Update()
    {
        //Keyboard commands
        Vector3 p = GetInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 100);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        transform.Translate(p);
        


        // When LMB clicked get mouse click position and set panning to true
        if (Input.GetKeyDown(KeyCode.Mouse1) && !panning)
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panning = true;
        }
        // If LMB is already clicked, move the camera following the mouse position update
        if (panning)
        {
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;
            transform.position += new Vector3(-distance.x, -distance.y, 0);
        }

        // If LMB is released, stop moving the camera
        if (Input.GetKeyUp(KeyCode.Mouse1))
            panning = false;

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, 0, 90, Space.Self);
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, 0, -90, Space.Self);
        }




        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            float cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
            if (cameraSize < 5)
            {
                gameObject.GetComponent<Camera>().orthographicSize += 1;
            }
            
        } else if(Input.GetKeyDown(KeyCode.Minus) || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            float cameraSize = gameObject.GetComponent<Camera>().orthographicSize;
            if (cameraSize > 1)
            {
                gameObject.GetComponent<Camera>().orthographicSize -= 1;
            }
        }

    }

    private Vector3 GetInput()
    {
        Vector3 vel = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            vel += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel += new Vector3(1, 0, 0);
        }
        return vel;
    }
}
