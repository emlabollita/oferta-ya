using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FooterBarController : MonoBehaviour
{
    public float animationVelocity = 0.3f;
    private bool showFooterBar = true;
    private float footerYposition, footerHeight;
    // Start is called before the first frame update
    void Start()
    {
        footerYposition = this.GetComponent<RectTransform>().position.y;
        footerHeight = this.GetComponent<RectTransform>().rect.height;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void HideShowFooter(){
        if(showFooterBar){
            this.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0.0f, - (footerHeight - footerYposition)), animationVelocity);
            Debug.Log("footerHeight: " + -(footerHeight - footerYposition));
            showFooterBar = false;
        }else{
            this.GetComponent<RectTransform>().DOMoveY(footerYposition, animationVelocity);
            showFooterBar = true;
        }
    }
    
}
