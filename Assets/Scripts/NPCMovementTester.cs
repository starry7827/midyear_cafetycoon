using UnityEngine;
using System.Collections.Generic;

public class NPCMovementTester : MonoBehaviour
{
    public NPCMovement npc1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<Vector2> testPath = new List<Vector2>();

        Vector2 start = npc1.transform.position;

        testPath.Add(start + new Vector2(15, 15));
        testPath.Add(start + new Vector2(30, 0));
        testPath.Add(start + new Vector2(15, -15));
        testPath.Add(start);

        npc1.setPath(testPath);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
