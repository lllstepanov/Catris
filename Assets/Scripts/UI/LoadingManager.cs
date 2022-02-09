using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatrisLoading {

    /// <summary>
    /// Loads scene by name
    /// </summary>
    internal class LoadingManager : MonoBehaviour
    {
        /// <summary>
        /// Name of the scene
        /// </summary>
        [SerializeField]
        private string sceneName = "";

        /// <summary>
        /// Loading operation that loads on background
        /// </summary>
        private AsyncOperation loadingSceneOperation;

        private void Start()
        {
            /// Start to load scene
            LoadScene();
            
            /// Assign GoToScene method to LoadingBar event
            LoadingBar.OnLoadingCompleted += GoToScene;
        }

        /// <summary>
        /// Loads scene on background
        /// </summary>
        private void LoadScene()
        {
            /// Assign loading process to local variable
            loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            
            /// Block switch of the scenes
            loadingSceneOperation.allowSceneActivation = false;
        }

        /// <summary>
        /// Activates switch between two scenes
        /// </summary>
        internal void GoToScene()
        {
            /// Allows to load scene conected to loadingSceneOperation
            loadingSceneOperation.allowSceneActivation = true;
        }
    }
}
