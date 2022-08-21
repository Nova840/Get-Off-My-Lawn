using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Rigidbody2D))]
public class Particle : MonoBehaviour
{
    private float timeRemaining;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Spawn(Vector2 pos, Vector2 vel, float lifetime, Sprite sprite)
    {
        Particle p = Instantiate(this, (Vector3)pos, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.body.velocity = vel;
        p.spriteRenderer.sprite = sprite;
        p.timeRemaining = lifetime;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0.0f)
            Destroy(gameObject);
    }
}
