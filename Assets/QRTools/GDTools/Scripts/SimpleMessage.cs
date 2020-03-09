using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace QRTools.GDTools
{
    [CreateAssetMenu(fileName = "New Message", menuName = "QRTools/GDTools/Message", order = 1)]
    public class SimpleMessage : ScriptableObject
    {
#if UNITY_EDITOR

        [SerializeField, BoxGroup("Infos")] string from = default;
        [SerializeField, InlineButton("SetDate"), BoxGroup("Infos")] string date = default;
        [SerializeField, GUIColor("GetButtonColor"), BoxGroup("Infos")] Priority priority = default;        

        [SerializeField, BoxGroup("Message")] UnityEngine.Object Object = default;
        [SerializeField, BoxGroup("Message")] private int atLine = default;
        [SerializeField, TextArea(3, 5), BoxGroup("Message")] string message = default;

        [SerializeField] bool isRead = default;

        string SetDate()
        {
            return date = string.Format(DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString()); 
        }

        private Color GetButtonColor()
        {
            Color c = Color.white;

            switch (priority)
            {
                case Priority.URGENT:
                    c = Color.red;
                    break;
                case Priority.MOYEN:
                    c = Color.green;
                    break;
                case Priority.PAS_IMPORTANT:
                    c = Color.yellow;
                    break;
            }

            if (isRead)
                c = Color.black;

            return c;
        }

        enum Priority
        {
            URGENT,
            MOYEN,
            PAS_IMPORTANT
        }

#endif
    }
}
