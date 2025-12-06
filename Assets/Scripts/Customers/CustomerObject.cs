using UnityEngine;

[CreateAssetMenu]
public class CustomerObject : ScriptableObject
{
    public string name;
    public Sprite portrait;
    public Vector2 spawnPosition;
    public Vector2 scale = Vector2.one;
    public string[] entryDialogues;
    public string[] satisfiedDialogues;
    public string[] disappointedDialogues;
}
