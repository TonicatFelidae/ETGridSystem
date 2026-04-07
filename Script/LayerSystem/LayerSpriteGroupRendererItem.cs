using ET.SupportKit;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    public class LayerSpriteGroupRendererItem : LayerRendererItemBase
    {
        public SpriteRenderer[] _renderer;
        public override void Init(LayerRenderer layerRenderer, LayerSystemLayerType layerType)
        {
            base.Init(layerRenderer, layerType);
        }
        public override void SetColor(Color color)
        {
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].color = color;
            }
        }
        public override void SetColorAlpha(float alpha)
        {
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].color.Set_a(alpha);
            }
        }
    }
}