using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (LocalizationManager.Instance != null)
        {
            tmp.text = LocalizationManager.Instance.GetText(key);
        }
    }
}