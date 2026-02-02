using UnityEngine;
using UnityEngine.UI;

namespace UAFaar.Core
{
    public class GridScaler : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private RectTransform parent;

        public void Configure(int cardCount)
        {
            int columns = Mathf.CeilToInt(Mathf.Sqrt(cardCount));
            int rows = Mathf.CeilToInt((float)cardCount / columns);

            float width = parent.rect.width;
            float height = parent.rect.height;

            float cellWidth = width / columns;
            float cellHeight = height / rows;

            float cellSize = Mathf.Min(cellWidth, cellHeight) * 0.75f;

            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = columns;
            grid.cellSize = new Vector2(cellSize, cellSize);
        }
    }
}
