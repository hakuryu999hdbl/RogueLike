using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public bool roomLeft, roomRight, roomUp, roomDown;
    //看清距离起点的距离
    public int stepToStart;
    public Text text;
    public int doorNumber;

    public GameObject ShowColor;

    public void UpdateRoom()
    {
        //计算距离初始点的网格距离
        stepToStart = (int)(Mathf.Abs(transform.position.x / 70) + Mathf.Abs(transform.position.y / 15));

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }


    //因为看不清而增加
    public void SetStartRoom()
    {
        roomType = RoomType.Start;
        ShowColor.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
    }
    public void SetEndRoom()
    {
        roomType = RoomType.Boss;
        ShowColor.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }


    //为了能和自己坐标一致的WallMap建立联系

    public const int CellX = 70;
    public const int CellY = 15;

    public enum RoomType { Normal, Start, Boss }
    public RoomType roomType = RoomType.Normal;

    Vector2Int GridPos => new Vector2Int(
        Mathf.RoundToInt(transform.position.x / CellX),
        Mathf.RoundToInt(transform.position.y / CellY)
    );
    void Start()
    {
        RoomRegistry.Register(GridPos, this);
    }

    void OnDestroy()
    {
        RoomRegistry.Unregister(GridPos, this);
    }
}
