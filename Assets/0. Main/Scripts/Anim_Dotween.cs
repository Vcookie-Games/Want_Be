using UnityEngine;
using DG.Tweening;

public class Example : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject Restart_btn;
    public GameObject Main_mnu_btn;
    void Start()
    {
        // Đặt kích thước ban đầu của đối tượng là rất nhỏ (ví dụ: 0.1)
       GameOverUI.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Phóng to đối tượng đến kích thước ban đầu (1, 1, 1) trong 1 giây
        GameOverUI.transform.DOScale(new Vector3(1f, 1f, 1f), 1.5f);

        // Xoay đối tượng 350 độ trên trục Z trong 1 giây
        GameOverUI.transform.DORotate(new Vector3(0f, 0f, 360f), 1.5f, RotateMode.FastBeyond360);


        // animation 2 nút restart và main menu
        // Đặt vị trí ban đầu của đối tượng
        float initialY = transform.localPosition.y;
        float bounceHeight = 3f; // Chiều cao nhấp nhô
        float duration = 1.5f; // Thời gian cho một chu kỳ nhấp nhô

        // Tạo hiệu ứng nhấp nhô lên xuống liên tục cho Restart_btn
        float initialY_Restart = Restart_btn.transform.localPosition.y;
        Restart_btn.transform.DOLocalMoveY(initialY_Restart + bounceHeight, duration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);

        // Tạo hiệu ứng nhấp nhô lên xuống liên tục cho Main_mnu_btn
        float initialY_MainMenu = Main_mnu_btn.transform.localPosition.y;
        Main_mnu_btn.transform.DOLocalMoveY(initialY_MainMenu + bounceHeight, duration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
