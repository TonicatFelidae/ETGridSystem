using ET.SupportKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    public class LayerSpriteRendererItem : LayerRendererItemBase
    {
        private SpriteRenderer _renderer;
        public override void Init(LayerRenderer layerRenderer, LayerSystemLayerType layerType)
        {
            base.Init(layerRenderer, layerType);
            _renderer = GetComponent<SpriteRenderer>();
        }
        public override void SetColor(Color color)
        {
            _renderer.color = color;
        }
        public override void SetColorAlpha(float alpha)
        {
            _renderer.color.Set_a(alpha);
        }
    }
}