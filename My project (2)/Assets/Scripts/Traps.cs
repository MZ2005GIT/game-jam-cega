using UnityEngine;

public class Traps : MonoBehaviour
{
    public GameObject trap;

    void Start()
    {
        InvokeRepeating(nameof(ToggleTrap), 0f, 2f);
    }

    void ToggleTrap()
    {
        bool isActive = !trap.activeSelf;
        trap.SetActive(isActive);
        GetComponent<Collider2D>().enabled = isActive;
    }
}