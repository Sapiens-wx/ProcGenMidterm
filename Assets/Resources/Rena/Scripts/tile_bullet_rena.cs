using System;
using System.Collections;
using UnityEngine;

public class tile_bullet_rena : Tile
{
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Tile otherTile = other.gameObject.GetComponent<Tile>();
        if (otherTile != null)
        {
            if (otherTile.hasTag(TileTags.Player))
            {
                other.gameObject.GetComponent<Player>().takeDamage(this, 1);
                Destroy(gameObject);
            }
            else if (otherTile.hasTag(TileTags.Weapon))
            {
                takeDamage(this, 1);
            }
        }
    }

    public void Shoot(Vector2 direction, float speed, float lifetime, int bulletHealth)
    {
        health = bulletHealth;
        StartCoroutine(StartShooting(direction, speed, lifetime));
    }

    public IEnumerator StartShooting(Vector2 direction, float speed, float lifetime)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed; 
        yield return new WaitForSeconds(lifetime); 
        Destroy(gameObject);
    }

    public void ShootSplit(Vector2 direction, float speed, float lifetime, int bulletHealth, float timeBeforeSplit,int numSplit)
    {
        health = bulletHealth;
        StartCoroutine(StartSplitShooting(direction, speed, lifetime,timeBeforeSplit,numSplit));
    }

    public IEnumerator StartSplitShooting(Vector2 direction, float speed,float lifetime, float timeBeforeSplit, int numSplit)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * speed;
        yield return new WaitForSeconds(timeBeforeSplit);
        
        float angleStep = 360/numSplit;
        for (int i = 0; i < numSplit; i++)
        {
            float angle = angleStep * i;
            Vector2 splitDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            tile_bullet_rena newBullet = Instantiate(this, transform.position, Quaternion.identity);
            newBullet.Shoot(splitDir, speed, lifetime, health);
        }
        Destroy(gameObject);
    }



}
