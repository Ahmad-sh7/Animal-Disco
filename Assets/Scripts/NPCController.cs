using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] Sprite dogeSprite;
    private string currentInput = "";
    private bool isDoge = false, isNinja = false, isMario = false;
    Dictionary<GameObject, Sprite> npcs;

    void Start()
    {
        npcs = new Dictionary<GameObject, Sprite>();
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            npcs[npc] = npc.GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(PerformDance());
    }

    private void Update()
    {
        GetUserInput();
        DeleteMario();
    }

    IEnumerator PerformDance()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5)); // Wait random number of seconds between  [2 and 5]
            int danceNumber = Random.Range(1, 3); // Get random number 1 or 2

            switch (danceNumber) {
                case 1:
                    StartCoroutine(DanceMoveController.RotateDance(transform));
                    break;
                case 2:
                    StartCoroutine(DanceMoveController.ScallingDance(transform));
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

            if (currentInput.EndsWith("ninja") || currentInput.EndsWith("doge") || currentInput.EndsWith("squidgame") || currentInput.EndsWith("mario"))
            {
                int code = currentInput.EndsWith("ninja") ? 1 : currentInput.EndsWith("doge") ? 2  : currentInput.EndsWith("mario") ? 3 : 4;
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
                        break;
                }
                currentInput = "";
            }
        }
    }
    void DeleteMario()
    {
        if (isMario)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            foreach (GameObject npc in npcs.Keys)
            {
                SpriteRenderer npcSpriteRenderer = npc.GetComponent<SpriteRenderer>();
                if (Mathf.Abs(player.transform.position.x - npc.transform.position.x) < 2f && Mathf.Abs(player.transform.position.y - npc.transform.position.y) < 2f)
                {
                    npcs.Remove(npc);
                    Destroy(npc.gameObject);
                }
            }
        }
    }
    void MarioFunction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        
        if (isMario)
        {
            PlayerScript.IncreaseScale(player.transform);
        }
        else
        {
            PlayerScript.DecreaseScale(player.transform);

        }

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
}
