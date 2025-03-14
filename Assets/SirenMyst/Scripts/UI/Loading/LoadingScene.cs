using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SirenMyst
{
    public class LoadingScene : SirenMonoBehaviour
    {
        [SerializeField] protected Transform LoadingScreen;
        [SerializeField] protected Image LoadingBarFill;

        public virtual void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

            this.LoadingScreen.gameObject.SetActive(true);

            while (!operation.isDone)
            {
                float processVal = Mathf.Clamp01(operation.progress / .9f);
                this.LoadingBarFill.fillAmount = processVal;
                yield return null;
            }
        }
    }
}