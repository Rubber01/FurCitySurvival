using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOwnerController : MonoBehaviour
{
    private BuildingTile buildingTile;
    private void Start()
    {
        buildingTile = gameObject.GetComponent<BuildingTile>();
    }
    public void SetPlayerOwnership(bool _value)
    {
        if (_value == true)
        {
            buildingTile.isControlledByPlayer = _value;
            buildingTile.isRaidable = !(_value);
        }
        else
        {
            buildingTile.isControlledByPlayer = !(_value);
            buildingTile.isRaidable =  _value;
        }
    }


}
