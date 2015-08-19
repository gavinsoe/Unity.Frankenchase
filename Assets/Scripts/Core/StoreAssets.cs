using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;
using Soomla.Store;

public class StoreAssets : IStoreAssets
{
    #region Constants

    #region Currency

    public const string CURRENCY_GOLD_ID = "currency_gold";

    #endregion
    #region Weapons

    public const string WEAPON_SWORD_ID = "weapon_sword";
    public const string WEAPON_WHIP_ID = "weapon_whip";
    public const string WEAPON_CROSSBOW_ID = "weapon_crossbow";

    #endregion
    #region Upgrades

    public const string UPGRADE_SWORD_ID = "upgrade_sword";
    public const string UPGRADE_WHIP_ID = "upgrade_whip";
    public const string UPGRADE_CROSSBOW_ID = "upgrade_crossbow";

    #endregion
    #region Categories

    public const string CATEGORY_WEAPON_NAME = "weapon";

    #endregion
    #endregion

    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] {
            CURRENCY_GOLD
        };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] {
            WEAPON_SWORD,
            WEAPON_WHIP,
            WEAPON_CROSSBOW,
            UPGRADE_SWORD,
            UPGRADE_WHIP,
            UPGRADE_CROSSBOW
        };
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { };
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[]{
            CATEGORY_WEAPON
        };
    }

    #region Virtual Currencies

    public static VirtualCurrency CURRENCY_GOLD = new VirtualCurrency(
        "Gold", //name
        "", //description
        CURRENCY_GOLD_ID
    );

    #endregion
    #region Weapons

    public static VirtualGood WEAPON_SWORD = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Sword",
        "[Sword Description]",
        WEAPON_SWORD_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID, 0)
    );

    public static VirtualGood WEAPON_WHIP = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Whip",
        "[Whip Description]",
        WEAPON_WHIP_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID, 0)
    );

    public static VirtualGood WEAPON_CROSSBOW = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,
        "Crossbow",
        "[Crossbow Description]",
        WEAPON_CROSSBOW_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID, 0)
    );

    public static List<string> WEAPON_LIST =
        new List<string>(new string[]{
            WEAPON_SWORD_ID,
            WEAPON_WHIP_ID,
            WEAPON_CROSSBOW_ID
        });

    #endregion
    #region Upgrades

    public static VirtualGood UPGRADE_SWORD = new SingleUseVG(
        "Sword Upgrade",
        "Not exactly consumable, just used to track number of upgrades",
        UPGRADE_SWORD_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID,0)
    );

    public static VirtualGood UPGRADE_WHIP = new SingleUseVG(
        "Sword Upgrade",
        "Not exactly consumable, just used to track number of upgrades",
        UPGRADE_WHIP_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID, 0)
    );

    public static VirtualGood UPGRADE_CROSSBOW = new SingleUseVG(
        "Sword Upgrade",
        "Not exactly consumable, just used to track number of upgrades",
        UPGRADE_CROSSBOW_ID,
        new PurchaseWithVirtualItem(CURRENCY_GOLD_ID, 0)
    );
    #endregion
    #region Virtual Categories

    public static VirtualCategory CATEGORY_WEAPON = new VirtualCategory(
        CATEGORY_WEAPON_NAME,
        new List<string>(new string[] { 
            WEAPON_SWORD_ID,
            WEAPON_WHIP_ID,
            WEAPON_CROSSBOW_ID
        })
    );

    #endregion
    
    #region Helper methods

    public static List<string> GetItemsInCategory(string categoryName)
    {
        if (categoryName == CATEGORY_WEAPON_NAME)
        {
            return WEAPON_LIST;
        } 
        else
        {
            return new List<string>();
        }
    }

    #endregion

}
