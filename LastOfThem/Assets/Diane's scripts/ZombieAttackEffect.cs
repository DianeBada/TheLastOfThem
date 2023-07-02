using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/ZombieAttackEffect")]
public sealed class ZombieAttackEffect : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    [Tooltip("Controls the intensity of the effect.")]
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

    [Tooltip("Controls the frequency of the red flashing effect.")]
    public float flashingFrequency = 5f;

    Material m_Material;
    float m_Timer;

    public bool IsActive() => m_Material != null && intensity.value > 0f;

    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    public override void Setup()
    {
        if (Shader.Find("Hidden/Shader/ZombieAttackEffect") != null)
            m_Material = new Material(Shader.Find("Hidden/Shader/Zombie Attack Effect"));
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        m_Timer += Time.deltaTime;
        float flashingFactor = Mathf.PingPong(m_Timer * flashingFrequency, 1f);

        m_Material.SetFloat("_Intensity", intensity.value * flashingFactor);
        cmd.Blit(source, destination, m_Material, 0);
    }

    public override void Cleanup() => CoreUtils.Destroy(m_Material);
}
