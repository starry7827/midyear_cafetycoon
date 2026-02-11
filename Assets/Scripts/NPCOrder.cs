using UnityEngine;

public class NPCOrder : MonoBehaviour
{
    public Drink drink;
    public Food food;


    void Start()
    {
        drink = (Drink)Random.Range(0, System.Enum.GetValues(typeof(Drink)).Length);
        food = (Food)Random.Range(0, System.Enum.GetValues(typeof(Food)).Length);

        Debug.Log("NPC wants: " + drink + " + " + food);
    }

    void Update()
    {

    }
}
