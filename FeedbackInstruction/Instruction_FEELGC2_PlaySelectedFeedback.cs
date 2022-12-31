using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

using MoreMountains.Feedbacks;
using MoreMountains.Tools;

using FEELGC2_Enums;
using FEELGC2_StaticMethods;
using FEELGC2_IntegrationObjects;

/////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////

[Category("FEEL/PlaySelectedFeedbacks")]
[Dependency("MoreMountains.Feedbacks", 3, 3, 3)]
[Dependency("MoreMountains.Tools", 3, 3, 3)]
[Dependency("FEELGC2_Integration", 1, 1, 1)]
[Dependency("FEELGC2_Objects", 1, 1, 1)]
[Keywords("Feel", "FEEL", "Feel Game Creator2", "FEELGC2", 
            "Animation", "Effects", "Special Effects")]
[Serializable]
public class FEELGC2_PlaySelectedFeedbacks : Instruction
{

    //////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////
    // This script will automatically get a GameObject of the selected tag
    //  from the drop down menu you selected
    [TagSelector]
    public string TagFilter = "";
    /* // Save for later; I can use this when I want to affect multiple tags
    [TagSelector]
    public string[] TagFilterArray = new string[] { };
    */

    [Header("Feedbacks")]
    // `ChosenFeedback` is an enum, so this will give user a drop-down of the enums in the UI
    public ChosenFeedback SelectedFeedback;

    // FEEL will add its stuff to the `GameObject` that called this script
    // (`gameObject` referes to the `GameObject` that called this script)
    private GameObject TargetObject = null;

    // The components that will be associated with the above `GameObject`
    protected FEELGC2_Components components = null;


    ///////////////////////////////////////////////////////////////////////////////////////
    // This method execute this Instruction ///////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////
    protected override Task Run(Args args)
    {

        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //

        // 1. Get the GameObject that FEEL will add its feedbacks to 
        TargetObject = GameObject.FindWithTag(TagFilter);
        if (string.IsNullOrEmpty(TagFilter))
        {
            Debug.Log("FEELGC2_PlaySelectedFeedbacks:Run() - TagFilter is NULL or Empty");
            return DefaultResult; // end this function as if nothing happened
        } 
        else 
        {
            #pragma warning disable 4014 // disabling warning since this function is not dependent on the async function
            StaticFEELGC2Methods.AsyncCheckIfMultipleObjectsInTag(TagFilter);
            #pragma warning restore 4014 // restoring this warning since other async methods may need this
        }


        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //

        // 2. Make sure this Instruction has what it needs to run successfully
        this.components = StaticFEELGC2Methods.Setup(
            this.TargetObject, 
            this.components
        );
        if (this.components == null) { // If this is NULL, `Setup()` FAILED!!!
            Debug.Log("GC2_PlayFEELFeedback:Run() - StaticFEELGC2Methods.Setup() FAILED");
            return DefaultResult; // end this function as if nothing happened
        }


        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //

        // 3. Add the feedback to the given `MMF_Player`
        //     (if it was already added, `AddChosenFeedback` will just
        //      return the given `MMF_Player` object as it was given)
        // 
        if(this.components.cMMFPlayer == null) // If `MMF Player` is null, set it
        {
            // ...ADD the `MMF Player` component
            //    (this will also GET `MMF Player` component)
            this.components.cMMFPlayer = TargetObject.GetComponent<MMF_Player>();
        }
        this.components.cMMFPlayer.enabled = true;
        this.components = StaticFEELGC2Methods.AddChosenFeedback(
            this.TargetObject, 
            this.components, 
            this.SelectedFeedback
        );
        

        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //
        
        // 4. Play the feedbacks (play it `HowManyTimesToPlay` time)
        StaticFEELGC2Methods.FEELGC2_PlayFeedbacks(
            this.components.cMMFPlayer
        );

        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //

        // 5. Clear the set feedback (this method assumes there's only one feedback set)
        StaticFEELGC2Methods.ClearFeedback(
            this.TargetObject, 
            this.SelectedFeedback,
            this.components
        ); 

        // -------------------------------------------------------------------- //
        // // // // // // // // // // // // // // // // // // // // // // // // //
        // -------------------------------------------------------------------- //

        return DefaultResult;
    }

}

