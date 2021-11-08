using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{

    /*
    |----------------------------------------------------------|
    | Base script Writen by Windexglow 11-13-10                |
    |----------------------------------------------------------|
    |   WASD: basic movement                                   |
    |   Shift: Makes camera accelerate                         |
    |----------------------------------------------------------|
    |                                                          |
    |----------------------------------------------------------|
    | Modified by Syrko 11-08-21                               |
    |----------------------------------------------------------|
    |   Made variables modifiable in the Unity Editor          |
    |   Controls work only when the right mouse button is held |
    |   Confines the camera within limits                      |
    |----------------------------------------------------------|
    */

    [Header("Camera Settings")]
    [SerializeField, Tooltip("Regular speed")]
    float mainSpeed = 100.0f;
    [SerializeField, Tooltip("Multiplied by how long shift is held.  Basically running")]
    float shiftAdd = 250.0f; 
    [SerializeField, Tooltip("Maximum speed when holdin shift")]
    float maxShift = 1000.0f; 
    [SerializeField, Tooltip("How sensitive it with mouse")]
    float camSens = 0.25f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun = 1.0f;
    
    // Flag to process the first frame that the right mouse button is held down
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
		{
            // Processing the Camera angle
            if (!isMoving)
            {
                isMoving = true;
                lastMouse = transform.eulerAngles;
            }
            else
            {
                lastMouse = Input.mousePosition - lastMouse;
                lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
                lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            }
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition; 

            // Processing the camera movement
            Vector3 p = GetBaseInput();
            if (p.sqrMagnitude > 0)
            { // Only move while a direction key is pressed
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    totalRun += Time.deltaTime;
                    p *= totalRun * shiftAdd;
                    p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                    p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                    p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
                }
                else
                {
                    totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                    p *= mainSpeed;
                }

                p *= Time.deltaTime;
                Vector3 newPosition = transform.position;
                transform.Translate(p);
                newPosition.x = Mathf.Clamp(transform.position.x, -25F, 30F);
                newPosition.y = Mathf.Clamp(transform.position.y, 2.7f, 27f);
                newPosition.z = Mathf.Clamp(transform.position.z, -25f, 30f);
                transform.position = newPosition;
            }
        }

		if (Input.GetKeyUp(KeyCode.Mouse1))
		{
            isMoving = false;
        }
    }

    private Vector3 GetBaseInput()
    {
        // Returns the basic values
        // If it's 0 then it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}