using UnityEngine;

namespace UAFaar.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelData[] levels;

        public LevelData GetLevel(int index)
        {
            return levels[Mathf.Clamp(index, 0, levels.Length - 1)];
        }
    }
}
