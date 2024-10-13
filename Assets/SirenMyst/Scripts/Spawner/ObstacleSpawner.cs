using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : Spawner
{
    protected static ObstacleSpawner instance;
    public static ObstacleSpawner Instance => instance;

    public static string stilts = "Stilts";
    public static string farmer = "Farmer";
    public static string farmerPoleElectric_1 = "FarmerPoleElectric_1";
    public static string farmerPoleElectric_2 = "FarmerPoleElectric_2";

    public static string FarmerBanana_1 = "FarmerBanana_1";
    public static string FarmerBanana_2 = "FarmerBanana_2";
    public static string FarmerBanana_3 = "FarmerBanana_3";
    public static string FarmerBanana_4 = "FarmerBanana_4";

    public static string StiltsHouse_1 = "StiltsHouse_1";
    public static string StiltsHouse_2 = "StiltsHouse_2";
    public static string StiltsHouse_3 = "StiltsHouse_3";
    public static string StiltsHouse_4 = "StiltsHouse_4";
    public static string StiltsHouse_5 = "StiltsHouse_5";
    public static string StiltsHouse_6 = "StiltsHouse_6";
    public static string StiltsHouse_7 = "StiltsHouse_7";

    protected override void Awake()
    {
        base.Awake();
        if (ObstacleSpawner.instance != null) Debug.LogError("Only 1 JunkSpawner allow to exist");
        ObstacleSpawner.instance = this;
    }
}
