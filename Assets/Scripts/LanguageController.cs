using System.Runtime.InteropServices;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLang();

    [SerializeField] private string _currentLanguage;

    public static LanguageController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CurrentLanguage = GetLang();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string CurrentLanguage { get { return _currentLanguage; } set { _currentLanguage = value; } }
}
