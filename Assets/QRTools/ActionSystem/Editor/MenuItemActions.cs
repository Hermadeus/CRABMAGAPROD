using UnityEditor;
using UnityEngine;

using System;

using QRTools.Events;

namespace QRTools.Actions
{
    public class MenuItemActions : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/QRTools/Actions/Colliders/Trigger - Box", false, 1)]
        static void CreateTriggerBox()
        {
            CreateTrigger("On Trigger (Box)", typeCollider.BOX, typeof(ActionHook_Trigger3D));
        }

        [MenuItem("GameObject/QRTools/Actions/Colliders/Trigger - Capsule", false, 2)]
        static void CreateTriggerCapsule()
        {
            CreateTrigger("On Trigger (Capsule)", typeCollider.CAPSULE, typeof(ActionHook_Trigger3D));
        }

        [MenuItem("GameObject/QRTools/Actions/Colliders/Trigger - Square", false, 3)]
        static void CreateTriggerSquare()
        {
            CreateTrigger("On Trigger 2D (Square)", typeCollider.SQUARE, typeof(ActionHook_Trigger2D), true);
        }

        [MenuItem("GameObject/QRTools/Actions/Colliders/Trigger - Capsule2D", false, 4)]
        static void CreateTriggerCapsule2D()
        {
            CreateTrigger("On Trigger 2D (Capsule2D)", typeCollider.CAPSULE2D, typeof(ActionHook_Trigger2D), true);
        }

        [MenuItem("GameObject/QRTools/Events/Colliders/Trigger - Box", false, 1)]
        static void Create_TriggerBox()
        {
            CreateTrigger("On Trigger (Box)", typeCollider.BOX, typeof(Event_Trigger3D));
        }

        [MenuItem("GameObject/QRTools/Events/Colliders/Trigger - Capsule", false, 2)]
        static void Create_TriggerCapsule()
        {
            CreateTrigger("On Trigger (Capsule)", typeCollider.CAPSULE, typeof(Event_Trigger3D));
        }

        [MenuItem("GameObject/QRTools/Events/Colliders/Trigger - Square", false, 3)]
        static void Create_TriggerSquare()
        {
            CreateTrigger("On Trigger 2D (Square)", typeCollider.SQUARE, typeof(Event_Trigger2D), true);
        }

        [MenuItem("GameObject/QRTools/Events/Colliders/Trigger - Capsule2D", false, 4)]
        static void Create_TriggerCapsule2D()
        {
            CreateTrigger("On Trigger 2D (Capsule2D)", typeCollider.CAPSULE2D, typeof(Event_Trigger2D), true);
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/On Awake", false, 0)]
        static void CreateAwakeHook()
        {
            CreateGameObject("On Awake Hook").AddComponent<ActionHook_Awake>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/On Start", false, 1)]
        static void CreateStartHook()
        {
            CreateGameObject("On Start Hook").AddComponent<ActionHook_Start>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/On Update", false, 10)]
        static void CreateUpdateHook()
        {
            CreateGameObject("On Update Hook").AddComponent<ActionHook_Update>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/On FixedUpdate", false, 11)]
        static void CreateFixedUpdateHook()
        {
            CreateGameObject("On FixedUpdate Hook").AddComponent<ActionHook_FixedUpdate>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/On Enable", false, 3)]
        static void CreateOnEnableHook()
        {
            CreateGameObject("On OnEnable Hook").AddComponent<ActionHook_OnEnable>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/Action Hook", false, 15)]
        static void CreateOnAllHook()
        {
            CreateGameObject("Action Hook").AddComponent<ActionHook>();
        }

        [MenuItem("GameObject/QRTools/Actions/Action_Hook/Action Hook With Tick", false, 20)]
        static void CreateOnAllHookWithTick()
        {
            CreateGameObject("Action Hook (with tick)").AddComponent<ActionHook_Tick>();
        }

        static GameObject CreateGameObject(string _name)
        {
            GameObject go = new GameObject();
            go.name = _name;
            go.transform.position = Vector3.zero;
            Selection.activeObject = go;
            return go;
        }

        static void CreateTrigger(string name, typeCollider colliderType, Type action, bool is2D = false)
        {
            GameObject go = new GameObject();
            go.transform.position = Vector3.zero;
            go.name = name;
            Selection.activeObject = go;

            switch (colliderType)
            {
                case typeCollider.BOX:
                    BoxCollider b = go.AddComponent<BoxCollider>();
                    b.isTrigger = true;
                    break;
                case typeCollider.CAPSULE:
                    CapsuleCollider c = go.AddComponent<CapsuleCollider>();
                    c.isTrigger = true;
                    break;
                case typeCollider.SQUARE:
                    BoxCollider2D b2 = go.AddComponent<BoxCollider2D>();
                    b2.isTrigger = true;
                    break;
                case typeCollider.CAPSULE2D:
                    CapsuleCollider2D c2 = go.AddComponent<CapsuleCollider2D>();
                    c2.isTrigger = true;
                    break;
            }

            go.AddComponent(action);

            if (is2D)
            {
                Rigidbody2D rb2d = go.AddComponent<Rigidbody2D>();
                rb2d.gravityScale = 0f;
            }
            else
            {
                Rigidbody rb = go.AddComponent<Rigidbody>();
                rb.useGravity = false;
            }
        }

        public enum typeCollider
        {
            BOX,
            CAPSULE,
            SQUARE,
            CAPSULE2D
        }
#endif
    }
}