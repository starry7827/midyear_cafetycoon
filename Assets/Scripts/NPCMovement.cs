using UnityEngine;
using System.Collections.Generic;   

public class NPCMovement : MonoBehaviour
{
    public float speed = 15f;
    public Queue<Vector2> paths = new Queue<Vector2>();
    private Vector2 currentPath;
    private Vector3 lastPos;
    public SpriteRenderer npc;
    public bool hasPath = false;
    public Animator animator;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npc = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPath)
        {
            if (paths.Count > 0){
                currentPath = paths.Dequeue();
                hasPath = true;
            }
            else
                return;
        }
        lastPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, currentPath, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentPath) < 0.1f)
            hasPath = false;

        animator.SetFloat("MoveX", (transform.position-lastPos).x);
        animator.SetBool("Moving", hasPath);
    }

    public void setPath(List<Vector2> points)
    {
        paths.Clear();
        foreach (var v in points)
            paths.Enqueue(v);

        hasPath = false;
    }

    public void addPoint(Vector2 point)
    {
        paths.Enqueue(point);
        
    }
}
