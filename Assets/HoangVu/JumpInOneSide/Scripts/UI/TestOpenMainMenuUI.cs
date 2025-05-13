using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class TestOpenMainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<ChooseInventoryUICanvas>();
    }

   
}
