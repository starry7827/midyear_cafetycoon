using UnityEngine;
using System.Collections.Generic;


public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public float spawnInterval = 20f; // this can be changed eventually to something that increases as time goes on or something random or we can make it change after one spawns i just made it this for testing
    public Vector2 spawnPos = new Vector2(103f, -38f);
    public Vector2 frontPos = new Vector2(-1f, 6f);
    public float spacing = 25f; // this can also be changed its how far inbetween the npcs
    private float timer = 0f;
    public int maxNPCS = 5;
    private List<GameObject> available;
    private List<NPCMovement> line = new List<NPCMovement>();
    public Transform player;
    public float interactRange = 15f;



    void Start()
    {
        available = new List<GameObject>(npcPrefabs);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            spawnNPC();
        }
    }

    void spawnNPC()
    {
        if (line.Count >= maxNPCS)
            return;


        if (available.Count == 0)
            return;

        int npcI = Random.Range(0, available.Count);
        GameObject chosenPrefab = available[npcI];
        available.RemoveAt(npcI);

        GameObject npc = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);

        NPCMovement npcMove = npc.GetComponent<NPCMovement>();

        line.Add(npcMove);
        updateLinePos();
    }

    public void updateLinePos()
    {
        Vector2 lineDir = (spawnPos - frontPos).normalized;

        for (int i = 0; i < line.Count; i++)
        {
            Vector2 point = frontPos + lineDir * spacing * i;
            List<Vector2> path = new List<Vector2>();
            path.Add(point);
            line[i].setPath(path);
        }
    }

    public bool npcReady()
    {
        if (line.Count == 0) return false;

        NPCMovement frontNPC = line[0];
        return Vector2.Distance(frontNPC.transform.position, frontPos) < 0.2f;
    }

    public void takeNPCOrder()
    {
        if (!npcReady()) return;

        NPCMovement orderPlaced = line[0];
        line.RemoveAt(0);

        NPCOrder order = orderPlaced.GetComponent<NPCOrder>();
        Debug.Log("Order taken: " + order.drink + " + " + order.food);

        Vector2 afterOrderPos = new Vector2(30f, 0f);
        orderPlaced.setPath(new List<Vector2> { afterOrderPos });

        updateLinePos();
    }
}
