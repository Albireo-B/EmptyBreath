using UnityEngine;
public interface IInteractableObject
{
    public bool enabled { get; set; }
    public Transform transform { get; }
    
    //This is the method for interaction, need to be implemented in other class
    public void OnPlayerInteraction();
}


