using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        for(int i = 0; i < _gameController.BackgroundParts.Length; i++)  // Перемещение фона вслед за камерой
        {
            _gameController.BackgroundParts[i].transform.position = new Vector3(_gameController.BackgroundParts[i].transform.position.x, _gameController.VirtualCamera.transform.position.y, _gameController.BackgroundParts[i].transform.position.z);
        }
    }
}
