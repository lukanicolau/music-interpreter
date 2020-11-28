using UnityEngine;
using TMPro;
public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI notesGUI;
    public TextMeshProUGUI detailsGUI;

    private void Start()
    {
        HideInfo();
    }

    public void DisplayInfo ()
    {
        notesGUI.alpha = 1;
        detailsGUI.alpha = 1;
    }
    public void HideInfo()
    {
        notesGUI.alpha = 0;
        detailsGUI.alpha = 0;
    }

}
