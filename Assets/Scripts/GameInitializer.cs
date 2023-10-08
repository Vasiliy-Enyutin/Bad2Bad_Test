using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    [Inject]
    private GameFactoryService _gameFactoryService = null!;

    private void Awake()
    {
        _gameFactoryService.CreatePlayer();
        CreateEnemies();
    }

    private void CreateEnemies()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        List<Vector3> tileWorldLocations = new();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {   
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        _gameFactoryService.CreateEnemies(tileWorldLocations);
    }
}
