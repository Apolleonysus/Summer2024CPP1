using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Text lifeValueText;

    private void OnEnable()
    {
        // Subscribe to the OnLifeValueChanged event
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLifeValueChanged += UpdateLifeValueDisplay;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnLifeValueChanged event
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLifeValueChanged -= UpdateLifeValueDisplay;
        }
    }

    private void UpdateLifeValueDisplay(int newLifeValue)
    {
        if (lifeValueText != null)
        {
            lifeValueText.text = "Lives: " + newLifeValue.ToString();
        }
    }
}
