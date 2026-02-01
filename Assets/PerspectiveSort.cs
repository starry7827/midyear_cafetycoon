using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    public Transform sortPoint; // optional override
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

        if (y > ((x/3) - 8.25)) {
            rend.sortingOrder = -1;
        } else {
            rend.sortingOrder = 1;
        }
        if (x <= -83 && transform.position.y <= -2) {
            rend.sortingOrder = 1;
        }
        if (x >= -61 && transform.position.y >= 30 && x <= -41.5 && transform.position.y <= 38.5) {
            rend.sortingOrder = -2;
        }

        // rend.sortingOrder = Mathf.RoundToInt(-y * 100);
    }
}
