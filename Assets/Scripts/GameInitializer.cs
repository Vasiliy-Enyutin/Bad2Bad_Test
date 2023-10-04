using Services;
using UnityEngine;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    [Inject]
    private GameFactoryService _gameFactoryService = null!;

    private void Awake()
    {
        _gameFactoryService.CreatePlayer();
    }
}
