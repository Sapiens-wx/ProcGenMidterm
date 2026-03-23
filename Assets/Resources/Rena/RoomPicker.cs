using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomPicker : Room
{
    public List<ValidatedRoom> RoomChoices;

    public override Room createRoom(ExitConstraint requiredExits)
    {
        /*foreach (ValidatedRoom room in RoomChoices)
        {
            room.ValidateExits();
        }*/

        List<ValidatedRoom> validRooms = new List<ValidatedRoom>();

        foreach (ValidatedRoom room in RoomChoices)
        {
            room.ValidateExits();
            if (room.MeetsConstraints(requiredExits)) // check if the selected room meets requirements send from level generator
                validRooms.Add(room);
        }

        ValidatedRoom roomPrefab = GlobalFuncs.randElem(validRooms);
        return roomPrefab.GetComponent<Room>().createRoom(requiredExits);
    }
}