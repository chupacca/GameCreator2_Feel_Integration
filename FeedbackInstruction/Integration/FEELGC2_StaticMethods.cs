using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using MoreMountains.Feedbacks;
using MoreMountains.Tools;

using FEELGC2_Enums;
using FEELGC2_CustomFeedbacks;
using FEELGC2_IntegrationObjects;

namespace FEELGC2_StaticMethods //FEELGC2_Integration
{ 

    // Useful Static Functions
    public static class StaticFEELGC2Methods
    {
        //////////////////////////////////////////////////////////////////////////////////////////////
        // Useful Static Async Functions
        //////////////////////////////////////////////////////////////////////////////////////////////

        /*
            * Asynchronous function to check if there are multiple GameObjects in a given tag.
            *
            * tagName [string]: Name of the tag we will be checking.
        */
        public static async Task<int> AsyncCheckIfMultipleObjectsInTag(string tagName)
        {
            // ... checking if there are more than 1 GameObject to a tag
            GameObject[] TargetObjectArray = GameObject.FindGameObjectsWithTag(tagName);
            if(TargetObjectArray.Length > 1)
            {
                Debug.Log("GC2_PlayFEELFeedback:Run() - More than 1 GameObject of selected Tag");
            }

            return await Task.FromResult(1);     
        }

        //////////////////////////////////////////////////////////////////////////////////////////////
        // Useful Static Functions (NOT ASYNC)
        //////////////////////////////////////////////////////////////////////////////////////////////

        /*  // This function is associated with `AddChosenFeedback()` !!!!!
            *  This method will get the corresponding `MMF_Feedback` based on the value of `searchLabel`.
            *
            *  chosenFeedback [ChosenFeedback]: the `enum ChosenFeedback` that will determine which feedback to return.
            *  feedbackLabel [string]: the string value that represents this feedback's `Label`.
            *  TargetObject [GameObject]: The Target GameObject that FEEL will add its stuff on.
            *
            *  Returns: A `MMF_Feedback` object (it will be NULL is an invalid `ChosenFeedback` is provided).
        */
        public static MMF_Feedback GetChosenFeedback(ChosenFeedback chosenFeedback, 
                                                     string feedbackLabel, 
                                                     GameObject targetObject)
        {
            MMF_Feedback mmfFeedback = null;
            switch (chosenFeedback)
            {
                //---------------------------//
                // Feedback (0) -------------//
                case ChosenFeedback.JumpAndScale:
                    
                    mmfFeedback = CustomFeedbacks.JumpAndScale(feedbackLabel, targetObject);

                    // I don't need to return `targetObject` since this change will apply
                    // to the original `GameObject` from whoever passed it
                    targetObject.transform.position = CustomFeedbacks.AdjustYAxis(targetObject);
                    break;

                //---------------------------//
                // Feedback (1) -------------/
                // TODO!!!!!!!!!!!!!!!!!!!!!!!!!!! UPDATE THIS!!!!!!!!
                case ChosenFeedback.ScaleHoizontally:

                    mmfFeedback = CustomFeedbacks.ScaleHoizontally(feedbackLabel, targetObject);
                    break;

                //---------------------------//
                // Feedback (2) -------------//
                // TODO!!!!!!!!!!!!!!!!!!!!!!!!!!! UPDATE THIS!!!!!!!!
                case ChosenFeedback.ScaleVertically:

                    mmfFeedback = CustomFeedbacks.ScaleVertically(feedbackLabel, targetObject);

                    // I don't need to return `targetObject` since this change will apply
                    // to the original `GameObject` from whoever passed it
                    targetObject.transform.position = CustomFeedbacks.AdjustYAxis(targetObject);

                    break;


                //---------------------------//
                // Feedback (3) -------------//
                case ChosenFeedback.RotateRight:

                    mmfFeedback = CustomFeedbacks.RotateRight(feedbackLabel, targetObject);
                    break;

                //---------------------------//
                // Feedback (4) -------------//
                case ChosenFeedback.RotateLeft:

                    mmfFeedback = CustomFeedbacks.RotateLeft(feedbackLabel, targetObject);
                    break;

                //---------------------------//
                // Feedback (5) -------------//
                case ChosenFeedback.SquashAndStretch:
                    
                    mmfFeedback = CustomFeedbacks.SquashAndStretch(feedbackLabel, targetObject);

                    // I don't need to return `targetObject` since this change will apply
                    // to the original `GameObject` from whoever passed it
                    targetObject.transform.position = CustomFeedbacks.AdjustYAxis(targetObject);
                    break;

                //---------------------------//
                // Feedback (6) -------------//
                // <to be entered when needed>


                //---------------------------//
                // Feedback (7) -------------//
                // <to be entered when needed>
                
                //---------------------------//
                // Feedback (15) -------------//
                // <to be entered when needed>
                case ChosenFeedback.CinemachineImpulse:
                    
                    mmfFeedback = CustomFeedbacks.CinemachineImpulse(feedbackLabel, targetObject);
                    break;

                //---------------------------//
                // ADD FEEL STUFF HERE 
            }

            return mmfFeedback;
        }

        // -------------------------------------------------------------------- //
        ////////// FUNCTIONS ASSOCIATED WITH `AddChosenFeedback()`: //////////////
        ////////////         That DO NOT involve ENUMS!            ///////////////  
        // -------------------------------------------------------------------- //

        /*
            * Check if searchLabel is NULL and print a debug message if it is NULL.
            *
            * searchLabel [string]: The string we're checking if it's NULL.
            * chosenFeedback [ChosenFeedback]: For potential debug logs.
            * whatMethodCalledThis [string]: The method that called this to help with debuggging.
            *
            * Returns: a boolean determining is `searchLabel` is NULL or not.
        */
        public static bool FeedbackLabelIsNull(string searchLabel, 
                                                ChosenFeedback chosenFeedback,
                                                string whatMethodCalledThis)
        {
            if(string.IsNullOrEmpty(searchLabel)) 
            {
                // Asynchronously log that `searchLabel` is Null or Empty
                #pragma warning disable 4014 // disabling warning since this function is not dependent on the async function
                Async_DebugLogWithFeedback(
                    "FEELGC2_Integration:FEELGC2_UsefulStaticMethods:"+ whatMethodCalledThis +"()<string searchLabel>",
                    chosenFeedback
                );
                #pragma warning restore 4014 // restoring this warning since other async methods may need this

                return true; // `searchLabel` is NULL
            }

            return false; //searchLabel is NOT NULL
        }

        /*
            *  Adds the chosen feedback it `chosenFeedback` is valid. If it's already added,
            *  then just return `mmfPlayer` as it was given.
            *
            *  TargetObject [GameObject]: The Target GameObject that FEEL will add its stuff on.
            *  feelgc2Component [FEELGC2_Components]: Object that has all the components.
            *  chosenFeedback [ChosenFeedback]: the `enum ChosenFeedback` that will determine which feedback to return.
            *
            *  Returns: `FEELGC2_Components` with the `chosenFeedback` added OR as 
            *           `feelgc2Components` was given.
        */
        public static FEELGC2_Components AddChosenFeedback(
                                                    GameObject targetObject, 
                                                    FEELGC2_Components feelgc2Components, 
                                                    ChosenFeedback chosenFeedback)
        {
            //Set the `MMF_Player` object 
            MMF_Player mmfPlayer = feelgc2Components.cMMFPlayer;

            ////////////////////////////////////////////////////
            // 1. Get the string value associated with the given `ChosenFeedback` 
            //    (will be NULL/Empty if NOT found)
            string searchLabel = chosenFeedback.ToString();//GetFeedbackLabel(chosenFeedback);
            //  Check if `searchLabel` is NULL/Empty
            if(FeedbackLabelIsNull(searchLabel, chosenFeedback, "AddChosenFeedback")) 
            {
                return feelgc2Components; // If `searchLabel` is NULL/Empty, 
                                         //   return the `FEELGC2_Components` as it was given
            }

            ////////////////////////////////////////////////////////////////////////////////
            /* This is no longer needed since we are going under the assumption 
                that there's only one custom feedback at a given time and that it
                will be removed after it is done playing.      
            // Commented this out cause this has a for loop which make time complexity O(n)

            // 2. Check if the chosen feedback already exists...
            if(GivenFeedbackExists(mmfPlayer, searchLabel, targetObject)) 
            {
                return feelgc2Components; // ...if the feedback ALREADY EXISTS, 
                                         //  return `FEELGC2_Components` was given
                                         //  (this is so we don't add a redundant feedback)
            } 
            */
            ////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////////////////////
            // 3. Get the corresponding `MMF_Feedback` based on the value of `chosenFeedback`
            MMF_Feedback selectedFeedback = GetChosenFeedback(chosenFeedback, searchLabel, targetObject);
            //     ... Add the `MMF_Feedback` if it is NOT NULL
            if (selectedFeedback == null)
            {    
                // ... If it is IS NULL, print a debug message            
                #pragma warning disable 4014 // disabling warning since this function is not dependent on the async function
                Async_DebugLogWithFeedback(
                    "FEELGC2_Integration:FEELGC2_UsefulStaticMethods:AddChosenFeedback()<MMF_Feedback selectedFeedback>",
                    chosenFeedback
                );
                #pragma warning restore 4014 // restoring this warning since other async methods may need this
            } 
            else
            {
                ////////////////////////////////////////////////
                // 4. If feedback is NOT NULL, add it
                //  This is a FEEL method!!!!! It adds a Feedback!
                mmfPlayer.AddFeedback(selectedFeedback); // DON'T MODIFY function name `AddFeedback` or its arguments (it's a FEEL function)
            }

            return feelgc2Components;
        }

        // -------------------------------------------------------------------- //
        ///////////////////////// USEFUL FUNCTIONS ///////////////////////////////
        // -------------------------------------------------------------------- //

        /*
            *  Asynchronosly function that prints with `Debug.Log()` to print out a debug message with given arguments.
            *
            *  origin [string]: The origin that called this method.
            *  chosenFeedback [ChosenFeedback]: the chosen enum `ChosenFeedback` 
            *    (use `FEELGC2_Integration.ChosenFeedback` to aid with debugging).
        */
        public static async Task<int> Async_DebugLogWithFeedback(string origin, 
                                                                 ChosenFeedback chosenFeedback)
        {
            string preamble = " - This `ChosenFeedback` DOES NOT exist: ";
            Debug.Log(origin + preamble + chosenFeedback);      

            return await Task.FromResult(1);      
        }

            // =   =   =   =   =   =   =   =   =   =   =   =   =   =   =   =   =   =   =   =  =  = //  

        /*
            *  Plays the feedbacks associated with the given `MMF_Player`.
            *
            *  mmfPlayer [MMF_Player]: The `MMF_Player` object with the feedbacks to be played.
        */
        public static void FEELGC2_PlayFeedbacks(MMF_Player mmfPlayer)
        {
            mmfPlayer.Initialization(); // initialize `MMF Player`
            mmfPlayer.PlayFeedbacks();  // play the feedbacks
        }


        // -------------------------------------------------------------------- //
        //////////////////////// SET UP COMPONENTS ///////////////////////////////
        // -------------------------------------------------------------------- //

        /*
            *  Provide a valid `FEELGC2_Components` object based on `TargetObject`.
            *
            *  TargetObject [GameObject]: The Target GameObject that FEEL will add its stuff on
            *  components [FEELGC2_Components]: The class object that has references to specific components in `TargetObject`
            *
            *  Returns: NULL if setup fails, but returns a valid `FEELGC2_Components` object
        */
        public static FEELGC2_Components Setup(GameObject TargetObject, 
                                               FEELGC2_Components components) 
        {

            // 1. If TargetObject is NULL, setup CANNOT SUCCEED, so return `NULL`
            if (TargetObject == null) 
            {
                Debug.Log(
                    "FEELGC2_Integration:FEELGC2_UsefulStaticMethods:Setup() - GameObject is NULL"
                );
                return null;
            } 

            // 2. If components is NULL...
            if(components == null) 
            {
                // ... add the `MMFPlayer` component
                components = new FEELGC2_Components(TargetObject);
            }

            // 3. Enable the `MMFPlayer` component
            MMF_Player mmfPlayer;
            mmfPlayer = TargetObject.GetComponent<MMF_Player>();
            mmfPlayer.enabled = true;

            return components; // if I reach this point, setup is successful!
        }

        /*
            *  Remove the chosen feedback (Assumes there is only one custom feedback).
            *
            *  TargetObject [GameObject]: The Target GameObject that FEEL will add its stuff on.
            *  chosenFeedback [ChosenFeedback]: the `enum ChosenFeedback` that will determine which string label to return.
            *  feelgc2Component [FEELGC2_Components]: Object that has all the components.
        */
        public static void ClearFeedback(GameObject targetObject, 
                                            ChosenFeedback chosenFeedback,
                                            FEELGC2_Components feelgc2Components)
        {
            //Set the `MMF_Player` object 
            MMF_Player mmfPlayer = feelgc2Components.cMMFPlayer;

            mmfPlayer.RemoveFeedback(0); // 0 is the index of the first feedback since
                                         //  we assume there is only ever 1 feedback added
        }





        //////////////////////////////////////////////////////////////
        //////////////////////////  ARCHIVE //////////////////////////
        //////////////////////////////////////////////////////////////

        /*
            *  This method will search if a `MMF_Feedback` with an associated label string (searchLabel)
           exists within the given `MMF_Player` object (mmfPlayer).
            *
            *  mmfPlayer [MMF_Player]: the `MMF_Player` object we will be searching on.
            *  searchLabel [string]: the label string that we will be searching on.
            *  TargetObject [GameObject]: The Target GameObject that FEEL will add its stuff on.
        public static bool GivenFeedbackExists(MMF_Player mmfPlayer, 
                                                string searchLabel,
                                                GameObject targetObject)
        {
            List<MMF_Feedback> mmfFeedbacksList = mmfPlayer.FeedbacksList;

            // If the list is empty or `searchLabel` is null...
            // ...then a feedback with the `Label` `searchLabel` DOES NOT exist
            if (mmfFeedbacksList.Count == 0 || searchLabel == null) 
            {
                return false; // this means this feedback DOES NOT exist
            } 

            // Iterate through the list to see if it exists
            // TODO: IS THERE A WAY I CAN MAKE THIS MORE EFFICIENT???
            for (int index = 0; index < mmfFeedbacksList.Count; index++)
            {
                if(mmfFeedbacksList[index].Label.Equals(searchLabel))
                {
                    return true;
                }
            }

            return false; // an MMF_Feedback with the `Label` of `searchLabel` was NOT FOUND
        }
        */

        // -------------------------------------------------------------------- //
        ////////// FUNCTIONS ASSOCIATED WITH `AddChosenFeedback()`: //////////////
        ///////////////////// ENUM DEPENDENT FUNCTIONS ///////////////////////////
        // -------------------------------------------------------------------- //

        /*  // This function is associated with `AddChosenFeedback()` !!!!!

            *  This method will get the corresponding string label based on `chosenFeedback`.
            *
            *  chosenFeedback [ChosenFeedback]: the `enum ChosenFeedback` that will determine which string label to return.
            * 
            *  Returns: A string that will be the label of the given `chosenFeedback`. Returns NULL otherwise.
        public static string GetFeedbackLabel(ChosenFeedback chosenFeedback)
        {
            switch (chosenFeedback)
            {
                //---------------------------//
                // Feedback (1) -------------//
                case ChosenFeedback.JumpAndScale:
                    return "JumpAndScale";

                //---------------------------//
                // Feedback (2) -------------//
                case ChosenFeedback.RotateRight:
                    return "RotateRight";

                //---------------------------//
                // Feedback (3) -------------//
                case ChosenFeedback.RotateLeft:
                    return "RotateLeft";

                //---------------------------//
                // Feedback (4) -------------//
                case ChosenFeedback.SquashAndStretch:
                    return "SquashAndStretch";


                //---------------------------//
                // Feedback (5) -------------//

                //---------------------------//
                // Feedback (15) -------------//
                case ChosenFeedback.CinemachineImpulse:
                    return "CinemachineImpulse";
                
                //---------------------------//
                // ADD FEEL STUFF HERE 
            }
            
            // This error triggers if invalid. Probably means the above enum and this switch
            //   statement is not in sync
            return "Error in FEELGC2_Integration.cs GetFeedbackLable()";
        }
        */
        
    }

    ////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////
}