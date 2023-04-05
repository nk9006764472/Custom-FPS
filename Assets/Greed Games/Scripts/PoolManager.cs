using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<int, List<GameObject>> pools = new();

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetItem(GameObject gameObject)
    {
        if (!pools.ContainsKey(gameObject.GetInstanceID()))
        {
            pools.Add(gameObject.GetInstanceID(), new List<GameObject>());
        }


        if (pools[gameObject.GetInstanceID()].Count > 0)
        {
            GameObject item = pools[gameObject.GetInstanceID()][0];
            pools[gameObject.GetInstanceID()].RemoveAt(0);
            item.SetActive(true);
            return item;
        }
        else
        {
            GameObject item = Instantiate(gameObject);
            item.SetActive(true);
            return item;
        }
    }

    public void ReturnItem(GameObject gameObject, int poolKey)
    {
        pools[poolKey].Add(gameObject);
        gameObject.SetActive(false);
    }
}
