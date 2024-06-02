using UnityEngine;

public class BlockController : MonoBehaviour
{
    /* ����� ��� �������� ��������������� */
    [SerializeField] private LayerMask _layerMaskPlatformElement;
    [SerializeField] private LayerMask _layerMaskDestroyZone;
    /* ---------- */

    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (gameObject.tag == "FallingBlock")  // ������� �����
        {
            transform.position += new Vector3(0f,  -_gameController.BlockFallSpeed * _gameController.BlockFallSpeedRatio, 0f) * Time.deltaTime;
        }
            

        if (Mathf.Abs(transform.position.x) >= 12f || transform.position.y <= -20f)   // �������� "������" 
        {
            Destroy(gameObject);
            _gameController.LoseGame();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_layerMaskPlatformElement & (1 << collision.gameObject.layer)) != 0)  // �������� ���������������
        {   
            if (gameObject.tag == "FallingBlock")
            {
                gameObject.layer = 8;
                gameObject.tag = "FallenBlock";
                gameObject.GetComponent<Rigidbody2D>().gravityScale = _gameController.GravityScale;
                _gameController.QuantityBlockOnPlatform += 1;

                _gameController.BlockHasFallen();
            }
        }
    }

    public void BlockMoveDown()
    /* ��������� ����� ��� ������� �� ������ */
    {
        _gameController.BlockFallSpeedRatio = 3f;
    }

    public void BlockStopMoveDown()
    /* ����������� ��������� ����� ��� ���������� ������ */
    {
        _gameController.BlockFallSpeedRatio = 1f;
    }

    public void BlockMoveLeft()
    /* �������� ����� ����� ��� ������� �� ������ */
    {
        transform.position += new Vector3(-_gameController.BlockSideShifting, 0f, 0f);
    }

    public void BlockMoveRight()
    /* �������� ����� ������ ��� ������� �� ������ */
    {
        transform.position += new Vector3(_gameController.BlockSideShifting, 0f, 0f);
    }

    public void BlockRotate()
    /* ��������� ����� ��� ������� �� ������ */
    {
        transform.eulerAngles += new Vector3(0f, 0f, 90f);
    }

}
