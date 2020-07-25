using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ItemDatabaseObject itemDatabaseObject;
    public ChestType chestType;
    public Item[] items;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        items = new Item[(int)chestType];
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Open!");
            foreach (Collider collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            StartCoroutine(OpenChest());
        }
    }

    private Item GenerateItem() // To be refactored with a loot table percentage sheet.
    {
        int itemIndex = Random.Range(0, itemDatabaseObject.Items.Length);
        Item item = itemDatabaseObject.Items[itemIndex];
        if (item == null)
        {
            return null;
        }
        return item;
    }

    private void DropItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = GenerateItem();
            GameObject item = Instantiate(items[i].groundItem, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator OpenChest()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color spriteCol = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, 0f, 5 * Time.deltaTime));
            spriteRenderer.color = spriteCol;
            
            
            if (spriteRenderer.color.a <= 0.05f)
            {
                spriteCol = new Color(0, 0, 0, 0);
                spriteRenderer.color = spriteCol;
                DropItems();
                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    child.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    public enum ChestType
    {
        TrickChest,
        OneItem,
        TwoItem,
        ThreeItem,
        FourItem
    }
}
