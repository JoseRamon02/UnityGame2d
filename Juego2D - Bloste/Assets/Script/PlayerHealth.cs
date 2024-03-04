using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Image healthImg;
    public bool isInmune;
    public float inmunityTime;
    public Blink material;
    public SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        health = maxHealth;
        material.original = sprite.material;
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = health/100;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            StartCoroutine(Inmunity());

            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            } else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            if (health <= 0)
            {
                print("Player dead");
            }
        }
    }


    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}
