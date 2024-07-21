using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_End_Game : MonoBehaviour
{
    public GameObject GameOver; 
    public void gameOver()
    {
        GameOver.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuGame");
    }    
}
