using UnityEngine;
using TMPro;
using System.Collections;

public class OrderTextBar : MonoBehaviour
{

    public TMP_Text textObj;
    private string curText = "";

    public void DisplayText(string text)
    {
        StopAllCoroutines();
        curText = "";
        StartCoroutine(PrintText(text));
    }

    private IEnumerator PrintText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            curText += text[i];
            textObj.text = curText;
            yield return new WaitForSeconds(0.03f);
        }
        curText = "";
        yield return null;
    }
}
