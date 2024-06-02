using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    /* ���������� ����� */
    [SerializeField] private TextMeshProUGUI _score;
    /* ---------- */

    private void Start()
    {
        _score.text = PlayerPrefs.GetFloat("RecordScoreQuantity").ToString();
    }
    public void ChangeSceneMainGame()
    /* ����� ����� �� ���� */
    {
        SceneManager.LoadScene("MainGame");
    } 
}
