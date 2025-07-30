using UnityEngine;
using UnityEngine.UI;

public class Togglescript : MonoBehaviour
{
    public GameObject Slider;

    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }


    private void Update()
    {
        if (toggle.isOn)
            Slider.SetActive(false);
        else
            Slider.SetActive(true);
    }
}