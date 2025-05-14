using UnityEngine;
using UnityEngine.UI;

public class UI_Ignition : MonoBehaviour
{
    [SerializeField] Image keyImage;
    [SerializeField] Engine playerCarEngine;
    [Space]
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite keyInSprite;
    [SerializeField] Sprite ignitionSprite;


    bool keyInserted;
    bool ignition;

    public void OnClick()
    {
        if (!keyInserted)
        {
            keyImage.sprite = keyInSprite;
            keyInserted = true;
            return;
        }

        if (!ignition)
        {
            keyImage.sprite = ignitionSprite;
            ignition = true;
        }
        else
        {
            keyImage.sprite = keyInSprite;
            ignition = false;
        }

        if (playerCarEngine != null)
        {
            playerCarEngine.ToggleEngineRunning();
        }
    }
}
