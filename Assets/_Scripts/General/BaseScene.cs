using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public static BaseScene Instance;

    //Managers
    public AIManager aiManager;

    //Scene components
    public Port[] enterancePortals;
    public Room[] rooms;

    public void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
    }


    public Room.RoomType GetCurrentRoomForPosition(Vector3 worldPos)
    {
        foreach (Room room in rooms)
        {
            if (room.CheckIfInRoom(worldPos))
            {
                return room.roomType;
            }
        }

        return Room.RoomType.NONE;
    }
}
