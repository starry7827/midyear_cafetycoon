using UnityEngine;

public class TrayLayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform tray;
    public Canvas canvas;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tray == null || canvas == null)
            return;
        
        if (tray.position.y > ((tray.position.x/3) - .25))
            canvas.sortingOrder = 1;
        else 
            canvas.sortingOrder = 9;
    }
}
