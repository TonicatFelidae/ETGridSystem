using ET.SupportKit;
using ET.GridSystem;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET.GridSystem
{
    public class LayerRenderer
    {
        public Dictionary<LayerSystemLayerType, LayerRendererData> dat = new();
        public void Add(GameObject go, LayerSystemLayerType layerSystemLayerType)
        {
            if (!dat.ContainsKey(layerSystemLayerType))
            {
                dat.Add(layerSystemLayerType, new LayerRendererData());
                if (layerSystemLayerType == LayerSystemLayerType.Roof) dat[layerSystemLayerType].targetColor.Set_a(0); // ONLY TRUE IF BEGINING CAMERA LESSER THAN 7
            }
            //if (go.GetComponent<LayerRendererItemBase>() != null) return; // already have component
            //Component seeker
            if (go.GetComponent<SpriteRenderer>())
            {
                if (go.GetComponent<LayerSpriteRendererItem>()) return;
                go.AddComponent<LayerSpriteRendererItem>();
            }
            else if (go.GetComponent<TilemapRenderer>())
            {
                if (go.GetComponent<LayerTilemapRendererItem>()) return;
                go.AddComponent<LayerTilemapRendererItem>();
            }
            // LayerSpriteRendererItem - already added
            //
            if (go.GetComponent<LayerRendererItemBase>() == null) return;  // no component
            // Initiation
            go.GetComponent<LayerRendererItemBase>().Init(this, layerSystemLayerType);
            go.GetComponent<LayerRendererItemBase>().SetColor(dat[layerSystemLayerType].targetColor);
            dat[layerSystemLayerType].renderers.Add(go.GetComponent<LayerRendererItemBase>());
        }
        public void Remove(GameObject go, LayerSystemLayerType layerSystemLayerType, float oriAlpha)
        {
            if (!dat.ContainsKey(layerSystemLayerType))
            {
                dat.Add(layerSystemLayerType, new LayerRendererData());
            }
            LayerRendererItemBase itemRendererControl = go.GetComponent<LayerRendererItemBase>();
            if (!itemRendererControl) return;// no component
            itemRendererControl.SetColorAlpha(oriAlpha);
            dat[layerSystemLayerType].renderers.Remove(itemRendererControl);
            GameObject.Destroy(itemRendererControl);
            //if (go.GetComponent<LayerRendererItemBase>()) Debug.LogError("Can't Remove LayerRendererItemBase");
        }
        public void CheckAndRemoveLayerControl(GameObject go, float oriAlpha)
        {
            if (go.GetComponent<LayerRendererItemBase>())
            {
                LayerRendererItemBase itemRendererControl = go.GetComponent<LayerRendererItemBase>();
                foreach (var itemList in dat)
                {
                    if (itemList.Value.renderers.Contains(itemRendererControl))
                    {
                        go.GetComponent<SpriteRenderer>().color.Set_a(oriAlpha);
                        itemList.Value.renderers.Remove(itemRendererControl);
                        GameObject.Destroy(itemRendererControl);
                        if (go.GetComponent<SpriteRenderer>()) Debug.LogError("Can't Remove LayerRendererItemBase");
                        break;
                    }
                }
            }
        }

        #region SP
        public void Add(GameObject go, GridLayer gridLayer)
        {
            switch (gridLayer)
            {
                case GridLayer.Floor:
                    break;
                case GridLayer.OnFloor:
                    break;
                case GridLayer.NormalGeo:
                    break;
                case GridLayer.Normal:
                    break;
                case GridLayer.OnNormal:
                    break;

                case GridLayer.Roof:
                case GridLayer.OnRoof:
                    Add(go, LayerSystemLayerType.Roof);
                    break;
                default:
                    break;
            }
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Total layer renderer: {dat.Count}");
            foreach (var item in dat)
            {
                sb.AppendLine($"{item.Key}: {item.Value.renderers.Count}");
            }
            return sb.ToString();
        }
    }
    public class LayerRendererData
    {
        private Color _DoColor = Color.white;
        public Color DoColor
        {
            get => _DoColor;
            set
            {
                _DoColor = value;
                for (int i = 0; i < renderers.Count; i++)
                {
                    renderers[i].SetColor(_DoColor);
                }
            }
        }
        public Color targetColor = Color.white;
        public List<LayerRendererItemBase> renderers = new();
    }
}
