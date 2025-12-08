using UnityEngine;

[CreateAssetMenu]
public class CustomerObject : ScriptableObject
{
    [Header("�����")]
    public string name;
    public Sprite portrait;
    public Vector2 spawnPosition;
    public Vector2 scale = Vector2.one;
    public RecipeSO wantedRecipe;

    [Header("������� ����� / ������")]
    public string[] entryDialogues;       
    public string orderText;              

    [Header("������ ����� ������")]
    public string questionText;          
    public string[] answerOptions;           
    public int correctAnswerIndex;       

    [Header("�������")]
    public string[] satisfiedDialogues;   
    public string[] disappointedDialogues;  
}
