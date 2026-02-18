using UnityEngine;

public class NPCOrder : MonoBehaviour
{
    public Drink drink;
    public Food food;
    public bool orderTaken = false;


    void Start()
    {
        drink = (Drink)Random.Range(0, 3/*System.Enum.GetValues(typeof(Drink)).Length*/);
        food = (Food)3 /*(Food)Random.Range(0, System.Enum.GetValues(typeof(Food)).Length)*/;

        if (drink == Drink.NoDrink && food == Food.NoFood)
        {
            if (Random.Range(0, 2) == 0)
                drink = (Drink)Random.Range(0, System.Enum.GetValues(typeof(Drink)).Length - 1);
            else
                food = (Food)Random.Range(0, System.Enum.GetValues(typeof(Food)).Length - 1);
        }
        Debug.Log("NPC wants: " + drink + " + " + food);
    }

    void Update()
    {

    }
}
