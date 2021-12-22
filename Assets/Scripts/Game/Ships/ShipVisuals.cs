using UnityEngine;

namespace Game.Ships
{
    public class ShipVisuals : MonoBehaviour
    {
        [SerializeField] private GameObject ShipVisual;
        private SpriteRenderer VisualsRenderer;

        private Material OutlineMaterial;
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
        private static readonly int IsEnabled = Shader.PropertyToID("_IsEnabled");

        private void Awake()
        {
            VisualsRenderer = ShipVisual.GetComponent<SpriteRenderer>();
            OutlineMaterial = VisualsRenderer.material;
        }

        public void SetImage(Sprite image)
        {
            VisualsRenderer.sprite = image;
        }
    
        public void SetOutlineColor(Color color)
        {
            OutlineMaterial.SetColor(OutlineColor, color);
        }

        public void ShowVisuals()
        {
            ShipVisual.SetActive(true);
        }

        public void ShowOutlines()
        {
            OutlineMaterial.SetInt(IsEnabled, 1);
        }
        
        public void HideOutlines()
        {
            OutlineMaterial.SetInt(IsEnabled, 0);
        }
    }
}