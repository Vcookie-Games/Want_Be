using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    protected static ObstacleSpawner instance;
    public static ObstacleSpawner Instance => instance;

    public static readonly string[] Farmers = {
        "Farmer",
        "FarmerPoleElectric_1",
        "FarmerPoleElectric_2",
        "FarmerBanana_1",
        "FarmerBanana_2",
        "FarmerBanana_3",
        "FarmerBanana_4"
    };

    public static readonly string[] Stilts = {
        "Stilts",
        "StiltsHouse_1",
        "StiltsHouse_2",
        "StiltsHouse_3",
        "StiltsHouse_4",
        "StiltsHouse_5",
        "StiltsHouse_6",
        "StiltsHouse_7"
    };

    public static readonly string[] StiltHousesV2 = {
        "StiltHouseV2_1",
        "StiltHouseV2_2",
        "StiltHouseV2_3",
        "BacLieuHouseV2",
        "AncientHouseV2",
        "VillaV2_1",
        "VillaV2_2",
        "VillaV2_3"
    };

    protected override void Awake()
    {
        base.Awake();
        if (ObstacleSpawner.instance != null) Debug.LogError("Only 1 JunkSpawner allow to exist");
        ObstacleSpawner.instance = this;
    }
}
