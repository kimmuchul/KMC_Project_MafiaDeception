using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace NoF
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        [SerializeField]
        private bool m_DelayDuplicateRemoval;


        private static T s_Instance;

        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = (T)FindFirstObjectByType(typeof(T));

                    if (s_Instance == null)
                    {
                        SetupInstance();
                    }
                    else
                    {
                        string typeName = typeof(T).Name;

                        Debug.Log("[Singleton] " + typeName + " instance already created: " +
                                  s_Instance.gameObject.name);
                    }
                }

                return s_Instance;
            }
        }

        public virtual void Awake()
        {
            // For demo purposes, this flag can delay the removal of duplicates
            if (!m_DelayDuplicateRemoval)
                RemoveDuplicates();
        }

        //private void OnEnable()
        //{
        //    // Clear the single instance when unloading the current scene
        //    ScenesManager.sceneUnloaded += OnSceneUnloaded;
        //}

        //private void OnDisable()
        //{
        //    if (s_Instance == this as T)
        //    {
        //        ScenesManager.sceneUnloaded -= OnSceneUnloaded;
        //    }
        //}

        private static void SetupInstance()
        {
            // lazy instantiation
            s_Instance = (T)FindFirstObjectByType(typeof(T));

            if (s_Instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;

                s_Instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }

        public void RemoveDuplicates()
        {
            if (s_Instance == null)
            {
                s_Instance = this as T;
            }
            else if (s_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Event-handling method

        // Destroy singleton when unloading scene (for demo use only)
        private void OnSceneUnloaded(Scene scene)
        {
            if (s_Instance == this as T)
            {
                s_Instance = null;
            }
        }
    }
}