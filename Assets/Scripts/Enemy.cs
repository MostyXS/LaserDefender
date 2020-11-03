using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health=100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenshots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed =1f;
    [SerializeField] GameObject DeathVFX;
    [SerializeField] float DurationOfExplosion=1f;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip shootClip;
    [Range(0, 1)] [SerializeField] float volume = 0.5f;
    [SerializeField] int scoreValue;
    private void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenshots, maxTimeBetweenShots);
        ParticleSystem particle = FindObjectOfType<ParticleSystem>();
    }
    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter<=0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenshots, maxTimeBetweenShots);
        }

    }

    private void Fire()
    {
        GameObject EnemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        EnemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position,volume);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position,volume);
        Destroy(gameObject);
        GameObject explosion = Instantiate(DeathVFX, transform.position, transform.rotation);
        Destroy(explosion, DurationOfExplosion);

    }
}
