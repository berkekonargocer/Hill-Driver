    using TMPro;
    using UnityEngine;

    namespace Nojumpo
{
    public class ChangeTextFromAnimation : MonoBehaviour
    {
        // -------------------------------- FIELDS ---------------------------------
        [SerializeField] TextMeshProUGUI textToChange;
        

        // ------------------------- CUSTOM PRIVATE METHODS ------------------------
        void ChangeText(string text) {
            textToChange.text = text;
        }
    }
}