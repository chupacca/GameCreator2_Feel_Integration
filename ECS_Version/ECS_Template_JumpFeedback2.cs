using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

// Creating a new IComponentData called JumpData 
public class JumpData : IComponentData
{
    // Declaring an MMFeedbacks field called Value
    public MMFeedbacks Value;
}

public class JumpSystem : SystemBase
{
    // Declaring an endSimulationEcbSystem variable to hold the EndSimulationEntityCommandBufferSystem
    private EndSimulationEntityCommandBufferSystem endSimulationEcbSystem;
    private InputAction jumpAction;
    protected override void OnCreate()
    {
        //Creating an InputAction for the space key, and enabling it
        jumpAction = new InputAction(binding: "<Keyboard>/space", interactions: InputActionInteraction.None);
        jumpAction.Enable();
        //Getting the EndSimulationEntityCommandBufferSystem from the world
        endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        //Creating an Entity query to get all entities that have the JumpData component
        var ecb = endSimulationEcbSystem.CreateCommandBuffer().ToConcurrent();
        var jobHandle = Entities
            .WithAll<JumpData>()
            .WithNativeDisableParallelForRestriction(ecb)
            .ForEach((int entityInQueryIndex, ref JumpData jumpData, in Translation translation) =>
            {
                // Checking if the jump action was triggered
                if (jumpAction.triggered)
                {
                    //If the action was triggered, play the feedbacks
                    jumpData.Value.PlayFeedbacks();
                }
            }).Schedule(Dependency);
        //Adding the JobHandle to the endSimulationEcbSystem.
		endSimulationEcbSystem.AddJobHandleForProducer(jobHandle);
    }
}
