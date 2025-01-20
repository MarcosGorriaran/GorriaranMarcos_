using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HPSliderTracker : MonoBehaviour
{
    [SerializeField]
    HPManager trackedEntity;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = trackedEntity.GetMaxHp();
        trackedEntity.onHPChange += UpdateSlider;
    }
    private void UpdateSlider()
    {
        slider.value = trackedEntity.GetMaxHp() - trackedEntity.GetHp();
    }
}
