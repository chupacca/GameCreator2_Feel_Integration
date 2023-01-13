using Unity.Entities;
using UnityEngine;

public class JumpSystemWrapper : ComponentSystem
{
    protected override void OnCreate()
    {
        //Create an instance of the JumpSystem
        var jumpSystem = World.GetOrCreateSystem<JumpSystem>();
    }
    protected override void OnUpdate()
    {
        //Call the OnUpdate method of the JumpSystem
        World.GetExistingSystem<JumpSystem>().OnUpdate();
    }
}
