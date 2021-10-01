using UnityEngine;

[CreateAssetMenu(menuName = "Building Generation/Wings/Each Random Height")]

public class EachWingRandomHeightStrategy : WingsStrategy
{
    public override Wing[] GenerateWings(BuildingSettings settings)
    {
        Wing[] wings = new Wing[settings.Size.x * settings.Size.y];

        int count = 0;

        for (int x = 0; x < settings.Size.x; x++)
        {
            for (int y = 0; y < settings.Size.y; y++)
            {
                int wingHeight = UnityEngine.Random.Range(1, settings.numberOfStories + 1);

                wings[count] = settings.wingStrategy != null ?
                         settings.wingStrategy.GenerateWing(settings, new RectInt(x, y, 1, 1), wingHeight) :
                         ScriptableObject.CreateInstance<DefaultWingStrategy>().GenerateWing(settings, new RectInt(x, y, 1, 1), wingHeight);

                count += 1;
            }
        }

        return wings;
    }
}

