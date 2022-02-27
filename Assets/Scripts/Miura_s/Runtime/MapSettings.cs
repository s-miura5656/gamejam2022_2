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

        /// <summary>
        /// í“¬”ÍˆÍŒ´“_
        /// </summary>
        public Vector3 OriginPosition => new Vector3(mapOrigin.x, 0, mapOrigin.y);

        /// <summary>
        /// í“¬”ÍˆÍI“_
        /// </summary>
        public Vector3 EndPosition => OriginPosition + new Vector3(areaSize.x, 0, areaSize.y);

#if UNITY_EDITOR
        [MenuItem("GameJam/Create/MapSettings")]
        public static void CreateAsset()
        {
            var asset = CreateInstance<MapSettings>();

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/MapSettings.asset");

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
    }
#endif
}
