using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameJam.Miura
{
    public class MapSettings : ScriptableObject
    {
        [SerializeField]
        private Vector2 mapOrigin;

        [SerializeField]
        private Vector2 areaSize;

        public Vector3 OriginPosition => new Vector3(mapOrigin.x, 0, mapOrigin.y);

        public Vector3 EndPosition => OriginPosition + new Vector3(areaSize.x, 0, areaSize.y);
    }

    public class CreateAsset
    {
#if UNITY_EDITOR
        [MenuItem("GameJam/Create/MapSettings")]
        public static void CreateMapSettings()
        {
            var asset = ScriptableObject.CreateInstance<MapSettings>();

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/MapSettings.asset");

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }

        [MenuItem("GameJam/Create/EnemySettings")]
        public static void CreateEnemysSettings()
        {
            var asset = ScriptableObject.CreateInstance<EnemySettings>();

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/EnemySettings.asset");

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
#endif
    }
}


