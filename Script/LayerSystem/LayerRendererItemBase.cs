using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
namespace ET.GridSystem
{
    public class LayerRendererItemBase : MonoBehaviour
    {
        private LayerRenderer _layerRenderer;
        public LayerSystemLayerType LayerType => _layerType;
        private LayerSystemLayerType _layerType;
        public virtual void Init(LayerRenderer layerRenderer, LayerSystemLayerType layerType)
        {
            _layerType = layerType;
            _layerRenderer = layerRenderer;
        }
        public virtual void SetColor(Color color)
        {
        }
        public virtual void SetColorAlpha(float alpha)
        {
        }
        private void OnDestroy()
        {
            if (_layerRenderer != null) _layerRenderer.Remove(gameObject, LayerType, 1);
        }

    }
}