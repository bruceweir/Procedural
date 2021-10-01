using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Building Settings")]
public class BuildingSettings : ScriptableObject
{
    public Vector2Int buildingSize;

    public int numberOfStories;
    public RoofStrategy roofStrategy;
    public WallsStrategy wallsStrategy;
    public WingsStrategy wingsStrategy;
    public WingStrategy wingStrategy;
    public StoryStrategy storyStrategy;
    public StoriesStrategy storiesStrategy;
    public Vector2Int Size { get { return buildingSize; } }


}
