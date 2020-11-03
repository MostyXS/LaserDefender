using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float XPadding = 1f;
    [SerializeField] float YMinPadding = 1f;
    [SerializeField] float YMaxPadding = 1f;
    [SerializeField] int health;
    [SerializeField] AudioClip PlayerDeathSound;
 
    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] AudioClip laserClip;
    [Range(0, 1)] [SerializeField] float volume = 0.5f;
    
    

    Coroutine firingCoroutine;
    
    
    
    float XMin, XMax, YMin, YMax;
    void Start()
    {
        SetUpMoveBoundaries();
        
       
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) {return;}
        ProcessHit(damageDealer);
        

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<SceneLoader>().LoadGameOver();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(PlayerDeathSound, Camera.main.transform.position, volume);
            
        }
    }

    
   
  
    

    IEnumerator FireContinuosly()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserClip, Camera.main.transform.position,volume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void Fire()
    {
         
        if (Input.GetButtonDown("Fire1"))
        {
           firingCoroutine = StartCoroutine(FireContinuosly());

        }   
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newXPos =Mathf.Clamp( transform.position.x+deltaX,XMin,XMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, YMin, YMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        XMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + XPadding;
        XMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - XPadding;
        YMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + YMinPadding;
        YMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - YMaxPadding;
    }
    public int GetHealthInfo()
    {
        return health;
    }


}
