using System;
using UnityEngine;

public class tile_boss_portal_trigger_rena : MonoBehaviour
{
    public GameObject boss;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Tile>() != null)
        {
            Tile otherTile = other.gameObject.GetComponent<Tile>();
            if (otherTile.hasTag(TileTags.Player))
            {
                boss.GetComponent<tile_boss_portal_rena>().activate();
                Destroy(gameObject);
            }
        }
    }
}
