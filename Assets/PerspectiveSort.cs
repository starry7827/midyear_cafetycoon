using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    public Transform sortPoint;
    Transform trans;
    SpriteRenderer rend;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        float y;
        float x = transform.position.x;

        if (sortPoint != null)
            y = sortPoint.position.y;
        else
            y = rend.bounds.min.y;

        rend.sortingOrder = Mathf.RoundToInt(-y * 100);

        if (y > ((x/3) - .25)) {
            rend.sortingOrder = -1;
        } else {
            rend.sortingOrder = 8;
        }
        if (x <= -75 && transform.position.y <= -13.72) {
            rend.sortingOrder = 3;
        } 
        if (x >= -61 && transform.position.y >= 30 && x <= -41.5 && transform.position.y <= 38.5) {
            rend.sortingOrder = -6;
        }

        
    }
}
