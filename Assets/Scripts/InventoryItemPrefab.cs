using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemPrefab : MonoBehaviour
{
    public InventoryItem item;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PickUp(PlayerCharacter character)
    {
        character.AddInventoryItem(item, 1);
        Destroy(gameObject);
    }
}
