using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items = new List<GameObject>();

    private void OnDestroy()
    {
        int chance = Random.Range(1, 3);
        if (chance == 1)
        {
            int randomIndex = Random.Range(0, items.Count);
            Instantiate(items[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
