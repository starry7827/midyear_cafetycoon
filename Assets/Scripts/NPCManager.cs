using UnityEngine;
using System.Collections.Generic;


public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject[] npcPrefabs;
    public float spawnInterval; // this can be changed eventually to something that increases as time goes on or something random or we can make it change after one spawns i just made it this for testing
    public Vector2 spawnPos;
    public Vector2 frontPos;
    public float spacing; // this can also be changed its how far inbetween the npcs
    public float spacing2;
    private float timer = 0f;
    public int maxNPCS;
    private List<GameObject> available;
    private List<NPCMovement> line = new List<NPCMovement>();
    private List<NPCMovement> pickupLine = new List<NPCMovement>();
    private List<NPCMovement> inbetweenLines = new List<NPCMovement>();
    private List<NPCMovement> seated = new List<NPCMovement>();
    private Vector2 afterOrderPos = new Vector2(35f, 20f);

    public Vector2 firstPos;
    public Vector2 LastPos;

    public Transform player;
    public float interactRange;



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

        if (inbetweenLines.Count > 0)
        {
            for (int i = 0; i < inbetweenLines.Count; i++)
            {
                NPCMovement npci = inbetweenLines[i];
                if (Vector2.Distance(npci.transform.position, afterOrderPos) < 0.2f)
                {
                    if (!npci.canSit || seated.Count > 2 || Random.Range(0,2) == 0)
                    {
                        pickupLine.Add(npci);
                        updatePickupLinePos();
                    }
                    else
                    {
                        seated.Add(npci);
                        npci.findTable(seated.IndexOf(npci));
                    }
                    inbetweenLines.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void spawnNPC()
    {
        if (line.Count + pickupLine.Count + inbetweenLines.Count + seated.Count >= maxNPCS)
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

    public void updatePickupLinePos()
    {
        Vector2 lineDir = (LastPos - firstPos).normalized;

        for (int i = 0; i < pickupLine.Count; i++)
        {
            Vector2 point = firstPos + lineDir * spacing2 * i;
            List<Vector2> path = new List<Vector2>();
            path.Add(point);
            pickupLine[i].setPath(path);
        }
    }

    public bool npcReady()
    {
        if (line.Count <= 0) return false;

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
        order.orderTaken = true;

        Vector2 afterOrderPos = new Vector2(35f, 20f);
        inbetweenLines.Add(orderPlaced);
        orderPlaced.setPath(new List<Vector2> { afterOrderPos });
        updateLinePos();
    }

    public void DeliverToNPC(NPCOrder order)
    {
        PlayerManager pm = PlayerManager.Instance;
        if (!order.orderTaken)
            return;
        if (pm.currentDrinkInHand == order.drink)
        {
            Debug.Log("Order Delivered! Correct Drink: " + order.drink);
            pm.AddMoney(15); // we gotta change this to make it work depending on what the drink is but that should be easy since we can just access order.drink here
            pm.ClearInventory();

            NPCMovement move = order.GetComponent<NPCMovement>();
            if (pickupLine.Contains(move)){
                move.setPath(new List<Vector2> { new Vector2(103f, -28f) });
                pickupLine.RemoveAt(pickupLine.IndexOf(move));
                updatePickupLinePos();
                move.destroy = true;
            }
        }
        else
        {
            Debug.Log("Wrong item! This NPC wants: " + order.drink);
        }
    }

    public NPCMovement GetFrontNPC()
    {
        if (line.Count == 0) return null;
        return line[0];
    }
}