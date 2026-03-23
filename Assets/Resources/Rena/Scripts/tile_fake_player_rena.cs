using System;
using UnityEngine;

public class tile_fake_player_rena : Tile {

    public float moveSpeed = 10f;
    public float moveAcceleration = 100f;

    protected int _walkDirection = 2;

    // 开关
    public bool isActivated = false;

    void FixedUpdate() {
        if (!isActivated) return;

        bool tryToMoveUp    = Input.GetKey(KeyCode.UpArrow)    || Input.GetKey(KeyCode.W);
        bool tryToMoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool tryToMoveDown  = Input.GetKey(KeyCode.DownArrow)  || Input.GetKey(KeyCode.S);
        bool tryToMoveLeft  = Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.A);

        Vector2 attemptToMoveDir = Vector2.zero;

        if (tryToMoveUp)         attemptToMoveDir += Vector2.down; 
        else if (tryToMoveDown)  attemptToMoveDir += Vector2.up;

        if (tryToMoveRight)      attemptToMoveDir += Vector2.left;
        else if (tryToMoveLeft)  attemptToMoveDir += Vector2.right;

        attemptToMoveDir.Normalize();
        
        if (attemptToMoveDir.x > 0)       _sprite.flipX = true;
        else if (attemptToMoveDir.x < 0)  _sprite.flipX = false;


        if (attemptToMoveDir.y != 0 && attemptToMoveDir.x == 0) {
            _walkDirection = attemptToMoveDir.y > 0 ? 0 : 2;
        } else if (attemptToMoveDir.x != 0) {
            _walkDirection = 1;
        }

        _anim.SetBool("Walking", attemptToMoveDir != Vector2.zero);
        _anim.SetInteger("Direction", _walkDirection);

        moveViaVelocity(attemptToMoveDir, moveSpeed, moveAcceleration);

        updateSpriteSorting();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Tile otherTile = other.gameObject.GetComponent<Tile>();
        if (otherTile != null && otherTile.hasTag(TileTags.Player))
        {
            other.gameObject.GetComponent<Player>().takeDamage(otherTile, 1);
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Tile otherTile = other.gameObject.GetComponent<Tile>();
        if (otherTile != null)
        {
            if (otherTile.hasTag(TileTags.Weapon))
            {
                takeDamage(this, 1);
            }
        }
    }

}