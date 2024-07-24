using UnityEngine;

public class WantBeSettingManager : MonoBehaviour
{
    public GameObject settingPopup;

    private void Start()
    {
        settingPopup.SetActive(false);
    }
    public void Click()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            settingPopup.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            settingPopup.SetActive(false);
        }
    }
}
