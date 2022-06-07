using UnityEngine;

namespace BAC3
{
    public static class ObjectUtils
    {
        public static GameObject FindObject<T>(string name, bool byParent = false, bool getLastIndex = false, int index = 0) where T : Component
        {
            Plugin.Logger.Debug($"Finding GameObject of name {name}.");
            T[] trs = Resources.FindObjectsOfTypeAll<T>();
            Plugin.Logger.Debug($"There are {trs.Length} GameObjects.");

            for (int i = 0; i < trs.Length; i++)
            {
                if (i != trs.Length - 1 && getLastIndex && index == 0) continue;
                if (i < index && getLastIndex && index != 0) continue;

                GameObject go = trs[i].gameObject;

                if (byParent)
                {
                    go = go.transform.parent.gameObject;
                }

                if (go == null) continue;

                if (go.name == name)
                {
                    Plugin.Logger.Debug("Found GameObject.");
                    return go;
                }
            }

            Plugin.Logger.Debug("Unable to find GameObject.");
            return null;
        }
    }
}
