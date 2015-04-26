
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;


/// <summary>
/// This class defines our game's economy, which includes virtual goods, virtual currencies
/// and currency packs, virtual categories
/// </summary>
public class ShopAssets : IStoreAssets{
	
	/// <summary>
	/// see parent.
	/// </summary>
	public int GetVersion() {
		return 125;
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{COIN_CURRENCY};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualGood[] GetGoods() {
		return new VirtualGood[] {/*MUFFINCAKE_GOOD, PAVLOVA_GOOD,CHOCLATECAKE_GOOD, CREAMCUP_GOOD, */FREE_CHAR0_LTVG,UNLOCK_CHAR1_LTVG,UNLOCK_CHAR2_LTVG,UNLOCK_CHAR3_LTVG,UNLOCK_CHAR4_LTVG,
			FREE_NOABILITY_LTVG,UNLOCK_ABILITY1_LTVG,UNLOCK_ABILITY2_LTVG,UNLOCK_ABILITY3_LTVG,UNLOCK_ABILITY4_LTVG};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[] {COIN_ONE,COIN_PACK/*TENMUFF_PACK, FIFTYMUFF_PACK, FOURHUNDMUFF_PACK, THOUSANDMUFF_PACK*/};
	}
	
	/// <summary>
	/// see parent.
	/// </summary>
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}
	
	/** Static Final Members **/
	
	public const string COIN_CURRENCY_ITEM_ID      = "currency_coin";
	
	public const string TENMUFF_PACK_PRODUCT_ID      = "android.test.refunded";
	
	public const string FIFTYMUFF_PACK_PRODUCT_ID    = "android.test.canceled";
	
	public const string FOURHUNDMUFF_PACK_PRODUCT_ID = "android.test.purchased";
	
	public const string THOUSANDMUFF_PACK_PRODUCT_ID = "2500_pack";
	
	public const string MUFFINCAKE_ITEM_ID   = "fruit_cake";
	
	public const string PAVLOVA_ITEM_ID   = "pavlova";
	
	public const string CHOCLATECAKE_ITEM_ID   = "chocolate_cake";
	
	public const string CREAMCUP_ITEM_ID   = "cream_cup";
	
	public const string UNLOCK_CHAR1_LIFETIME_PRODUCT_ID = "phiephierun_unlock_char1";
	public const string UNLOCK_CHAR2_LIFETIME_PRODUCT_ID = "phiephierun_unlock_char2";
	public const string UNLOCK_CHAR3_LIFETIME_PRODUCT_ID = "phiephierun_unlock_char3";
	public const string UNLOCK_CHAR4_LIFETIME_PRODUCT_ID = "phiephierun_unlock_char4";

	public const string UNLOCK_ABILITY1_LIFETIME_PRODUCT_ID = "phiephierun_unlock_ability1";
	public const string UNLOCK_ABILITY2_LIFETIME_PRODUCT_ID = "phiephierun_unlock_ability2";
	public const string UNLOCK_ABILITY3_LIFETIME_PRODUCT_ID = "phiephierun_unlock_ability3";
	public const string UNLOCK_ABILITY4_LIFETIME_PRODUCT_ID = "phiephierun_unlock_ability4";

	
	public const string COIN_PACK100_PRODUCT_ID="phiephierun_coin_100";
	/** Virtual Currencies **/
	
	public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
		"Coins",										// name
		"I am coin currency",												// description
		COIN_CURRENCY_ITEM_ID							// item id
		);
	
	
	/** Virtual Currency Packs **/
	public static VirtualCurrencyPack COIN_ONE = new VirtualCurrencyPack(
		"1 coin",                                   // name
		"only 1 coin",                       // description
		"coin1",                                   // item id
		1,												// number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 0)
		);

	public static VirtualCurrencyPack COIN_PACK = new VirtualCurrencyPack(
		"100 coins",                                   // name
		"Test refund of an item",                       // description
		"coins100",                                   // item id
		100,												// number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(COIN_PACK100_PRODUCT_ID, 0.99)
		);
	
	public static VirtualCurrencyPack TENMUFF_PACK = new VirtualCurrencyPack(
		"10 Muffins",                                   // name
		"Test refund of an item",                       // description
		"muffins_10",                                   // item id
		10,												// number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(TENMUFF_PACK_PRODUCT_ID, 0.99)
		);
	
	public static VirtualCurrencyPack FIFTYMUFF_PACK = new VirtualCurrencyPack(
		"50 Muffins",                                   // name
		"Test cancellation of an item",                 // description
		"muffins_50",                                   // item id
		50,                                             // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(FIFTYMUFF_PACK_PRODUCT_ID, 1.99)
		);
	
	public static VirtualCurrencyPack FOURHUNDMUFF_PACK = new VirtualCurrencyPack(
		"400 Muffins",                                  // name
		"Test purchase of an item",                 	// description
		"muffins_400",                                  // item id
		400,                                            // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(FOURHUNDMUFF_PACK_PRODUCT_ID, 4.99)
		);
	
	public static VirtualCurrencyPack THOUSANDMUFF_PACK = new VirtualCurrencyPack(
		"1000 Muffins",                                 // name
		"Test item unavailable",                 		// description
		"muffins_1000",                                 // item id
		1000,                                           // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(THOUSANDMUFF_PACK_PRODUCT_ID, 8.99)
		);
	
	/** Virtual Goods **/
	
	public static VirtualGood MUFFINCAKE_GOOD = new SingleUseVG(
		"Fruit Cake",                                       		// name
		"Customers buy a double portion on each purchase of this cake", // description
		"fruit_cake",                                       		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 225)); // the way this virtual good is purchased
	
	public static VirtualGood PAVLOVA_GOOD = new SingleUseVG(
		"Pavlova",                                         			// name
		"Gives customers a sugar rush and they call their friends", // description
		"pavlova",                                          		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 175)); // the way this virtual good is purchased
	
	public static VirtualGood CHOCLATECAKE_GOOD = new SingleUseVG(
		"Chocolate Cake",                                   		// name
		"A classic cake to maximize customer satisfaction",	 		// description
		"chocolate_cake",                                   		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 250)); // the way this virtual good is purchased
	
	
	public static VirtualGood CREAMCUP_GOOD = new SingleUseVG(
		"Cream Cup",                                        		// name
		"Increase bakery reputation with this original pastry",   	// description
		"cream_cup",                                        		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 50));  // the way this virtual good is purchased
	
	
	/** Virtual Categories **/
	// The muffin rush theme doesn't support categories, so we just put everything under a general category.
	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General", new List<string>(new string[] { MUFFINCAKE_ITEM_ID, PAVLOVA_ITEM_ID, CHOCLATECAKE_ITEM_ID, CREAMCUP_ITEM_ID })
		);
	
	
	/** LifeTimeVGs **/
	// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
	
	public static VirtualGood FREE_CHAR0_LTVG = new LifetimeVG(
		"Free Char 0", 													// name
		"Give to player for free!",				 							// description
		"freechar0",														// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 0));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_CHAR1_LTVG = new LifetimeVG(
		"Unlock Char 1", 													// name
		"You can Unlock CHAR 1!",				 							// description
		"unlockchar1",														// item id
		new PurchaseWithMarket(UNLOCK_CHAR1_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_CHAR2_LTVG = new LifetimeVG(
		"Unlock Char 2", 													// name
		"You can Unlock CHAR 2!",				 							// description
		"unlockchar2",														// item id
		new PurchaseWithMarket(UNLOCK_CHAR2_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_CHAR3_LTVG = new LifetimeVG(
		"Unlock Char 3", 													// name
		"You can Unlock CHAR 3!",				 							// description
		"unlockchar3",														// item id
		new PurchaseWithMarket(UNLOCK_CHAR3_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_CHAR4_LTVG = new LifetimeVG(
		"Unlock Char 4", 													// name
		"You can Unlock CHAR 4!",				 							// description
		"unlockchar4",														// item id
		new PurchaseWithMarket(UNLOCK_CHAR4_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static VirtualGood FREE_NOABILITY_LTVG = new LifetimeVG(
		"No ability", 													// name
		"No ability!",				 							// description
		"noability",														// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 0));	// the way this virtual good is purchased
	
	public static VirtualGood UNLOCK_ABILITY1_LTVG = new LifetimeVG(
		"Unlock ability 1", 													// name
		"You can Unlock ability 1!",				 							// description
		"unlockability1",														// item id
		new PurchaseWithMarket(UNLOCK_ABILITY1_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
	
	public static VirtualGood UNLOCK_ABILITY2_LTVG = new LifetimeVG(
		"Unlock ability 2", 													// name
		"You can Unlock ability 2!",				 							// description
		"unlockability2",														// item id
		new PurchaseWithMarket(UNLOCK_ABILITY2_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_ABILITY3_LTVG = new LifetimeVG(
		"Unlock ability 3", 													// name
		"You can Unlock ability 3!",				 							// description
		"unlockability3",														// item id
		new PurchaseWithMarket(UNLOCK_ABILITY3_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood UNLOCK_ABILITY4_LTVG = new LifetimeVG(
		"Unlock ability 4", 													// name
		"You can Unlock ability 4!",				 							// description
		"unlockability4",														// item id
		new PurchaseWithMarket(UNLOCK_ABILITY4_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

}

