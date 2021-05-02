using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUIPanel : MonoBehaviour
{
    public Image progressBar; 
    public TextMeshProUGUI interactionText;

    public void SetInteractionText(string interactingText)
    {
        interactionText.SetText(interactingText);
    }

    public void UpdateProgressBar(float fillAmount)
    {
        progressBar.fillAmount = fillAmount;
    }

    public void ResetUI()
    {
        progressBar.fillAmount = 0f;
        interactionText.SetText("");
    }
}
