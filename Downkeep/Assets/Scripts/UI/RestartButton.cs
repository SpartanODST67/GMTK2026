using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Transition transition;

    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Time.timeScale = 1;
        transition.Slide(new Vector3(0, -1800, 0), Vector3.zero, () => SceneManager.LoadScene("SampleScene"));
    }
}
