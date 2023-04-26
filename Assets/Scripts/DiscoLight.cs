using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    private float changeDelay = 1f;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    void Start()
    {
        InvokeRepeating("ChangeColor", changeDelay, changeDelay);
    }

    void ChangeColor()
    {
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1f);
    }
}
