using UnityEngine;

public class NPCOrderPopUpController : MonoBehaviour
{
    public GameObject popupPrefab;
    public Vector3 offset = new Vector3(0f, 26f, 0f);

    private GameObject popupInstance;

    void Start()
    {
        popupInstance = Instantiate(popupPrefab, transform.position + offset, Quaternion.identity);
        popupInstance.transform.SetParent(transform); 
        popupInstance.transform.localPosition = offset;

        popupInstance.SetActive(false); 
    }

    public void ShowTakeOrder()
    {
        popupInstance.SetActive(true);
    }

    public void HidePopup()
    {
        popupInstance.SetActive(false);
    }
}
