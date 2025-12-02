using UnityEngine;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    public GameObject[] pages;
    public Animator pageAnimator;
    public Animator PanelAnimator;
    public Button selanjutnyaButton;
    public Button mulaiSekarangButton;
    private int currentPageIndex = 0;

    void Start()
    {
        selanjutnyaButton.gameObject.SetActive(true);
        mulaiSekarangButton.gameObject.SetActive(false);
        selanjutnyaButton.onClick.AddListener(GoToNextPage);
    }

    public void GoToNextPage()
    {
        currentPageIndex++;

        if (currentPageIndex < pages.Length)
        {
            pageAnimator.SetTrigger("Next");
            PanelAnimator.SetTrigger("Next");
        }

        if (currentPageIndex == pages.Length - 1)
        {
            selanjutnyaButton.gameObject.SetActive(false);
            mulaiSekarangButton.gameObject.SetActive(true);
        }
    }
}