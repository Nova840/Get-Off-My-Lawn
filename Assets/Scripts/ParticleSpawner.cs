using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public Sprite[] sprites;

    [Range(1.0f, 20.0f)]
    public int particleCount = 10;

    [Range(10.0f, 180.0f)]
    public float angleSpread = 90.0f;

    public float ySpread = 2.0f;

    [Range(0.25f, 4.0f)]
    public float lifetime = 1.0f;

    public float velocity = 5.0f;

    private Particle particle;
    private float phase;
    private float rate;

    private void Awake()
    {
        rate = particleCount / lifetime;
        particle = GetComponentInChildren<Particle>();
        if (particle == null)
        {
            Debug.LogError("Particle spawner is missing particle");
        }
        particle.gameObject.SetActive(false);
    }

    private void Update()
    {
        phase += rate * Time.deltaTime;
        int count = (int)phase;
        phase -= count;
        //Debug.LogFormat("COUNT: {0}", count);
        for (int i = 0; i < count; i++)
        {
            float angle = 0.5f * Mathf.Deg2Rad * Random.Range(-angleSpread, +angleSpread);
            Vector2 pos = (Vector2)transform.position
                + new Vector2(0, 0.5f * Random.Range(-ySpread, ySpread));
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 vel = dir * velocity;
            Sprite sprite = sprites[Random.Range(0, sprites.Length)];
            particle.Spawn(pos, vel, lifetime, sprite);
        }
    }
}
