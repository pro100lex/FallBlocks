using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_gameController.FallingBlock.transform.position.y > _gameController.StartGameHeightPoint.transform.position.y)
        {
            _gameController.CameraFollow.transform.position = new Vector3(0f, _gameController.FallingBlock.transform.position.y, 0f);
        }
        else
        {
            _gameController.CameraFollow.transform.position = new Vector3(0f, _gameController.StartGameHeightPoint.transform.position.y, 0f);
        }
    }
}
