using UnityEngine;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    public GameObject[] pages;
    public Animator pageAnimator;
    public Button selanjutnyaButton;
    public Button mulaiSekarangButton;
    //public Animator dotAnimator;
    private int currentPageIndex = 0;

    void Start()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        if (pages.Length > 0)
        {
            pages[0].SetActive(true);
        }

        selanjutnyaButton.gameObject.SetActive(true);
        mulaiSekarangButton.gameObject.SetActive(false);
        selanjutnyaButton.onClick.AddListener(GoToNextPage);
    }

    public void GoToNextPage()
    {
        pages[currentPageIndex].SetActive(false);
        currentPageIndex++;

        if (currentPageIndex < pages.Length)
        {
            pages[currentPageIndex].SetActive(true);
            pageAnimator.SetTrigger("Next");
        }

        if (currentPageIndex == pages.Length - 1)
        {
            selanjutnyaButton.gameObject.SetActive(false);
            mulaiSekarangButton.gameObject.SetActive(true);
        }
    }
}