using ET.SupportKit;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    public class LayerTilemapRendererItem : LayerRendererItemBase
    {
        //private TilemapRenderer _renderer;
        private Tilemap _tileMap;
        public override void Init(LayerRenderer layerRenderer, LayerSystemLayerType layerType)
        {
            base.Init(layerRenderer, layerType);
            //_renderer = GetComponent<TilemapRenderer>();
            _tileMap = GetComponent<Tilemap>();
        }
        public override void SetColorAlpha(float alpha)
        {
            _tileMap.color.Set_a(alpha);
        }
        public override void SetColor(Color color)
        {
            _tileMap.color = color;
        }
    }
}