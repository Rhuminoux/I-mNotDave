using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgueSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform fromPoint;
    [SerializeField] private Transform toPoint;
    [SerializeField] private int quantity;

    private void Awake()
    {
        while (quantity > 0){
            GameObject algue = Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Count)],
            new Vector3(UnityEngine.Random.Range(fromPoint.position.x, toPoint.position.x), transform.position.y, 0f),
            Quaternion.identity);
            algue.transform.parent = transform;
            quantity--;
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
            sr.enabled = false;
    }
}
