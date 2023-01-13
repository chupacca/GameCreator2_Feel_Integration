using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

// JumpData class is a component data that implements the IComponentData interface
// it has a public field Value which is an instance of the MMFeedbacks class
public class JumpData: IComponentData
{
    public MMFeedbacks Value;
}

public class JumpSystem: SystemBase
{
    private ComponentGroup jumpDataGroup;
    protected override void OnCreateManager()
    {
        // Create a group of all entities that have the JumpData component
        jumpDataGroup = GetComponentGroup(typeof(JumpData));
    }
    protected override void OnUpdate()
    {
        if(Input.KeyDown(KeyCode.Space))
        {
            // Get the array of all entities that have the JumpData component 
            var jumpDataArray = jumpDataGroup.GetComponentDataArray<JumpData>();
            for (int i = 0; i < jumpDataArray.Length; i++)
            {
                var jumpData = jumpDataArray[i];
                jumpData.Value.PlayFeedbacks();
            }
        }
    }
}
