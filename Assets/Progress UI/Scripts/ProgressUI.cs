using UnityEngine ;
using System.Collections ;
using UnityEngine.UI ;
using EasyUI.Progress ;
using TMPro;

/* -------------------------------
     Created by : Hamza Herbou
     hamza95herbou@gmail.com
   ------------------------------- */

namespace EasyUI.Helpers {

   public class ProgressData {
      public string title;
      public bool detailsEnabled = false;
      public string details;
      public float progress;
      public ProgressColor color;
   }

   public class ProgressUI : MonoBehaviour {
      [Header ("UI References :")]
      [SerializeField] private CanvasGroup uiCanvasGroup ;
      [SerializeField] private Image uiOverlayImage;
      [SerializeField] private Image uiPopupImage;
      [SerializeField] private Image uiLoadingCircleImage;
      [SerializeField] private Animator loadingCircleAnimator;
      [SerializeField] private GameObject uiDetailsSection;
      [SerializeField] private TextMeshProUGUI uiTitleText;
      [SerializeField] private TextMeshProUGUI uiDetailsText;
      [SerializeField] private TextMeshProUGUI uiProgressText;
      [SerializeField] private Slider uiProgressSlider;
      [SerializeField] private Image uiProgressFillImage;

      [Space]
      [Header ("Progress UI Settings :")]
      [SerializeField] private Settings settings ;

      private ProgressData progressData = new ProgressData();

      private int IS_ROTATING_ANIM_PARAM;

      private void Awake () {
         uiCanvasGroup.alpha = 0f ;
         IS_ROTATING_ANIM_PARAM = Animator.StringToHash("IsRotating");
         UpdateTheme();
         SetLoadingCircleAnimation(false);
      }

      private void ResetProgressData(){
         progressData.title = "";
         progressData.detailsEnabled = false;
         progressData.details="";
         progressData.progress=0f;
         progressData.color=ProgressColor.Default;
      }

      public void Init (ProgressData data) {
         progressData = data;

         UpdateTheme();
         UpdateColors();

         SetTitleText(data.title);
         
         SetDetails(data.detailsEnabled);
         if (data.detailsEnabled){
            SetDetailsText("");
            SetProgressValue(0f);
         }

         Show () ;
      }


      private void Show () {
         Dismiss () ;
         SetLoadingCircleAnimation(true);
         StartCoroutine (Fade (0f, 1f,settings.fadeInDuration)) ;
      }

      private void UpdateTheme(){
         uiOverlayImage.color = settings.theme.OverlayColor;
         uiPopupImage.color = settings.theme.BackgroundColor;
      }
      
      private void SetDetails(bool enabled){
         uiDetailsSection.SetActive(enabled);
      }

      public void SetTitleText(string text){
         progressData.title = text;
         uiTitleText.text = text;
      }
      
      public void SetDetailsText(string text){
         progressData.details = text;
         uiDetailsText.text = text;
      }
      
      public void SetProgressValue(float progress){
         progressData.progress = progress;
         uiProgressSlider.value = progress;
         uiProgressText.text = string.Format("{0} %", Mathf.Clamp(Mathf.Floor(progress), 0f, 100f));
      }
      
      private void UpdateColors(){
         Color c = settings.theme.ProgressColors[(int)progressData.color].value;

         uiTitleText.color = settings.theme.TitleTextColor;
         uiLoadingCircleImage.color = c;
         uiProgressFillImage.color = c;
         uiDetailsText.color = settings.theme.DetailsTextColor;
         uiProgressText.color = settings.theme.DetailsTextColor;
      }

      public void Hide () {
         SetLoadingCircleAnimation(false);
         StartCoroutine (Fade (uiCanvasGroup.alpha, 0f,settings.fadeOutDuration)) ;
         ResetProgressData();
      }

      private IEnumerator Fade (float startAlpha, float endAlpha, float fadeDuration) {
         float startTime = Time.time ;
         float alpha = startAlpha ;

         if (fadeDuration > 0f) {
            //Anim start
            while (alpha != endAlpha) {
               alpha = Mathf.Lerp (startAlpha, endAlpha, (Time.time - startTime) / fadeDuration) ;
               uiCanvasGroup.alpha = alpha ;

               yield return null ;
            }
         }

         uiCanvasGroup.alpha = endAlpha ;
      }

      public void Dismiss () {
         StopAllCoroutines () ;
         SetLoadingCircleAnimation(false);
         uiCanvasGroup.alpha = 0f ;
      }

      

      private void SetLoadingCircleAnimation(bool animate){
         loadingCircleAnimator.SetBool(IS_ROTATING_ANIM_PARAM,animate);
      }



      private void OnDestroy () {
         EasyUI.Progress.Progress.__isLoaded = false ;
      }
   }

}
