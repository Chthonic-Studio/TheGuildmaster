using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance { get; private set; }

    public List<Store> activeStores = new List<Store>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterStore(Store store)
    {
        if (!activeStores.Contains(store))
        {
            activeStores.Add(store);
        }
    }

    public void UnregisterStore(Store store)
    {
        if (activeStores.Contains(store))
        {
            activeStores.Remove(store);
        }
    }

    public Store GetRandomStore(Store.StoreType storeType)
    {
        List<Store> storesOfType = activeStores.FindAll(store => store.storeType == storeType);
        if (storesOfType.Count > 0)
        {
            return storesOfType[Random.Range(0, storesOfType.Count)];
        }
        return null;
    }
}