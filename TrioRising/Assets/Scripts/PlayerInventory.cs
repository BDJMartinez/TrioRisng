using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    public List<BuffPickUps> items = new List<BuffPickUps>();
    public int maxItems;

    public bool AddItem(BuffPickUps item)
    {
        if(items.Count < maxItems)
        {
            items.Add(item);
            return true;
        }
        Debug.Log("Inventory is full!");
            return false;
    }

    public void RemoveItem(BuffPickUps item)
    {
        items.Remove(item);
    }

    public void Clear()
    {
        items.Clear();
    }








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
