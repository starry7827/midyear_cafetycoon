using UnityEngine;

public class FoodCabinet : MonoBehaviour
{
    public Transform player;
    public KeyCode one = KeyCode.Alpha1;
    public KeyCode two = KeyCode.Alpha2;
    public KeyCode three = KeyCode.Alpha3;
    public KeyCode res = KeyCode.R;
    public Transform boxCenter;
    public float boxWidth;
    public float boxHeight;
    public GameObject glow;
    public GameObject crossaint;
    public GameObject muffin;
    public GameObject cookie;
    private int[] foodCounts = new int[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < foodCounts.Length; i++)
        {
            foodCounts[i] = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = PlayerManager.Instance;
        if (pm == null)
        {
            Debug.Log("The PlayerManager is missing!");
        }
        if (player == null || boxCenter == null) return;

        Vector2 p = player.position;
        Vector2 c = boxCenter.position;
        float halfW = boxWidth / 2f;
        float halfH = boxHeight / 2f;

        bool playerInBox = p.x >= c.x - halfW && p.x <= c.x + halfW &&
                           p.y >= c.y - halfH && p.y <= c.y + halfH;

        if (!playerInBox || pm.currentFoodInHand != Food.NoFood)
        {
            if (glow.activeSelf)
                glow.SetActive(false);
            return;
        }
        if (!glow.activeSelf)
            glow.SetActive(true);

        if (Input.GetKeyDown(one) && foodCounts[0] > 0)
        {
            foodCounts[0]--;
            pm.currentFoodInHand = Food.Muffin;
            Debug.Log("Picked up Muffin!");
            if (foodCounts[0] == 0)
                muffin.SetActive(false);
        }
        else if (Input.GetKeyDown(two) && foodCounts[1] > 0)
        {
            foodCounts[1]--;
            pm.currentFoodInHand = Food.Crossaint;
            Debug.Log("Picked up Crossaint!");
            if (foodCounts[1] == 0)
                crossaint.SetActive(false);
        }
        else if (Input.GetKeyDown(three) && foodCounts[2] > 0)
        {
            foodCounts[2]--;
            pm.currentFoodInHand = Food.Cookie;
            Debug.Log("Picked up Cookie!");
            if (foodCounts[2] == 0)
                cookie.SetActive(false);
        }
        else if (Input.GetKeyDown(res))
        {
            if (pm.money < 25)
            {
                Debug.Log("YOU'RE BROKE HAAHAHHA GET MORE MONEY YOU NEED $25 TO RESTOCK");
            }
            else restock();
        }

    }

    private void restock()
    {
        PlayerManager pm = PlayerManager.Instance;
        for (int i = 0; i < foodCounts.Length; i++)
        {
            foodCounts[i] += 3;
        }
        pm.money -= 25;
        if (!crossaint.activeSelf) crossaint.SetActive(true);
        if (!muffin.activeSelf) muffin.SetActive(true);
        if (!cookie.activeSelf) cookie.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        if (boxCenter == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCenter.position, new Vector3(boxWidth, boxHeight, 0.1f));
    }
}
