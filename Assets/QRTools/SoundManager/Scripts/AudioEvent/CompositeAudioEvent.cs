using System;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

namespace QRTools.Audio
{
    [CreateAssetMenu(menuName = "QRTools/Audio/AudioEvents/Composite")]
    public class CompositeAudioEvent : AudioEvent
    {
        [Serializable]
        public struct CompositeEntry
        {
            public SimpleAudioEvent Event;
            public float Weight;
        }

        public CompositeEntry[] Entries;

        public override void Play(AudioSource source)
        {
            float totalWeight = 0;
            for (int i = 0; i < Entries.Length; i++)
                totalWeight += Entries[i].Weight;

            float pick = Random.Range(0, totalWeight);
            for(int i = 0; i < Entries.Length; i++)
            {
                if(pick > Entries[i].Weight)
                {
                    pick -= Entries[i].Weight;
                    continue;
                }

                Entries[i].Event.Play(source);
                return;
            }
        }

        public void PlaySound(AudioSource source, string _name)
        {
            for (int i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].Event.Name == _name)
                    Entries[i].Event.Play(source);
            }
        }
    }
}
