using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.Player.Crafting
{
    public enum InventoryItem
    {
        None = 0,
        TestItem = 1 << 0,
    }
    public class Inventory : MonoBehaviour
    {
        public int inventoryBitField;

        #region INVENTORY_MANIPULATION
        public void AddItem(InventoryItem item)
        {
            inventoryBitField |= (int)item;
        }

        public void RemoveItem(InventoryItem item)
        {
            inventoryBitField &= ~(int)item;
        }

        public bool CheckForItem(InventoryItem item)
        {
            return (inventoryBitField & (int)item) == (int)item;
        }
        #endregion
    }
}
