using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton

    public static PlayerStats PlayerStatsInstance;

    private void Awake()
    {
        if (PlayerStatsInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        PlayerStatsInstance = this;
    }
    #endregion

    public Stat Strength;
    public Stat Dexterity;
    public Stat Constitution;
    public Stat Intelligence;
    public Stat Charisma;

}
