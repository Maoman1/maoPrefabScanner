using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using TriangleNet;
using UnityEngine;
using Object = UnityEngine.Object;

namespace maoPrefabScanner
{
    [BepInPlugin("mao.prefabscanner", "Prefab Scanner", "1.2")]
    public class Plugin : BasePlugin
    {
        public static ManualLogSource Log;

        public override void Load()
        {
            Log = base.Log;
            Log.LogInfo("[Prefab Scanner] Plugin loaded. Press F7 in-world to brute-force scan components.");

            ClassInjector.RegisterTypeInIl2Cpp<AltarEnhancer>();

            var go = new GameObject("AltarEnhancer");
            go.AddComponent<AltarEnhancer>();
            Object.DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    public class AltarEnhancer : MonoBehaviour
    {
        private ManualLogSource log;

        public AltarEnhancer(System.IntPtr ptr) : base(ptr)
        {
            log = Plugin.Log;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F7))
            {
                Vector3 playerPos = GameObject.FindWithTag("Player")?.transform.position ?? Vector3.zero;
                float radius = 5f;
                int found = 0;

                var components = Object.FindObjectsOfType<Component>();
                foreach (var c in components)
                {
                    try
                    {
                        var go = c.gameObject;
                        if (Vector3.Distance(go.transform.position, playerPos) > radius)
                            continue;

                        var name = go.name;
                        var typeName = c.GetIl2CppType().FullName;
                        if (!string.IsNullOrEmpty(typeName))
                        {
                            log.LogInfo($"[Prefab Scanner] GameObject: {name} - Component: {typeName}");
                            found++;
                        }
                    }
                    catch { }
                }
                log.LogInfo($"[Prefab Scanner] Scan complete. Found {found} nearby objects within {radius}m.");
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                foreach (var go in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (go.name.ToLower().Contains("jotun"))
                    {
                        log.LogInfo("=== [Prefab Scanner] Found Jotun-related GameObject ===");
                        PrintAllGameObjectInfo(go);
                    }
                }
            }
        }

        void PrintAllGameObjectInfo(GameObject go, string indent = "")
        {
            log.LogInfo($"{indent}[Prefab Scanner]GameObject: {go.name}");
            foreach (var comp in go.GetComponents<Component>())
            {
                try
                {
                    log.LogInfo($"{indent}  [Prefab Scanner]Component: {comp.GetIl2CppType().FullName}");
                }
                catch (System.Exception e)
                {
                    log.LogInfo($"{indent}  [Prefab Scanner][Error: " + e.Message + "]");
                }
            }
            for (int i = 0; i < go.transform.childCount; i++)
            {
                PrintAllGameObjectInfo(go.transform.GetChild(i).gameObject, indent + "  ");
            }
        }
    }
}
