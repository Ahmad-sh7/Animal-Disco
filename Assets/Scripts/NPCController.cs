using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Sprite dogeSprite;
    private string currentInput = "";
    private bool isDoge = false, isNinja = false, isMario = false, isSquidGame = false, isDancing = false, isPolice = false;
    Dictionary<GameObject, Sprite> npcs;
    DiscoLight[] discoLights;
    GameObject player;
    private float moveDistance = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        npcs = new Dictionary<GameObject, Sprite>();
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            npcs[npc] = npc.GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(PerformDance());
    }

    private void Update()
    {
        GetUserInput();
        if (isMario)
            DeleteMario();
        if (isSquidGame)
            RemoveDancingNPCWhileSquidGame();
        if (isPolice)
            Police();
    }

    IEnumerator PerformDance()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 6)); // Wait random number of seconds between  [2 and 5]
            int danceNumber = Random.Range(1, 3); // Get random number 1 or 2

            switch (danceNumber) {
                case 1:
                    isDancing = true;
                    StartCoroutine(DanceMoveController.RotateDance(transform, this));
                    break;
                case 2:
                    isDancing = true;
                    StartCoroutine(DanceMoveController.ScallingDance(transform, this));
                    break;
            }
            yield return new WaitForSeconds(0.5f); // Wait until the dance is finish
        }
    }

    void GetUserInput()
    {
        if (Input.anyKeyDown)
        {
            string input = Input.inputString;

            if (input.Length == 0 || input[0] == ' ' || !char.IsLetter(input[0])) return;

            currentInput += input.ToLower();

            if (currentInput.EndsWith("ninja") || currentInput.EndsWith("doge") || currentInput.EndsWith("squidgame") || currentInput.EndsWith("mario") || currentInput.EndsWith("police"))
            {
                int code = 
                    currentInput.EndsWith("ninja") ? 1 :
                    currentInput.EndsWith("doge") ? 2  :
                    currentInput.EndsWith("mario") ? 3 :
                    currentInput.EndsWith("squidgame") ? 4 :
                    currentInput.EndsWith("police") ? 5 : -1;
                switch (code)
                {
                    case 1:
                        isNinja = isNinja ? false : true;
                        NinjaFunction();
                        break;
                    case 2:
                        isDoge = isDoge ? false : true;
                        DogeFunction();
                        break;
                    case 3:
                        isMario = isMario ? false : true;
                        MarioFunction();
                        break;
                    case 4:
                        isSquidGame = true;
                        SquidGame();
                        break;
                    case 5:
                        isPolice = isPolice ? false : true;
                        // Police();
                        break;
                }
                currentInput = "";
            }
        }
    }
    void DeleteMario()
    {
        foreach (GameObject npc in npcs.Keys)
            if (Mathf.Abs(player.transform.position.x - npc.transform.position.x) < 2f && Mathf.Abs(player.transform.position.y - npc.transform.position.y) < 2f)
            {
                npcs.Remove(npc);
                Destroy(npc.gameObject);
                break;
            }
    }
    void MarioFunction()
    {
        if (isMario)
            PlayerScript.IncreaseScale(player.transform);
        else
            PlayerScript.DecreaseScale(player.transform);
    }
    void DogeFunction()
    {
        foreach (GameObject npc in npcs.Keys)
        {
            SpriteRenderer npcSpriteRenderer = npc.GetComponent<SpriteRenderer>();
            if (isDoge)
                npcSpriteRenderer.sprite = dogeSprite;
            else
                npcSpriteRenderer.sprite = npcs[npc];
        }
    }

    void NinjaFunction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (isNinja)
        {
            PlayerScript.ChangeSpeed(2.5f);
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            PlayerScript.ChangeSpeed(5f);
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void SquidGame()
    {
        PlayerScript player = FindObjectOfType<PlayerScript>();
        player.SetIsSquidGame(true);

        discoLights = FindObjectsOfType<DiscoLight>();
        foreach (DiscoLight discoLight in discoLights)
        {
            discoLight.GetComponent<SpriteRenderer>().color = new Color(0, 1f, 0, 1f);
            discoLight.StartSquidGame();
        }

    }

    void Police()
    {
        foreach (GameObject npc in npcs.Keys)
        {
            Vector3 direction = npc.transform.position - player.transform.position;
            float distance = direction.magnitude;

            if (distance < moveDistance)
            {
                float force = 10f * (1f - distance / moveDistance);
                npc.transform.position += direction.normalized * force * Time.deltaTime;
            }
        }     
    }

    void RemoveDancingNPCWhileSquidGame()
    {
        if (discoLights[0].IsRed() && isDancing)
            Invoke("DestroyGameObject", 1f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public bool IsDancing()
    {
        return isDancing;
    }

    public void SetIsDancing(bool dancing)
    {
        isDancing = dancing;
    }

    public bool IsSquidGame()
    {
        return isSquidGame;
    }

}
