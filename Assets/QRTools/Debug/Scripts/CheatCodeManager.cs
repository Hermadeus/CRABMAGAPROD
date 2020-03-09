using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QRTools.Variables;

namespace QRTools.Debugging
{
    public class CheatCodeManager : MonoBehaviour
    {
        public DictionaryStringActionVariable cheatCodeDictionary = default;

        [SerializeField]
        private KeyCode keyCodeOne = default;
        [SerializeField]
        private TMP_InputField inputField = default;
        [SerializeField]
        private Animator animator = default;
        [SerializeField]
        private bool cursorOption = false;
        [SerializeField]
        private Image image = default;
        [SerializeField]
        private TMP_Text title = default;
        [SerializeField]
        private Color
            findCodeColor = default,
            notFindCodeColor = default;

        private bool isOpen = false;

        public string Code
        {
            get => inputField.text;
        }

        private void Start()
        {
            inputField.onValueChanged.AddListener(delegate { VerifyCode(inputField.text); });
            inputField.onEndEdit.AddListener(ValidateCode);
        }

        private void Update()
        {
            OpenOrCloseCheatCodeMenu();
        }

        void OpenOrCloseCheatCodeMenu()
        {
            if (Input.GetKeyDown(keyCodeOne))
            {
                isOpen = !isOpen;
                animator.SetBool("isOpen", isOpen);

                if (cursorOption)
                    Cursor.visible = isOpen;
            }
        }

        public bool VerifyCode(string value)
        {
            if (cheatCodeDictionary.ActionExist(value))
            {
                image.color = findCodeColor;
                return true;
            }
            else
            {
                image.color = notFindCodeColor;
                return false;
            }
        }

        public void ValidateCode(string value)
        {
            if (VerifyCode(value))
                cheatCodeDictionary.PlayAction(value);
            else
                StartCoroutine(ErrorMessage());
        }

        IEnumerator ErrorMessage()
        {
            animator.SetTrigger("error");
            title.text = "ERROR !";
            yield return new WaitForSeconds(1f);
            title.text = "CHEATCODE MENU";
            yield break;
        }        
    }
}
