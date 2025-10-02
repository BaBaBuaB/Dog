using UnityEngine;
using System.Collections.Generic;

public class Searchable : MonoBehaviour, IInteractable
{
    [Header("Item Pool (ScriptableObjects or Prefabs)")]
    public List<GameObject> possibleItems;
    public Transform spawnPoint;

    [Header("Search Settings")]
    public int maxSearches = 1;
    public float chanceToSpawn = 1f;
    public bool allowDuplicates = true;

    private int currentSearchCount = 0;
    private List<GameObject> remainingItems;
    private GameObject spawnedItem = null;

    private void Awake()
    {
        if (!allowDuplicates)
            remainingItems = new List<GameObject>(possibleItems);
    }

    public void OnInteract(PlayerController player)
    {
        if (currentSearchCount >= maxSearches)
        {
            Debug.Log("No more searches allowed.");
            return;
        }

        currentSearchCount++;

        if (Random.value <= chanceToSpawn && possibleItems.Count > 0)
        {
            GameObject itemToSpawn = null;

            if (allowDuplicates)
            {
                int index = Random.Range(0, possibleItems.Count);
                itemToSpawn = possibleItems[index];
            }
            else
            {
                if (remainingItems.Count == 0)
                {
                    Debug.Log("No items left to spawn.");
                    return;
                }

                int index = Random.Range(0, remainingItems.Count);
                itemToSpawn = remainingItems[index];
                remainingItems.RemoveAt(index);
            }

            spawnedItem = Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
            Debug.Log($"Searched! Item spawned: {spawnedItem.name}");
        }
        else
        {
            Debug.Log("Searched! But found nothing.");
        }
    }
}