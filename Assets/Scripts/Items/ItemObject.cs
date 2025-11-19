using UnityEngine;

public enum categories
    {
        plant,
        tea,
        tool,
        misc
    }

[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
    public string name;
    public categories category;

    public float weight;
}
