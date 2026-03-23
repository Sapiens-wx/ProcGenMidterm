using System;
using UnityEngine;

public class tile_shovel_rena: Tile
{
    public GameObject maskPrefab;
    private GameObject spawnedMask;
    private Tile pointedTile;
    public override void init()
    {
        base.init();
        // 初始时先生成一个，但先隐藏掉
        if (maskPrefab != null && spawnedMask == null)
        {
            spawnedMask = Instantiate(maskPrefab);
            spawnedMask.SetActive(false);
        }
    }
    public override void pickUp(Tile tilePickingUsUp)
    {
        base.pickUp(tilePickingUsUp);
        if (spawnedMask != null) spawnedMask.SetActive(true);
    }
    public override void dropped(Tile tileDroppingUs)
    {
        base.dropped(tileDroppingUs);
        if (spawnedMask != null) spawnedMask.SetActive(false);
    }
    
    private void Update()
    {
        if (_tileHoldingUs && spawnedMask)
        {
            updateMaskPosition();
        }
    }

    private void updateMaskPosition()
    {
        // 获取当前房间
        Transform room = _tileHoldingUs.transform.parent;
        if (room == null) return;

        // 坐标转换
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseLocalPos = room.InverseTransformPoint(mouseWorldPos);
        Vector3 playerLocalPos = room.InverseTransformPoint(_tileHoldingUs.transform.position);
        
        // 计算鼠标方向
        Vector3 direction = mouseLocalPos - playerLocalPos;

        // 获取玩家当前格格子
        Vector2 playerGrid = toGridCoord(playerLocalPos.x, playerLocalPos.y);
        int targetX = (int)playerGrid.x;
        int targetY = (int)playerGrid.y;

        // 十字格逻辑判定（上下or左右）
        // 设置一个判定死区（Deadzone），防止鼠标在玩家中心时 Mask 乱跳
        float deadzone = TILE_SIZE * 0.2f; 
        
        if (Mathf.Abs(direction.x) > deadzone) 
        {
            targetX += (direction.x > 0) ? 1 : -1;
        }
        
        if (Mathf.Abs(direction.y) > deadzone) 
        {
            targetY += (direction.y > 0) ? 1 : -1;
        }

        if (targetX == (int)playerGrid.x && targetY == (int)playerGrid.y)
        {

        }
        
        // 将网格索引转回世界坐标并赋值给预览框
        Vector2 maskLocalPos = toWorldCoord(targetX, targetY);

        if (_tileHoldingUs)
        {
            Vector2 worldPos = room.TransformPoint(maskLocalPos);
            pointedTile = tileAtPoint(worldPos, (TileTags)(-1));
        }

        if (pointedTile) 
        {
            if (pointedTile.CompareTag("HiddenTreasure")) 
            {
                Debug.Log("Treasure is aimed！");
            }
        }
        spawnedMask.transform.SetParent(room); 
        spawnedMask.transform.localPosition = new Vector3(maskLocalPos.x, maskLocalPos.y, -0.2f);
    }
    
    private void OnDestroy()
    {
        if (spawnedMask != null) Destroy(spawnedMask);
    }

    public override void useAsItem(Tile tileUsingUs)
    {
        if (pointedTile)
        {
            if (pointedTile.CompareTag("HiddenTreasure"))
            {
                Destroy(pointedTile.gameObject);
            }
        }

    }
}
