using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Core.DrawPass;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Actors {
    public abstract class ActorBehaviour : IActor {
        readonly Dictionary<Type, IComponent> components = [];
        readonly List<IComponent> componentList = [];
        readonly DrawPassId drawPassId;

        /// <summary>
        ///     Adds a component to the actor following the IComponent contract.
        ///     Component initializer will fire before being added.
        /// </summary>
        /// <param name="component">The component to be added to the actor</param>
        /// <returns>The initiated component</returns>
        protected T AddComponent<T>(T component) where T : IComponent {
            component.Initialize(this);
            components.TryAdd(component.GetType(), component);
            componentList.Add(component);
            return component;
        }

        /// <summary>
        ///     Tries to fetch a component by its type. If it succeeds, it returns the component.
        ///     If not, the return type is false, flagging the operation as failed.
        /// </summary>
        /// <param name="componentType">The type of the component which should be fetched</param>
        /// <param name="component">The component from fetched from the list</param>
        /// <typeparam name="T">T follows contract IComponent</typeparam>
        /// <returns>If true, the operation is successful, if not, then it indicates the operation has failed</returns>
        protected bool TryGetComponent<T>(Type componentType, out T component) where T : IComponent {
            component = default;
            if (!components.TryGetValue(componentType, out var actorComponent)) {
                return false;
            }

            if (actorComponent is not T castedActorComponent) {
                return false;
            }

            component = castedActorComponent;
            return true;
        }

        /// <summary>
        ///     Removes a component from the actor.
        /// </summary>
        /// <param name="component">The component which should be removed</param>
        protected void RemoveComponent<T>(T component) where T : IComponent {
            components.Remove(component.GetType());
            componentList.Remove(component);
        }

        /// <summary>
        ///     Checks whether the actor has a given component
        /// </summary>
        /// <param name="componentType">The type of component which should be checked</param>
        /// <returns>Returns true if the component is attached to the actor, returns false if not</returns>
        protected bool HasComponent(Type componentType) => components.ContainsKey(componentType);

        /// <summary>
        ///     Updates each component's update loop
        /// </summary>
        /// <param name="deltaTime">Time elapsed since last </param>
        /// frame
        public void UpdateComponents(float deltaTime) {
            foreach (var component in componentList) {
                component.Update(deltaTime);
            }
        }

        /// <summary>
        /// Registers the actor to the selected drawPassId passed through the constructor
        /// </summary>
        /// <param name="drawPass">The drawpass Instance of the scene</param>
        public void RegisterDrawPass(DrawPass drawPass) => drawPass.RegisterActors(drawPassId, this);

        /// <summary>
        ///     Creates an instance of an of actor and assigns it to a drawPass
        /// </summary>
        /// <param name="drawPassId">The drawPass the actor will be assigned to</param>
        protected ActorBehaviour(DrawPassId drawPassId) => this.drawPassId = drawPassId;

        //-----------------------------------------------------------//
        // Filler functions for filling in actors
        // and giving them functionality
        //
        // Start is called when an actor gets initialized, right after the constructor
        // Update is called each frame before the draw method
        // Draw is called after the update method.
        // End is called when the actor reaches the end of its lifecycle and the destructor is called 
        //-----------------------------------------------------------//

        public virtual void Start() {
        }

        public virtual void End() {
        }

        public virtual void Update(float deltaTime) {
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
        }
    }
}