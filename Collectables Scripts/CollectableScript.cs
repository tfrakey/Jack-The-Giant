using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyCollectable", 10.0f);
    }    

    void DestroyCollectable()
    {
        gameObject.SetActive(false);
    }
}
