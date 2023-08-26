using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFishController : MonoBehaviour
{
    public float speed;
    public int fishValue;
    [SerializeField] private CollectibleFishSO m_fishSO;
    [SerializeField] private List<Sprite> m_referenceSprites;

    private Animator m_animator;
    private List<Sprite> m_currentSprites;
    private SpriteRenderer m_spriteRrenderer;
    private Vector2 m_spawnPoint;
    private Vector2 m_destination;
    private Vector2 m_direction;

    // Update is called once per frame
    private void Awake()
    {
        fishValue *= m_fishSO.lvl;
        m_animator = GetComponent<Animator>();
        m_currentSprites = m_fishSO.animationSprites;
        m_spriteRrenderer = GetComponent<SpriteRenderer>();

        m_spawnPoint.y = PlayerController.Singleton.transform.position.y - 5;
        m_spawnPoint.x = Random.Range(PlayerController.Singleton.transform.position.x - 8, PlayerController.Singleton.transform.position.x + 8);

        transform.position = m_spawnPoint;

        _movingDirection = (PlayerController.Singleton.transform.position - transform.position).normalized;
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

    private void LateUpdate()
    {
        for (int i = 0; i < m_referenceSprites.Count; i++)
        {
            if (m_spriteRrenderer.sprite == m_referenceSprites[i])
            {
                m_spriteRrenderer.sprite = m_currentSprites[i];
                return;
            }
        }
    }

    protected void Move()
    {
        transform.position += (_movingDirection * speed * Time.fixedDeltaTime);
    }

    public void GetCaught(Transform harpoon)
    {
        m_animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = m_fishSO.deathSprite;
        transform.parent = harpoon;
        speed = 0;
    }
}
