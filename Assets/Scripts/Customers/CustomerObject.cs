using UnityEngine;

[CreateAssetMenu]
public class CustomerObject : ScriptableObject
{
    public string name;
    public string[] entryDialogues;
    public string[] satisfiedDialogues;
    public string[] disappointedDialogues;
}
