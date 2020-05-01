using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class LineSystem : MonoBehaviour
    {
        public Line[] lines;

        public void SelectLine(int line)
        {
            if (line < -2 || line > 2)
                return;

            for (int i = 0; i < lines.Length; i++)
            {
                if (line == lines[i].transform.position.x)
                    lines[i].Select();
                else
                    lines[i].Unselect();
            }
        }

        public void DeselectAll()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].Unselect();
            }
        }
    }
}