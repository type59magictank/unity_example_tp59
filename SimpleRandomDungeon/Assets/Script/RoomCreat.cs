using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomCreat : MonoBehaviour
{
    public enum Direction { up,down,left,right};
    public Direction direction;


    [Header("房间控制")]
    public GameObject roomPrefab;
    public int roomNum;
    public Color startColor, endColor;
    public GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    bool _______________________;

    public List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roomNum; i++)
        {
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>());

            //改变位置
            ChangePointPos();

        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        //rooms[roomNum-1].GetComponent<SpriteRenderer>().color = endColor;
        endRoom = rooms[0].gameObject;

        //找到最后房间
        foreach (var room in rooms)
        {
            //if (room.transform.position.x* room.transform.position.x/ ((xOffset / yOffset) * (xOffset / yOffset)) + room.transform.position.y * room.transform.position.y 
            //    > endRoom.transform.position.x*endRoom.transform.position.x / ((xOffset / yOffset)* (xOffset / yOffset)) + endRoom.transform.position.y * endRoom.transform.position.y)
            //{
            //    endRoom = room.gameObject;
            //}
            SetUpRoom(room, room.transform.position);
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
    }

    public void SetUpRoom(Room newRoom,Vector3 roomPos)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPos + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPos + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPos + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPos + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
    }

}
