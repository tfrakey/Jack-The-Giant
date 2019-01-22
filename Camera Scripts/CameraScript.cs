using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Private variables
    private float speed = 1.0f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;

    private float easySpeed = 2.5f;
    private float mediumSpeed = 3.0f;
    private float hardSpeed = 3.8f;

    //Public variables
    [HideInInspector]
    public bool moveCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(GamePreferences.GetEasyDifficultyState() == 0)
        {
            maxSpeed = easySpeed;
        }

        if (GamePreferences.GetMediumDifficultyState() == 0)
        {
            maxSpeed = mediumSpeed;
        }

        if (GamePreferences.GetHardDifficultyState() == 0)
        {
            maxSpeed = hardSpeed;
        }

        moveCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            MoveCamera();
        }
            
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position;
        float oldY = temp.y;
        float newY = temp.y - (speed * Time.deltaTime);
        temp.y = Mathf.Clamp(temp.y, oldY, newY);
        transform.position = temp;
        speed += acceleration * Time.deltaTime;

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}
