using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private PlayerScript playerScript;
    private bool follow = true;

    private void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    void Update()
    {
        if (follow)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    public void StopFollow()
    {
        follow = false;
    }

    public void StartFollow()
    {
        follow = true;
    }
}
