using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Window : MonoBehaviour
{
    public House parentHouse;

    void Start()
    {
        parentHouse = GetComponentInParent<House>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentHouse.ToggleRoof(false);
            Debug.Log("Open");
            PlayerInteractor.staticInstance.window = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            parentHouse.ToggleRoof(true);
            PlayerInteractor.staticInstance.window = null;
        }
    }
}
