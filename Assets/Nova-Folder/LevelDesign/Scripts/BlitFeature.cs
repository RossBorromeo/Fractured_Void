using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitFeature : ScriptableRendererFeature
{
    class BlitPass : ScriptableRenderPass
    {
        private Material blitMaterial;
        private RTHandle tempTexture;
        private RTHandle source;

        public BlitPass(Material material)
        {
            blitMaterial = material;
        }

        public void SetSource(RTHandle src)
        {
            source = src;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Create a temporary render texture
            tempTexture = RTHandles.Alloc(cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("BlitPass");

            if (blitMaterial == null || source == null || tempTexture == null)
            {
                Debug.LogError("BlitPass: Required resources are missing!");
                return;
            }

            // Corrected Blit usage with RTHandles
            Blitter.BlitCameraTexture(cmd, source, tempTexture, blitMaterial, 0);
            Blitter.BlitCameraTexture(cmd, tempTexture, source);


            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if (tempTexture != null)
            {
                RTHandles.Release(tempTexture);
                tempTexture = null;
            }
        }
    }

    public Material blitMaterial;
    private BlitPass blitPass;

    public override void Create()
    {
        blitPass = new BlitPass(blitMaterial);
        blitPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (blitMaterial != null)
        {
            blitPass.SetSource(renderer.cameraColorTargetHandle);
            renderer.EnqueuePass(blitPass);
        }
    }
}
