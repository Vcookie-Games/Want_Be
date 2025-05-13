using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class PictureInventoryUICanvas : UICanvas
{
    [SerializeField] private List<PictureItem> _pictureItems;

    public override void Open()
    {
        base.Open();
        Refresh();
    }

    void Refresh()
    {
        foreach (var item in _pictureItems)
        {
            item.Refresh();
        }
    }

    public void BackToChoose()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<ChooseInventoryUICanvas>();
    }
}
