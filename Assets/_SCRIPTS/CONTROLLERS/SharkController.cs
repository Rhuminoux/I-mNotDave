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
        m_direction.x = Random.Range(0, 2) == 0 ? -1 : 1;
        m_direction.y = Random.Range(0, 2) == 0 ? 1 : -1;

        if (m_direction.x == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            m_destination.x = PlayerController.Singleton.transform.position.x - 9;
            m_spawnPoint.x = PlayerController.Singleton.transform.position.x + 9;
        }
        else
        {
            m_destination.x = PlayerController.Singleton.transform.position.x + 9;
            m_spawnPoint.x = PlayerController.Singleton.transform.position.x - 9;
        }
        

        m_destination.y = Random.Range(PlayerController.Singleton.transform.position.y + 3.9f,
                PlayerController.Singleton.transform.position.y - 3.9f);
        m_spawnPoint.y = m_destination.y * -1;

        transform.position = m_spawnPoint;

        _movingDirection = (Vector3)m_destination - transform.position;
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
        transform.position += Vector3.up * Time.fixedDeltaTime * 0.5f;
    }
}
