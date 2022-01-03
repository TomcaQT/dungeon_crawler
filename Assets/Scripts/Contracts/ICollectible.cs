public interface ICollectible
{
    public void OnPickUp(PlayerStats playerStats);

    public bool AutoCollect { get;set; }
}
