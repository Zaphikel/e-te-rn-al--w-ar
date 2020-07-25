using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public SpriteRenderer roofSpriteRenderer;

    public GameObject roof;

    public bool isPlayerInHouse;

    public bool isHouseFadingIn;
    public bool isHouseFadingOut;

    private void Start()
    {
        isHouseFadingIn = false;
        isHouseFadingOut = false;
    }

    public void ToggleRoof(bool isTogglingIn)
    {
        if (!isPlayerInHouse)
        {
            if (isTogglingIn)
            {
                isHouseFadingOut = false;
                StartCoroutine(ToggleRoofIn());
                return;
            }

            isHouseFadingIn = false;
            StartCoroutine(ToggleRoofOut()); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInHouse = false;
        }
    }

    public IEnumerator ToggleRoofOut()
    {
        isHouseFadingOut = true;
        while (roofSpriteRenderer.color.a > 0 && isHouseFadingOut)
        {
            Color spriteCol = new Color(roofSpriteRenderer.color.r, roofSpriteRenderer.color.g, roofSpriteRenderer.color.b, Mathf.Lerp(roofSpriteRenderer.color.a, 0f, 5 * Time.deltaTime));
            roofSpriteRenderer.color = spriteCol;
            
            if (roofSpriteRenderer.color.a <= 0.05f)
            {
                spriteCol = new Color(1, 1, 1, 0);
                roofSpriteRenderer.color = spriteCol;
            }
            yield return null;
        }
        isHouseFadingOut = false;
    }

    public IEnumerator ToggleRoofIn()
    {
        isHouseFadingIn = true;
        while (roofSpriteRenderer.color.a < 1 && isHouseFadingIn)
        {
            Color spriteCol = new Color(roofSpriteRenderer.color.r, roofSpriteRenderer.color.g, roofSpriteRenderer.color.b, Mathf.Lerp(roofSpriteRenderer.color.a, 1f, 5 * Time.deltaTime));
            roofSpriteRenderer.color = spriteCol;
            
            if (roofSpriteRenderer.color.a >= 0.95f)
            {
                spriteCol = new Color(1, 1, 1, 1);
                roofSpriteRenderer.color = spriteCol;
            }
            yield return null;
        }
        isHouseFadingIn = false;
    }

}
