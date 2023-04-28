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
        StartChangeColor();
    }

    void ChangeColor()
    {
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1f);
    }

    public void StopChangeColor()
    {
        CancelInvoke("ChangeColor");
    }

    public void StartChangeColor()
    {
        InvokeRepeating("ChangeColor", changeDelay, changeDelay);
    }

    public void StartSquidGame()
    {
        StopChangeColor();
        InvokeRepeating("RedGreenGame", 2f, 2f);
    }

    void RedGreenGame()
    {
        if (Mathf.Abs(spriteRenderer.color.r - 1) < Mathf.Epsilon)
            spriteRenderer.color = new Color(0, 1f, 0, 1f);
        else if (Mathf.Abs(spriteRenderer.color.g - 1) < Mathf.Epsilon)
            spriteRenderer.color = new Color(1f, 0, 0, 1f);
    }

    public bool IsRed()
    {
        if (Mathf.Abs(spriteRenderer.color.r - 1) < Mathf.Epsilon)
            return true;
        return false;
    }
}
