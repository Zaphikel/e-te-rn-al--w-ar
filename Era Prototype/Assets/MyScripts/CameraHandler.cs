using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Camera mainCamera;
    Transform player;
    public float depthLevel;

    public float followSpeed = 0.7f;
    
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        depthLevel = mainCamera.depth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        transform.position = new Vector3(Vector2.Lerp(transform.position, player.position, followSpeed).x, Vector2.Lerp(transform.position, player.position, followSpeed).y, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "House-See-Through")
        {
            StartCoroutine(FadeObjectOut(collision.GetComponent<SpriteRenderer>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "House-See-Through")
        {
            StartCoroutine(FadeObjectIn(collision.GetComponent<SpriteRenderer>()));
        }
    }

    IEnumerator FadeObjectOut(SpriteRenderer spriteRenderer)
    {
        while (spriteRenderer.color.a > 0)
        {
            Color spriteCol = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, 0f, 5 * Time.deltaTime));
            spriteRenderer.color = spriteCol;


            if (spriteRenderer.color.a <= 0.05f)
            {
                spriteCol = new Color(0, 0, 0, 0);
                spriteRenderer.color = spriteCol;
                foreach (Transform child in GetComponentsInChildren<Transform>())
                {
                    child.gameObject.SetActive(false);
                }
            }
            yield return null;
        }
    }

    IEnumerator FadeObjectIn(SpriteRenderer spriteRenderer)
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
        while (spriteRenderer.color.a < 1)
        {
            Color spriteCol = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(spriteRenderer.color.a, 1f, 5 * Time.deltaTime));
            spriteRenderer.color = spriteCol;


            if (spriteRenderer.color.a >= 0.85f)
            {
                spriteCol = new Color(0, 0, 0, 1);
                spriteRenderer.color = spriteCol;
            }
            yield return null;
        }
    }

}
