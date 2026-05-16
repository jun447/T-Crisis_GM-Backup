using UnityEngine;
using TMPro;

public class Version : MonoBehaviour
{
    [SerializeField] private TMP_Text myTextElement;

    void Start()
    {
        myTextElement.SetText(Application.unityVersion);
    }

    void Update()
    {
        
    }
}
