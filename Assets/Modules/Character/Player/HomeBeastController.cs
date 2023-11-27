using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBeastController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<AnemyController>().Die();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<AnemyController>().Die();
        
    }
}
