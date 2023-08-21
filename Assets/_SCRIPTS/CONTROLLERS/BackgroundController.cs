using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]private MeshRenderer m_renderer;
    [SerializeField]private Material m_material;

    public float speed = 1; //could be cool to make it go faster if the player goes faster

    // Update is called once per frame
    void Update()
    {
        m_material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }

    public void ChangeMaterial(Material newMaterial)
    {
        RenderSettings.skybox = newMaterial;
    }
}
