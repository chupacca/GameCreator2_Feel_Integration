using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FEELGC2_Enums {

    /* 
        * Enums that will give the user options of `MMF_Feedback`s;
        *  values are explicitly defined to aid debugging.
        * IF YOU APPEND/UPDATE/DELETE from this enum:
        *   1. Update `GetChosenFeedback()`'s switch statement
        *     + By extension, udate -> FEELGC2_CustomFeedbacks.cs:FEELGC2_CustomFeedbacks
        *
        *   // Not doing 2 anymore?
        *   2. Update `GetFeedbackLabel()`'s switch statement
    */
    public enum ChosenFeedback: int
    {
        // Transform FEEL MMFeedbacks 
        JumpAndScale = 0,
        ScaleHoizontally = 1,
        ScaleVertically = 2,
        RotateRight = 3,
        RotateLeft = 4,
        SquashAndStretch = 5,

        // TODO: Implement THESE
        ////////////////////////////////////////////////////
        LightDim = 6,       // MMFeedbackLight   
        LightFlash = 7,     // MMFeedbackLight 
        Flicker = 8,        // MMFeedBackFlicker

        Pause = 9,                       // MMFeedbackPause
        MostionBlurURP = 10,              // MMFeedbackMotionBlur_URP
        LensDistortionURP = 11,          // MMFeedbackMotionBlur_URP
        ChromaticAberrationURP = 12,     // MMFeedbackChromaticAberration_URP 
        ColorGrading = 13,               // MMFeedbackColorGrading

        // Shake 
        CameraShakeEvent = 14,   // MMCamerShakeEvent (MMCinemachineCameraShaker on Cinemachine or MMCameraShaker on camera needed)
                                 // ugh, it has weird conditions (need to set velocity on this motherfucker)
                                 
        CinemachineImpulse = 15, // MMFeedbackCinemachineImpulse
        HapticPreset = 16, // MMFeedbackNVPreset 

        // Animate Object 
        Position = 17, // this needs a prefab

    }

}