using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MovingObject
{
    [SerializeField]private Vector2 m_spawnPoint;
    [SerializeField]private Vector2 m_destination;
    [SerializeField]private Vector2 m_direction;

    private void Awake()
    {
        
        m_spawnPoint.y = PlayerController.Singleton.transform.position.y - 5;
        m_spawnPoint.x = Random.Range(PlayerController.Singleton.transform.position.y - 8, PlayerController.Singleton.transform.position.y + 8);

        transform.position = m_spawnPoint;

        _movingDirection = PlayerController.Singleton.transform.position - transform.position;
        if (_movingDirection.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    
    private Vector3 _movingDirection;
    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Singleton.transform.position) > 15)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += (_movingDirection * speed * Time.fixedDeltaTime);
    }
}
