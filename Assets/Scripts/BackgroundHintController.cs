using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundHintController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        transform.position = new Vector3(_gameController.FallingBlock.transform.position.x, _gameController.FallingBlock.transform.position.y, transform.position.z);
    }

    public void ChangeBackgroundHint()
    {
        int blockWidth = (int)(Mathf.Round(_gameController.FallingBlock.GetComponent<PolygonCollider2D>().bounds.size.x / 1.5f));

        for (int i = 0; i < _gameController.BackgroundHintsList.Length; i++)
        {
            _gameController.BackgroundHintsList[i].SetActive(false);
        }

        _gameController.BackgroundHintsList[blockWidth - 1].SetActive(true);
    } 
}
