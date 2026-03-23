using System;
using UnityEngine;

public class tile_fake_player_trigger_rena : Tile
{
    public GameObject fakePlayer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Tile>() != null)
        {
            Tile otherTile = other.gameObject.GetComponent<Tile>();
            if (otherTile.hasTag(TileTags.Player))
            {
                fakePlayer.GetComponent<tile_fake_player_rena>().isActivated = true;
                Destroy(gameObject);
            }
        }
    }
}