using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Roof/Always Random")]

public class AlwaysRandomRoof : RoofStrategy
{
    public override Roof GenerateRoof(BuildingSettings settings, RectInt bounds)
    {

        return new Roof((RoofType)Random.Range(0, 4));

    }
}