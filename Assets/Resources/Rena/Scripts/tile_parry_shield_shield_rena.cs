using UnityEngine;

public class tile_parry_shield_shield_rena : Tile
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Tile>() != null) {
            Tile otherTile = other.gameObject.GetComponent<Tile>();
            if (otherTile.hasTag(TileTags.Enemy)) {
                // 弹开
                Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
                if (otherBody != null) {
                    Vector2 knockbackDir = (otherTile.transform.position - transform.position).normalized;
                    otherBody.AddForce(knockbackDir * 1000f * otherBody.mass);
                }
                // 伤害
                otherTile.takeDamage(this, 2);
            }
        }
    }
}