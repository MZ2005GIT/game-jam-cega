using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public GameObject[] Keys;
    private List<GameObject> KeysObtained = new List<GameObject>();
    public GameObject[] Doors;
    public GameObject[] DoorsOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            KeysObtained.Add(collision.gameObject);
        }

        for (int i = 0; i < Keys.Length; i++)
        {
            if (collision.gameObject == Keys[i])
            {
                Doors[i].SetActive(false);
                DoorsOpened[i].SetActive(true);
            }
        }
    }
}
