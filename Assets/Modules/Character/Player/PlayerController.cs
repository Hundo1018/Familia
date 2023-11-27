using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.TouchEnded += Shot;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shot(Vector2 vector2)
    {
        
    }
}
