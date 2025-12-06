using UnityEngine;

[CreateAssetMenu]
public class CustomerObject : ScriptableObject
{
    [Header("Общее")]
    public string name;
    public Sprite portrait;
    public Vector2 spawnPosition;
    public Vector2 scale = Vector2.one;

    [Header("Диалоги входа / заказа")]
    public string[] entryDialogues;       
    public string orderText;              

    [Header("Вопрос после заказа")]
    public string questionText;          
    public string[] answerOptions;           
    public int correctAnswerIndex;       

    [Header("Реакции")]
    public string[] satisfiedDialogues;   
    public string[] disappointedDialogues;  
}
