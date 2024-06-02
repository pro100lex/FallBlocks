using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        transform.position = new Vector3(0f, _gameController.FindMaxHeightBlockFromFallenBlocks(), 0f);
    }

    public void CreateBlock()
    /* Создание блока */
    {
        Instantiate(_gameController.BlockTypes[_gameController.BlockNextType], new Vector3(0f, transform.position.y + _gameController.HeightSpawnBlock, 0f), Quaternion.identity);
    }

}
