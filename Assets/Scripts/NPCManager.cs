using UnityEngine;
using System.Collections.Generic;


public class NPCManager : MonoBehaviour
{
    public GameObject npc1Prefab;
    public float spawnInterval = 30f; // this can be changed eventually to something that increases as time goes on or something random or we can make it change after one spawns i just made it this for testing
    public Vector2 spawnPos = new Vector2(103f, -54f);
    public Vector2 frontPos = new Vector2(-1f, 6f);
    public float spacing = 10f; // this can also be changed its how far inbetween the npcs
    private float timer = 0f;
    private List<NPCMovement> line = new List<NPCMovement>();

    void Start()
    {

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
        GameObject npc = Instantiate(npc1Prefab, spawnPos, Quaternion.identity);
        NPCMovement npcMove = npc.GetComponent<NPCMovement>();

        line.Add(npcMove);


        Vector2 lineDir = (spawnPos - frontPos).normalized;

        for (int i = 0; i < line.Count; i++)
        {
            Vector2 point = frontPos + lineDir * spacing * i;
            List<Vector2> path = new List<Vector2>();
            path.Add(point);
            line[i].setPath(path);
        }
    }
}
