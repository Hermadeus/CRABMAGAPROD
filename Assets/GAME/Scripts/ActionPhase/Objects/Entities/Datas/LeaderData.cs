using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/LeaderData")]
    public class LeaderData : EntityData
    {
        public Sprite thumbnail;

        public Sprite thumbnailToken = default;
        public Sprite thumbnailTokenUlt = default;
        public Sprite thumbnailTokenInUlt = default;
        public Sprite thumbnailTokenNone = default;
    }
}