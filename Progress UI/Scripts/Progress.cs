using UnityEngine ;
using EasyUI.Helpers ;
using UnityEngine.Events;

/* -------------------------------
     Created by : Hamza Herbou
     hamza95herbou@gmail.com
   ------------------------------- */

namespace EasyUI.Progress {

   public enum ProgressColor {
      Default,
      Red,
      Purple,
      Magenta,
      Blue,
      Green,
      Yellow,
      Orange
   }

   public static class Progress {
      private static bool isActive = false ;
      public static bool IsActive {
         get {return isActive;}
         private set {isActive = value;}
      }
      
      public static UnityAction OnProgressShow;
      public static UnityAction OnProgressHide;

      public static bool __isLoaded = false ;

      private static ProgressUI progressUI ;

      private static void Prepare () {
         if (!__isLoaded) {
            GameObject instance = MonoBehaviour.Instantiate (Resources.Load<GameObject> ("ProgressUI")) ;
            instance.name = "[ PROGRESS UI ]" ;
            progressUI = instance.GetComponent <ProgressUI> () ;
            __isLoaded = true ;

         }
         isActive = true;
      }

      public static void Show (string title, ProgressColor color = ProgressColor.Default) {
         PerformShow(new ProgressData{
            title=title, 
            color=color
         });
      }

      public static void Show (string title, ProgressColor color, bool detailsEnabled) {
         PerformShow(new ProgressData{
            title=title, 
            color=color,
            detailsEnabled=true
         });
      }

      public static void SetProgressValue (float progress) {
         progressUI.SetProgressValue(progress);
      }

      public static void SetDetailsText (string text) {
         progressUI.SetDetailsText(text);
      }

      public static void SetTitleText (string text) {
         progressUI.SetTitleText(text);
      }

      private static void PerformShow(ProgressData data){
         if (AlreadyActive())
                     return;

         Prepare () ;
         progressUI.Init(data);
         if (OnProgressShow != null) 
            OnProgressShow.Invoke();
      }

      public static void Hide () {
         if (isActive){
            isActive = false;
            progressUI.Hide();
            if (OnProgressHide != null) 
               OnProgressHide.Invoke();
         }
      }

      private static bool AlreadyActive(){
         if (isActive){
            Debug.Log("Progress UI already active");
            return true;
         }
         return false;
      }
   
   }

}
