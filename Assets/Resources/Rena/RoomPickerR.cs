using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomPickerR : Room
{
    public List<ValidatedRoomR> RoomChoices;

    public override Room createRoom(ExitConstraint requiredExits)
    {
        /*foreach (ValidatedRoom room in RoomChoices)
        {
            room.ValidateExits();
        }*/

        List<ValidatedRoomR> validRooms = new List<ValidatedRoomR>();

        foreach (ValidatedRoomR room in RoomChoices)
        {
            room.ValidateExits();
            if (room.MeetsConstraints(requiredExits)) // check if the selected room meets requirements send from level generator
                validRooms.Add(room);
        }

        ValidatedRoomR roomRPrefab = GlobalFuncs.randElem(validRooms);
        return roomRPrefab.GetComponent<Room>().createRoom(requiredExits);
    }
}