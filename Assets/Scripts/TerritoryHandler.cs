using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(ParticleSystem))]
public class TerritoryHandler : MonoBehaviour
{

    private SpriteRenderer sprite;
    private ParticleSystem particle;

    public Color32 oldColor;
    public Color32 startColor;
    public Color32 hoverColor;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        sprite.color = startColor;
    }

    void OnMouseEnter()
    {
        oldColor = sprite.color;
        sprite.color = hoverColor;
        particle.Play();
    }

    void OnMouseExit()
    {
        sprite.color = oldColor;
        particle.Stop();
    }
}
