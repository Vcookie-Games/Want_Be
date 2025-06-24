public interface IObstacle
{
    static bool isPlayerProtect{ get; set; }
    bool isPlayerDead { get; }
    void Active();
    void DeActive(); 

}
