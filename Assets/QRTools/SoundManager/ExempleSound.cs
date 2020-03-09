using UnityEngine;
using QRTools.Audio;

public class ExempleSound : MonoBehaviour
{
    public PlaySound ps;
    public AudioClip clip;
    public AnimationCurve curve;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Play a sound
            ps.Play();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Change sound with a lerp transition
            ps.ChangeSound(clip, 1f, curve, 10f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Change value (ex : volume) with a lerp transition
            ps.SetValue(ps.Source.volume, 0f, curve, (x) => ps.SetVolume(ps.Source, x));
        }
    }
}
