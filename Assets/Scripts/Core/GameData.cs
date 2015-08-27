using UnityEngine;
using System.Collections;

[System.Serializable]
public static class GameData : System.Object
{
    /// <summary>
    /// Initializer
    /// </summary>
    static GameData()
    {
        #region Default states
        
        _rank = 0;
        _money = 0;
        _upgradeLevelSword = 0;
        _upgradeLevelWhip = 0;
        _upgradeLevelWhip = 0;
        _equippedWeapon = Constants.weapon_sword;
        _settingSound = true;

        #endregion

        if (ES2.Exists(Constants.data_rank))
            _rank = ES2.Load<int>(Constants.data_rank);

        if (ES2.Exists(Constants.data_money))
            _money = ES2.Load<int>(Constants.data_money);

        if (ES2.Exists(Constants.data_upgrade_sword))
            _upgradeLevelSword = ES2.Load<int>(Constants.data_upgrade_sword);

        if (ES2.Exists(Constants.data_upgrade_whip))
            _upgradeLevelWhip = ES2.Load<int>(Constants.data_upgrade_whip);

        if (ES2.Exists(Constants.data_upgrade_crossbow))
            _upgradeLevelCrossbow = ES2.Load<int>(Constants.data_upgrade_crossbow);

        if (ES2.Exists(Constants.data_equipped_weapon))
            _equippedWeapon = ES2.Load<string>(Constants.data_equipped_weapon);

        if (ES2.Exists(Constants.settings_sound))
            _settingSound = ES2.Load<bool>(Constants.settings_sound);
    }

    /// <summary>
    /// The current rank of the character
    /// </summary>
    private static int _rank;
    public static int rank
    {
        get
        {
            return _rank;
        }
        set
        {
            _rank = value;
            ES2.Save(_rank, Constants.data_rank);
        }
    }

    /// <summary>
    /// The amount of money the character owns at the moment
    /// </summary>
    private static int _money;
    public static int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            ES2.Save(_money, Constants.data_money);
        }
    }

    private static string _equippedWeapon;
    public static string equippedWeapon
    {
        get
        {
            return _equippedWeapon;
        }
        set
        {
            _equippedWeapon = value;
            ES2.Save(_equippedWeapon, Constants.data_equipped_weapon);
        }
    }

    /// <summary>
    /// Upgrade level of the sword
    /// </summary>
    private static int _upgradeLevelSword;
    public static int upgradeLevelSword
    {
        get
        {
            return _upgradeLevelSword;
        }
        set
        {
            _upgradeLevelSword = value;
            ES2.Save(_upgradeLevelSword, Constants.data_upgrade_sword);
        }
    }

    /// <summary>
    /// Upgrade level of the Whip
    /// </summary>
    private static int _upgradeLevelWhip;
    public static int upgradeLevelWhip
    {
        get
        {
            return _upgradeLevelWhip;
        }
        set
        {
            _upgradeLevelWhip = value;
            ES2.Save(_upgradeLevelWhip, Constants.data_upgrade_whip);
        }
    }

    /// <summary>
    /// Upgrade level of the crossbow
    /// </summary>
    private static int _upgradeLevelCrossbow;
    public static int upgradeLevelCrossbow
    {
        get
        {
            return _upgradeLevelCrossbow;
        }
        set
        {
            _upgradeLevelCrossbow = value;
            ES2.Save(_upgradeLevelCrossbow, Constants.data_upgrade_crossbow);
        }
    }

    #region General settings

    private static bool _settingSound;
    public static bool settingSound
    {
        get
        {
            return _settingSound;
        }
        set
        {
            _settingSound = value;
            ES2.Save(_settingSound, Constants.settings_sound);
        }
    }

    #endregion
}
