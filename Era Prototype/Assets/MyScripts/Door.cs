using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Door : MonoBehaviour
{
    public House parentHouse;
    public bool isOpen;

    void Start()
    {
        parentHouse = GetComponentInParent<House>();
        isOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Open");
        if (collision.CompareTag("Player"))
        {
            parentHouse.ToggleRoof(false);
            Debug.Log("Open");
            PlayerInteractor.staticInstance.currentDoor = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentHouse.ToggleRoof(true);
            PlayerInteractor.staticInstance.currentDoor = null;
        }
    }
}
