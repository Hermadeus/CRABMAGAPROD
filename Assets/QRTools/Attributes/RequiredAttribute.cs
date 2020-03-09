using UnityEngine;
using UnityEditor;
using System;

namespace QRTools
{
    public class RequiredAttribute : PropertyAttribute
    {
#if UNITY_EDITOR
        public string message;
        public MessageType messageType = MessageType.Error;

        public Type type;

        public RequiredAttribute()
        {
            message = "Warn, the field is empty !";
            messageType = MessageType.Error;
            type = GetType();
        }

        public RequiredAttribute(string _message)
        {
            message = _message;
        }

        public RequiredAttribute(string _message, MessageType _messageType) : base()
        {
            message = _message;
            messageType = _messageType;
        }

        public RequiredAttribute(MessageType _messageType) : base()
        {
            messageType =_messageType;
        }
#endif
    }
}
