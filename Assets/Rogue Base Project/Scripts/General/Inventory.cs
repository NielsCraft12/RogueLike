using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int selectedSlot = 0;
    [SerializeField]
    List<GameObject> selected = new List<GameObject>();
    [SerializeField]
    private int itemAmount = 0;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemAmountText;

    [SerializeField]
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            selected[i].SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {


        itemAmountText.text = itemAmount.ToString();
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput > 0f) // Scrolling up
        {
            selectedSlot++;
        }
        else if (scrollInput < 0f) // Scrolling down
        {
            selectedSlot--;
        }

        if (selectedSlot < 0)
        {
            selectedSlot = selected.Count - 1;
        }
        else if (selectedSlot >= selected.Count)
        {
            selectedSlot = 0;
        }

        for (int i = 0; i < selected.Count; i++)
        {
            if (i == selectedSlot)
            {
                selected[i].SetActive(true);
            }
            else
            {
                selected[i].SetActive(false);
            }
        }

        if (selectedSlot == 1)
        {
            player.GetComponent<PlayerControler>().isBowEquipped = true;
        }
        else
        {
            player.GetComponent<PlayerControler>().isBowEquipped = false;
        }
    }
}
