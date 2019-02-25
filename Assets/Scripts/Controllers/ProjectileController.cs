using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    //-----VARIABLES-----

    public float speed;
    public int damage;

    private Rigidbody2D rigid2D;

    private float armingTime;
    private string sourceTag;
    public string targetTag;

    public GameObject explosionPrefab;

    //-----METHODS-----

    //Setup the object and set its lifespan to 5 seconds
    public void Initialise (string _sourceTag, string _targetTag) {
        rigid2D = GetComponent<Rigidbody2D>();        
        armingTime = Time.fixedDeltaTime * 2f;
        sourceTag = _sourceTag;
        targetTag = _targetTag;
        Destroy(gameObject, 5f);
    }
   
    //Move the projectile forward each frame
    public void FixedUpdate () {
        armingTime -= Time.fixedDeltaTime;
        rigid2D.MovePosition(transform.position + (transform.up * speed * Time.fixedDeltaTime));
    }

    //If its armed and hits the target object then reduce that objects health and create an explsion
    void OnTriggerEnter2D (Collider2D other) {
        if (armingTime <= 0 && other.CompareTag(targetTag)) {
            //Create the explosion
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position + (transform.up * 0.1f), Quaternion.identity);
            explosionInstance.transform.SetParent(GameManager.instance.effectsPool);
            Destroy(explosionInstance, 0.267f);
            Destroy(gameObject);

            //Deal damage to the others objects stats
            StatsController stats = other.GetComponent<StatsController>();
            stats.Damage(damage);
        }
    }



}
