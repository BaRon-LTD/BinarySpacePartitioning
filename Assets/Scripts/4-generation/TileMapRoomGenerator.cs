using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapRoomGenerator : MonoBehaviour {
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("Map width and height")]
    [SerializeField] int roomWidth = 50;
    [SerializeField] int roomHeight = 50;

    [Tooltip("Minimum room size")]
    [SerializeField] int minRoomSize = 6;

    [SerializeField] private float probabilitySplit = 0.5f;
    private RoomGenerator roomGenerator;

    void Start() {
        roomGenerator = new RoomGenerator(roomWidth, roomHeight, minRoomSize , probabilitySplit);
        roomGenerator.GenerateMap();
        DisplayMap(roomGenerator.Map);
    }

    private void DisplayMap(int[,] map) {
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                var position = new Vector3Int(x, y, 0);
                var tile = map[x, y] == 1 ? wallTile : floorTile;
                tilemap.SetTile(position, tile);
            }
        }
    }
}
