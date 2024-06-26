using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using DG.Tweening;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static GameController instance;

    private Tilemap tilemap;

    private Vector3Int cake;
    private Vector3Int gift;

    private Vector3Int direction;

    public GameObject framePrefab;
    public GameObject candyPrefab;
    public GameObject cakePrefab;
    public GameObject giftPrefab;

    private GameObject cakeObj;
    private GameObject giftObj;

    public TextMeshProUGUI timeText;
    public int time;

    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        timeText.text = "00 : 45";
        time = 45;
        StartCoroutine(CalculateTime());
        
        tilemap = FindObjectOfType<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach(Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            
            if(tile == null) { continue; }
            if (tile.name == "cake ")
            {
                cake = pos;
                Debug.Log("cake" + pos);
                cakeObj = Instantiate(cakePrefab, pos, Quaternion.identity, tilemap.transform);
            }
            if (tile.name == "gift box")
            {
                gift = pos;
                Debug.Log("gift" + pos);
                giftObj = Instantiate(giftPrefab, pos, Quaternion.identity, tilemap.transform);
            }
            if (tile.name == "candy ")
            {
                Instantiate(candyPrefab, pos, Quaternion.identity, tilemap.transform);
            }
            Instantiate(framePrefab, pos , Quaternion.identity, tilemap.transform);
        }
    }

    IEnumerator CalculateTime()
    {
        yield return new WaitForSeconds(1);
        time--;
        timeText.text = "00 : "+time;
        if(time == 0)
        {
            Debug.Log("lost");
            MenuGamePlay.instance.LostEvent();
            yield break;
        }
        StartCoroutine(CalculateTime());
    }

    public void StopCoron()
    {
        StopAllCoroutines();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Lấy vị trí bắt đầu của touch/finger
        Vector2 startPosition = eventData.position - eventData.delta;

        // Tính vector di chuyển từ vị trí bắt đầu đến vị trí hiện tại
        Vector2 dragVector = eventData.position - startPosition;

        // Xác định hướng kéo dựa trên dragVector
        if (Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
        {
            if (dragVector.x > 0)
            {
                Debug.Log("Drag right");
                direction = Vector3Int.right;
            }
            else
            {
                Debug.Log("Drag left");
                direction= Vector3Int.left;
            }
        }
        else
        {
            if (dragVector.y > 0)
            {
                Debug.Log("Drag up");
                direction = Vector3Int.up;
            }
            else
            {
                Debug.Log("Drag down");
                direction = Vector3Int.down;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");

        while (CheckNextToTile(cake, direction))
        {
            var temp = cake + direction;
            //Debug.Log(temp);
            // Lấy tile tại vị trí A và B
            TileBase tileA = tilemap.GetTile(cake);
            TileBase tileB = tilemap.GetTile(temp);

            // Đổi chỗ 2 tile
            tilemap.SetTile(cake, tileB);
            tilemap.SetTile(temp, tileA);

            cake = temp;
        }
        
        while (CheckNextToTile(gift, direction))
        {
            var temp = gift + direction;

            // Lấy tile tại vị trí A và B
            TileBase tileA = tilemap.GetTile(gift);
            TileBase tileB = tilemap.GetTile(temp);

            // Đổi chỗ 2 tile
            tilemap.SetTile(gift, tileB);
            tilemap.SetTile(temp, tileA);

            gift = temp;
        }
        while (CheckNextToTile(cake, direction))
        {
            var temp = cake + direction;
            //Debug.Log(temp);
            // Lấy tile tại vị trí A và B
            TileBase tileA = tilemap.GetTile(cake);
            TileBase tileB = tilemap.GetTile(temp);

            // Đổi chỗ 2 tile
            tilemap.SetTile(cake, tileB);
            tilemap.SetTile(temp, tileA);

            cake = temp;
        }
        giftObj.transform.DOMove(gift, 0.2f);
        cakeObj.transform.DOMove(cake, 0.2f);
    }



    public bool CheckNextToTile(Vector3Int obj, Vector3Int direction)
    {
        if (tilemap.GetTile(obj + direction) != null
            && tilemap.GetTile(obj + direction).name == "frame") return true;

        if ( direction == Vector3Int.down)
        {
            if(tilemap.GetTile(obj + direction) == null) return false;
            if(tilemap.GetTile(obj + direction).name == "gift box")
            {
                Debug.Log("win");
                MenuGamePlay.instance.WinEvent();
                cake = obj + direction;
                return false;
            }
            
        }

        return false;
    }
}
