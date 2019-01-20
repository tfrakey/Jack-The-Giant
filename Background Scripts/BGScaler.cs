using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float width = sr.sprite.bounds.size.x; //width of our background sprite
        float worldHeight = Camera.main.orthographicSize * 2.0f; //Determine the world height from the camera
        float worldWidth = worldHeight / Screen.height * Screen.width; //Determine the world width by the camera

        tempScale.x = worldWidth / width; //Set the x of the local scale variable
        transform.localScale = tempScale; //Set the local scale to the final calculated value, essentially stretching the background to fit the camera
    }

    
}
