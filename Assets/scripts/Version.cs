using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    [SerializeField] private TMP_Text myTextElement;

    void Start()
    {
        if (myTextElement == null)
        {
            Debug.LogError("Version: Missing TMP_Text reference.");
            enabled = false;
            return;
        }
        myTextElement.SetText(Application.unityVersion);
    }
}
