
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class tile_hiddenTreasure_rena:Tile
{
    public List<GameObject> hiddenTreasures;
    public GameObject curProp;
    private void Awake()
    {
        curProp = hiddenTreasures[Random.Range(0, hiddenTreasures.Count)];
        /*SpriteRenderer propSpr = curProp.GetComponentInChildren<SpriteRenderer>();
        if (propSpr)
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
            
            if (_sprite != null)
            {
                _sprite.sprite = propSpr.sprite;
            }
        }*/
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded || curProp == null) return;
        Transform room = transform.parent;
        if (room == null) return;
        Vector2 gridPos = toGridCoord(transform.localPosition.x, transform.localPosition.y);
        spawnTile(curProp, room, (int)gridPos.x, (int)gridPos.y);
    }
}
