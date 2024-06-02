using Cinemachine;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    /* ����� �������� ������ */
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    /* ---------- */

    /* ���������� ��������� */
    [SerializeField] private GameObject _blockSpawner;
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _backgroundHint;
    [SerializeField] private GameObject[] _blockTypes;
    [SerializeField] private GameObject[] _backgroundHintsList;
    [SerializeField] private GameObject[] _nextBlockTypeImage;
    [SerializeField] private GameObject _virtualCamera;
    [SerializeField] private GameObject _cameraFollow;
    [SerializeField] private GameObject _startGameHeightPoint;
    [SerializeField] private string _gameLanguage;
    /* ---------- */

    /* ���������� ����� */
    [SerializeField] private float _blockFallSpeed;
    [SerializeField] private float _blockSideShifting;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _blockFallSpeedRatio;
    [SerializeField] private float _heightSpawnBlock;
    /* ---------- */

    /* ��������� ������ */
    [SerializeField] private TextMeshProUGUI _scoreCurrentGameText;
    [SerializeField] private TextMeshProUGUI _scoreRecordAllGames;
    [SerializeField] private Image _blockNextTypeImage;
    /* ---------- */

    /* ������ ��� */
    [SerializeField] private GameObject[] _backgroundParts;
    /* ---------- */

    /* ���������� ���� */
    private GameObject _fallingBlock;
    private int _quantityBlockOnPlatform;
    private int _blockNextType;
    /* ---------- */

    /* ����� ��������� */
    [SerializeField] private GameObject _screenLoseGame;
    /* ---------- */

    /* ���������������� ������� */
    [SerializeField] private GameObject _playerHUD;
    /* ---------- */

    /* ����� ����� */
    [SerializeField] private GameObject _screenPauseGame;
    /* ---------- */

    /* ����� ����� ���� */
    [SerializeField] private TextMeshProUGUI _endGameScreenScoreCurrentGame;
    [SerializeField] private TextMeshProUGUI _endGameScreenScoreAllGames;
    /* ---------- */

    private void Start()
    {
        BlockNextType = UnityEngine.Random.Range(0, BlockTypes.Length - 1);
        BlockSpawner.GetComponent<SpawnerController>().CreateBlock();
        HintNextBlockUpdate();

        ScoreRecordAllGames.text = PlayerPrefs.GetFloat("RecordScoreQuantity").ToString();

        for (int i = 0; i < BackgroundHintsList.Length; i++)   // �������� �������� ��������� ��� ������
        {
            BackgroundHintsList[i].transform.localScale = new Vector3(BackgroundHintsList[i].transform.localScale.x, VirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize * 10f, BackgroundHintsList[i].transform.localScale.z);
        }
    }

    private void Update()
    {
        FallingBlock = GameObject.FindGameObjectWithTag("FallingBlock");

        BackgroundHint.GetComponent<BackgroundHintController>().ChangeBackgroundHint();
    }

    public float FindMaxHeightBlockFromFallenBlocks()
    /* ���������� ������������ ������ ����� �� ��� ������� */
    {
        GameObject[] fallenBlocks = GameObject.FindGameObjectsWithTag("FallenBlock");
        float maxHeightCurrentFallenBlocks = Platform.transform.position.y;

        for (int i = 0; i < fallenBlocks.Length; i++)
        {
            if (fallenBlocks[i].transform.position.y > maxHeightCurrentFallenBlocks)
            {
                maxHeightCurrentFallenBlocks = fallenBlocks[i].transform.position.y;
            }
        }

        return maxHeightCurrentFallenBlocks;
    }

    private void GameScoreUpdate()
    /* ���������� ����� ���� */
    {
        ScoreCurrentGameText.text = (QuantityBlockOnPlatform).ToString();

        if (PlayerPrefs.GetFloat("RecordScoreQuantity") < QuantityBlockOnPlatform)
        {
            PlayerPrefs.SetFloat("RecordScoreQuantity", QuantityBlockOnPlatform);
        }
    }

    public void LoseGame()
    /* �������� */
    {
        PlayerHUD.SetActive(false);
        ScreenLoseGame.SetActive(true);

        Time.timeScale = 0f;

        EndGameScreenScoreCurrentGame.text = (QuantityBlockOnPlatform).ToString();
        EndGameScreenScoreAllGames.text = PlayerPrefs.GetFloat("RecordScoreQuantity").ToString();
    }

    public void PauseGame()
    /* ����� ���� */
    {
        ScreenPauseGame.SetActive(true);
        PlayerHUD.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    /* ����������� ���� */
    {
        ScreenPauseGame.SetActive(false);
        PlayerHUD.SetActive(true);

        Time.timeScale = 1f;
    }

    public void RestartGame()
    /* ������������� ���� */
    {
        ShowAdv();

        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    /* ����� � ���� */
    {
        ShowAdv();

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void OnButtonArrowDownDown()
    /* ��������� ������� ��� ������� ������ ���� */
    {
        FallingBlock.GetComponent<BlockController>().BlockMoveDown();
    }

    public void OnButtonArrowDownUp()
    /* ��������� ������� ��� ���������� ������ ���� */
    {
        FallingBlock.GetComponent<BlockController>().BlockStopMoveDown();
    }

    public void OnButtonArrowLeft()
    /* ��������� ������� ��� ������� ������ ����� */
    {
        FallingBlock.GetComponent<BlockController>().BlockMoveLeft();
    }

    public void OnButtonArrowRight()
    /* ��������� ������� ��� ������� ������ ������ */
    {
        FallingBlock.GetComponent<BlockController>().BlockMoveRight();
    }

    public void OnButtonArrowsRotate()
    /* ��������� ������� ��� ������� ������ ��������� */
    {
        FallingBlock.GetComponent<BlockController>().BlockRotate();
    }

    public void BlockHasFallen()
    /* ��������� ������� ��� ������� ����� */
    {
        BlockSpawner.GetComponent<SpawnerController>().CreateBlock();
        HintNextBlockUpdate();

        GameScoreUpdate();

    }

    private void HintNextBlockUpdate()
    /* ���������� ��������� ����������� ���������� ����� */
    {
        BlockNextType = UnityEngine.Random.Range(0, BlockTypes.Length - 1);

        for (int i = 0; i < NextBlockTypeImage.Length; i++) // ��������� ���� ����� � ���������
        {
            NextBlockTypeImage[i].SetActive(false);
        }
        NextBlockTypeImage[BlockNextType].SetActive(true);
    }

    public int BlockNextType { get { return _blockNextType; } set { _blockNextType = value; } }
    public float BlockFallSpeed { get { return _blockFallSpeed; } }
    public float BlockSideShifting { get { return _blockSideShifting; } }
    public float GravityScale { get { return _gravityScale; } }
    public GameObject[] BlockTypes { get { return _blockTypes; } }
    public TextMeshProUGUI ScoreCurrentGameText { get { return _scoreCurrentGameText; } set { _scoreCurrentGameText = value; } }
    public int QuantityBlockOnPlatform { get { return _quantityBlockOnPlatform; } set { _quantityBlockOnPlatform = value; } }
    public float BlockFallSpeedRatio { get { return _blockFallSpeedRatio; } set { _blockFallSpeedRatio = value; } }
    public GameObject[] BackgroundHintsList { get { return _backgroundHintsList; } }
    public GameObject FallingBlock { get { return _fallingBlock; } set { _fallingBlock = value; } }
    public float HeightSpawnBlock { get { return _heightSpawnBlock; } }
    public GameObject[] BackgroundParts { get { return _backgroundParts; } }
    public GameObject VirtualCamera { get { return _virtualCamera; } }
    public GameObject PlayerHUD { get { return _playerHUD; } }
    public GameObject ScreenLoseGame { get { return _screenLoseGame; } }
    public TextMeshProUGUI EndGameScreenScoreCurrentGame { get { return _endGameScreenScoreCurrentGame; } }
    public TextMeshProUGUI EndGameScreenScoreAllGames { get { return _endGameScreenScoreAllGames; } }
    public GameObject ScreenPauseGame { get { return _screenPauseGame; } }
    public GameObject Platform { get { return _platform; } }
    public TextMeshProUGUI ScoreRecordAllGames { get { return _scoreRecordAllGames; } }
    public GameObject BlockSpawner { get { return _blockSpawner; } }
    public GameObject BackgroundHint { get { return _backgroundHint; } }
    public GameObject[] NextBlockTypeImage { get { return _nextBlockTypeImage; } }
    public GameObject StartGameHeightPoint { get { return _startGameHeightPoint; } }
    public GameObject CameraFollow { get { return _cameraFollow; } }
}
