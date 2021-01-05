using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static LoadItemController;

public class LateralMenuController : MonoBehaviour
{

    public bool openMenu = false, showMenu = true;
    public float itemContainerMargin = 10.0f;
    public float animationVelocity = 0.3f;

    private Image backgroundImageMenu;
    private GameObject btnUserProfile;
    private RectTransform rectTransformContainer, rectTransformItemContainer,rectTransformBtnCloseMenu;
    private Image[] bgItems;
    private float rectTransformXPosition;
    private Vector2 sizeDeltaLeftMenu;
    private Button btnOpenLateralMenu;
    private LoadItemController loadItemController;

    private FooterBarController footerBarController;
    // Start is called before the first frame update
    void Start()
    {
        backgroundImageMenu = this.transform.Find("MenuExpanded").gameObject.transform.Find("bg").gameObject.GetComponent<Image>();
        btnUserProfile = this.transform.Find("MenuExpanded").gameObject.transform.Find("ItemContainer").gameObject.transform.Find("UserProfile").gameObject;
        rectTransformContainer = this.transform.Find("MenuExpanded").gameObject.transform.Find("ItemContainer").gameObject.transform.Find("ListMenuItems").gameObject.transform.Find("Viewer").gameObject.transform.Find("Container").gameObject.GetComponent<RectTransform>();
        rectTransformItemContainer = this.transform.Find("MenuExpanded").gameObject.transform.Find("ItemContainer").gameObject.GetComponent<RectTransform>();
        rectTransformBtnCloseMenu = this.transform.Find("MenuExpanded").gameObject.transform.Find("BtnCloseMenuMask").gameObject.transform.Find("BtnCloseMenu").gameObject.GetComponent<RectTransform>();

        sizeDeltaLeftMenu = new Vector2(GameObject.Find("Canvas").gameObject.GetComponent<RectTransform>().rect.width, GameObject.Find("Canvas").gameObject.GetComponent<RectTransform>().rect.height);

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDeltaLeftMenu.x - 20.0f, sizeDeltaLeftMenu.y - 20.0f);

        footerBarController = GameObject.Find("Canvas").gameObject.transform.Find("ControlsContainer").gameObject.transform.Find("FooterBar").gameObject.GetComponent<FooterBarController>();

        btnOpenLateralMenu = this.transform.Find("Btn").gameObject.GetComponent<Button>();

        loadItemController = GameObject.Find("Scripts").GetComponent<LoadItemController>();

        Init();
    }

    private void Init()
    {
        Color colorBG = backgroundImageMenu.color;
        colorBG.a = 0.0f;

        backgroundImageMenu.color = colorBG;
        
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(130.0f, 400);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, 0.0f);

        rectTransformBtnCloseMenu.anchoredPosition = new Vector2(0.0f,rectTransformBtnCloseMenu.anchoredPosition.y);


        foreach (Transform c in rectTransformContainer.gameObject.transform)
        {
            c.Find("bg").gameObject.SetActive(false);
            c.Find("Title").gameObject.SetActive(false);

            c.gameObject.GetComponent<Button>().onClick.AddListener(()=> {
                loadItemController.SetCategoryId(c.gameObject.transform.GetSiblingIndex());
            });
            Debug.Log(c.gameObject.transform.GetSiblingIndex());
        }

        

        rectTransformItemContainer.offsetMin = new Vector2(0.0f, rectTransformItemContainer.offsetMin.y);
        btnUserProfile.SetActive(false);
        openMenu = true;

        btnOpenLateralMenu.onClick.AddListener(()=> {
            OpenCloseLateralMenu();
        });

        rectTransformBtnCloseMenu.gameObject.GetComponent<Button>().onClick.AddListener(()=> {
            OpenCloseLateralMenu();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideShowLateralMenu()
    {
        if (showMenu)
        {
            this.GetComponent<RectTransform>().DOAnchorPosX(-120.0f, animationVelocity).OnComplete(()=>{
                this.gameObject.SetActive(false);
            });

            showMenu = false;

        }
        else
        {
            this.gameObject.SetActive(true);
            this.GetComponent<RectTransform>().DOAnchorPosX(10.0f, animationVelocity);
            showMenu = true;
        }
        
    }

    public void OpenCloseLateralMenu()
    {
        if (openMenu)
        {
            btnOpenLateralMenu.gameObject.SetActive(false);

            footerBarController.HideShowFooter();

            backgroundImageMenu.DOFade(1.0f, animationVelocity);

            this.GetComponent<RectTransform>().DOSizeDelta(new Vector2(sizeDeltaLeftMenu.x - 20.0f, sizeDeltaLeftMenu.y - 20.0f), animationVelocity);
            this.GetComponent<RectTransform>().DOAnchorPosX(10.0f, 0.3f).OnComplete(() =>
            {
                rectTransformBtnCloseMenu.DOAnchorPosX(35.0f, animationVelocity);
            });

            foreach (Transform c in rectTransformContainer.gameObject.transform)
            {
                c.Find("bg").gameObject.SetActive(true);
                c.Find("Title").gameObject.SetActive(true);
            }

            rectTransformItemContainer.offsetMin = new Vector2(itemContainerMargin, rectTransformItemContainer.offsetMin.y);

            btnUserProfile.SetActive(true);
            openMenu = false;
        }
        else
        {
            btnOpenLateralMenu.gameObject.SetActive(true);

            footerBarController.HideShowFooter();

            backgroundImageMenu.DOFade(0.0f, 0.3f);

            this.GetComponent<RectTransform>().DOSizeDelta(new Vector2(130.0f, 400), animationVelocity);
            this.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0.0f, 0.0f), animationVelocity);
            rectTransformBtnCloseMenu.DOAnchorPosX(0.0f, animationVelocity);

            foreach (Transform c in rectTransformContainer.gameObject.transform)
            {
                c.Find("bg").gameObject.SetActive(false);
                c.Find("Title").gameObject.SetActive(false);
            }

            rectTransformItemContainer.offsetMin = new Vector2(0.0f, rectTransformItemContainer.offsetMin.y);
            btnUserProfile.SetActive(false);
            openMenu = true;
        }
    }
}
