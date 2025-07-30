using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    public Slider slider;
    private Image Filler;

    // Use this for initialization
    private void Start()
    {
        Filler = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        Filler.fillAmount = slider.value;
    }
}