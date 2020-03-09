using UnityEngine;

namespace QRTools.GDTools
{
    [CreateAssetMenu(fileName = "New Task", menuName = "QRTools/GDTools/Task", order = 0)]
    public class FeatureListOption : ScriptableObject
    {
        public string featureName;
        public TypeObject typeObject;
        public AssignedTeamMember teamMember;

        [Tooltip("check to complete")]
        public bool isDone;
        public string date;
        [TextArea(3, 15)] public string description;
    }

    public enum TypeObject
    {
        CODE,
        GRAPH,
        SOUND,
        LD
    }

    public enum AssignedTeamMember
    {
        JEAN,
        LOUIS,
        PAUL
    }
}
