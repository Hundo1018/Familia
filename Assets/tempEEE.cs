using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempEEE : MonoBehaviour
{
    private float _speed = 3;
    public GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            transform.rotation.SetLookRotation(_target.transform.position, Vector3.down);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        List<Collider2D> temp = new();
        int c = other.GetContacts(temp);
        Debug.Log(c);
    }
}
