using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inicia Juego");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.relativeVelocity.magnitude > 2)
            Debug.Log("Colision detectada con "+ collision.gameObject.name);
    }
}
