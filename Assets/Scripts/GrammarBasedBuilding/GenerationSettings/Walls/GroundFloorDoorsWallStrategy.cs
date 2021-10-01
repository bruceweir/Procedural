using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Walls/Door on Ground Floor")]
public class GroundFloorDoorsWallStrategy : WallsStrategy
{
    public override Wall[] GenerateWalls(BuildingSettings settings, RectInt bounds, int level)
    {
        Wall[] walls = new Wall[(bounds.size.x + bounds.size.y) * 2];


        for (int i = 0; i < walls.Length; i++)
        {
            walls[i] = (Wall)UnityEngine.Random.Range(0, 2);
        }

        if (level == 0)
        {
            walls[0] = Wall.Door;
        }

        return walls;
    }
}
