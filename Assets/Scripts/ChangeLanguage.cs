using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private Sprite _ru;
    [SerializeField] private Sprite _en;

    private void Start()
    {
        if (LanguageController.Instance.CurrentLanguage == "en")
        {
            GetComponent<Image>().sprite = _en;
        }
        else if (LanguageController.Instance.CurrentLanguage == "ru")
        {
            GetComponent<Image>().sprite = _ru;
        }
        else
        {
            GetComponent<Image>().sprite= _en;
        }
    }
}
