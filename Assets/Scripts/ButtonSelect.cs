using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public Image img;

    void Start() {
        img = GetComponent<Image>();
    }

    public void selectHotLatte() {
        img.color = new Color32(108, 65, 42, 255);
    }

    public void resetHotLatte() {
        img.color = new Color32(171, 102, 68, 255);
    }
}
