using UnityEngine;

public class room_0_rena : Room
{
    // 几个基础版本
    // V1:只有墙
    // 别的版本还没想
    public float borderWallProbability = 0.7f;
    
    public override void fillRoom(LevelGenerator ourGenerator, ExitConstraint requiredExits)
    {
        roomGenerationV1(ourGenerator, requiredExits);
    }
    protected void roomGenerationV1(LevelGenerator ourGenerator, ExitConstraint requiredExits) {
        generateWalls(ourGenerator, requiredExits);
    }
    protected void generateWalls(LevelGenerator ourGenerator, ExitConstraint requiredExits)
    {
        // Basically we go over the border and determining where to spawn walls.
        bool[,] wallMap = new bool[LevelGenerator.ROOM_WIDTH, LevelGenerator.ROOM_HEIGHT];
        for (int x = 0; x < LevelGenerator.ROOM_WIDTH; x++)
        {
            for (int y = 0; y < LevelGenerator.ROOM_HEIGHT; y++)
            {
                if (x == 0 || x == LevelGenerator.ROOM_WIDTH - 1
                           || y == 0 || y == LevelGenerator.ROOM_HEIGHT - 1)
                {
                    if (x == LevelGenerator.ROOM_WIDTH / 2
                        && y == LevelGenerator.ROOM_HEIGHT - 1
                        && requiredExits.upExitRequired)
                    {
                        wallMap[x, y] = false;
                    }
                    else if (x == LevelGenerator.ROOM_WIDTH - 1
                             && y == LevelGenerator.ROOM_HEIGHT / 2
                             && requiredExits.rightExitRequired)
                    {
                        wallMap[x, y] = false;
                    }
                    else if (x == LevelGenerator.ROOM_WIDTH / 2
                             && y == 0
                             && requiredExits.downExitRequired)
                    {
                        wallMap[x, y] = false;
                    }
                    else if (x == 0
                             && y == LevelGenerator.ROOM_HEIGHT / 2
                             && requiredExits.leftExitRequired)
                    {
                        wallMap[x, y] = false;
                    }
                    else
                    {
                        wallMap[x, y] = Random.value <= borderWallProbability;
                    }
                    continue;
                }
                wallMap[x, y] = false;
            }
        }
        for (int x = 0; x < LevelGenerator.ROOM_WIDTH; x++) {
            for (int y = 0; y < LevelGenerator.ROOM_HEIGHT; y++) {
                if (wallMap[x, y]) {
                    Tile.spawnTile(ourGenerator.normalWallPrefab, transform, x, y);
                }
            }
        }
    }

}
