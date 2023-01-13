// JumpData class is a component data that implements the IComponentData interface
// it has a public field Value which is an instance of the MMFeedbacks class
public class JumpData: IComponentData
{
    public MMFeedbacks Value;
}

// JumpSystem class is a system that controls the behavior of the jump action
// it is derived from the SystemBase class which is a base class for creating systems in Unity's ECS
public partial class JumpSystem: SystemBase
{
    // OnUpdate method is called every frame
    protected override void OnUpdate()
    {
        // Check if the space key is pressed
        if(Input.KeyDown(KeyCode.Space))
        {
            // Retrieve a collection of all entities that have the JumpData component
            foreach (var jumpData in SystemAPI.Query<JumpData>)
            {
                // Call the PlayFeedbacks method on the Value field of the JumpData component
                jumpData.Value.PlayFeedbacks();
            }
        }
    }
}
