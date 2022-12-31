# GameCreator2_Feel_Integration
Creating Game Creator 2 Actions that integrate with the FEEL API

--------------

### <span style="color:Gold;">Starts Here</span>: <span style="color:aquamarine;">Instruction_FEELGC2_PlaySelectedFeedbacks.cs:Run()</span>

1. Gets certain values from the Inspector
	* The `TagFilter` that's selected in the drop down
	* The `SelectedFeedback` that's selected in the drop down

2. Make sure this _custom_ <span style="color:RoyalBlue;">Game Creator 2 Instruction</span> has what is needs
	* Calls <span style="color:aquamarine;">FEELGC2_StaticMethods:FEELGC2_UsefulStaticMethods.Setup()</span>
		* <span style="color:Gold;">(a)</span> - Look for this symbol for more details

3. Add the _chosen feedback_ that's selected in the drop down menu in the Inspector
	* Calls <span style="color:aquamarine;">FEELGC2_StaticMethods.AddChosenFeedback()</span>
		* <span style="color:Gold;">(b)</span> - Look for this symbol for more details

4. Play the _feedbacks_
	* Calls <span style="color:aquamarine;">FEELGC2_StaticMethods.FEELGC2_PlayFeedbacks()</span>
		* <span style="color:aquamarine;">mmfPlayer.Initialization();</span> <span style="color:DimGray;">// initialize `MMF Player`</span>
		* <span style="color:aquamarine;">mmfPlayer.PlayFeedbacks();</span>  <span style="color:DimGray;">// play the feedbacks</span>

5. Clear the selected & added _feedback_
	* Calls <span style="color:aquamarine;">FEELGC2_StaticMethods.ClearFeedback()</span>

-----------------------

### <span style="color:Gold;">(a)</span> <span style="color:aquamarine;">FEELGC2_StaticMethods.Setup()</span>

1. If TargetObject is `NULL`, setup **CANNOT SUCCEED**, so return `NULL`

2. If components is `NULL`, add the <span style="color:aquamarine;">MMFPlayer</span> _component_
	* Calls <span style="color:aquamarine;">FEELGC2_IntegrationObjects:FEELGC2_Components:FEELGC2_Components()</span>
		* This will add the <span style="color:aquamarine;">MMFPlayer</span> component if it doesn't already exist

3. Enable the <span style="color:aquamarine;">MMFPlayer</span> _component_

--------------

### <span style="color:Gold;">(b)</span> <span style="color:aquamarine;">FEELGC2_StaticMethods.AddChosenFeedback()</span>
1. Calls <span style="color:aquamarine;">FEELGC2_StaticMethods:GetFeedbackLabel()</span>
	* Get the _key `string` value_ for the <span style="color:Gold;">selected feedback</span> the user selected from the drop down
	* This is the _key `string` value_ for the <span style="color:aquamarine;">FEELGC2_Enums:ChosenFeedback</span> <span style="color:Gold;">enum</span>
		* The _value_ of the <span style="color:Gold;">enum</span> is an _integer_

2. Check if the <span style="color:Gold;">selected feedback</span> from `Step 1` (right above) already exists...
	* ... you don't need to continue this method (_adding the feedback_)
	* Calls <span style="color:aquamarine;">FEELGC2_IntegrationObjects:FEELGC2_Components:GivenFeedbackExists()</span>

3. Get the corresponding <span style="color:aquamarine;">MMFeedback</span> based on the value of <span style="color:Gold;">selected feedback</span>
	* Calls <span style="color:aquamarine;">FEELGC2_IntegrationObjects:FEELGC2_Components:GetChosenFeedback()</span>

4. If <span style="color:Gold;">selected feedback</span> is **DOES NOT EXIST**, then <span style="color:FireBrick;">ADD IT</span>!!!

------
