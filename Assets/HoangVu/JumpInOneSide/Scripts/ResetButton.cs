using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void OnClick()
    {
        GameController.Instance.ReloadScene();
    }
}
