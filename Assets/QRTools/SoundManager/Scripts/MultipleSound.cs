using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Audio
{
    [CreateAssetMenu(fileName = "New MultipleSounds", menuName = "QRTools/Audio/MultipleSounds", order = 4)]
    public class MultipleSound : BaseSound
    {
        [Tooltip("List of clip")]
        [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
        public List<AudioClip> Clips
        {
            get => clips;
            set
            {
                clips = value;
            }
        }
    }
}
