using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker : MonoBehaviour
{
    public Tilemap tilemap; // Kéo và thả Tilemap mà bạn muốn duyệt qua vào đây

    void Start()
    {
        // Lấy giới hạn của Tilemap trong local space
        BoundsInt bounds = tilemap.cellBounds;

        // Duyệt qua tất cả các vị trí trong Tilemap
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);

            if (tile != null)
            {
                // Xử lý tile đã tìm thấy tại vị trí pos
                Debug.Log("Tìm thấy tile " + tile.name + " tại vị trí " + pos);
            }
        }
    }
}