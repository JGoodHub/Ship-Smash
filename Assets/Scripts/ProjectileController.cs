using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rigid2D;

    private float armingTime;
    private string sourceTag;
    public string targetTag;

    public GameObject explosionPrefab;

    public void Initialise (string _sourceTag, string _targetTag) {
        rigid2D = GetComponent<Rigidbody2D>();        
        armingTime = Time.fixedDeltaTime * 2f;
        sourceTag = _sourceTag;
        targetTag = _targetTag;
        Destroy(gameObject, 5f);
    }
   
    public void FixedUpdate () {
        armingTime -= Time.fixedDeltaTime;
        rigid2D.MovePosition(transform.position + (transform.up * speed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (armingTime <= 0 && other.CompareTag(targetTag)) {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position + (transform.up * 0.1f), Quaternion.identity);
            explosionInstance.transform.SetParent(GameManager.instance.effectsPool);
            Destroy(explosionInstance, 0.267f);
            Destroy(gameObject);

            //Deal Damage to others stats
        }
    }



}
