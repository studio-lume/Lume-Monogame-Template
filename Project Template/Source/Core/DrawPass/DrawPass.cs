using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.DrawPass {
    public class DrawPass : DrawPassCore {
        /// <summary>
        ///     Draw every actor the screen following batches and drawPassIds.
        ///     each batch can be customized with the parameters of the Batch() function.
        ///     <example>
        ///         <code>
        ///      using (var batch = Batch(1, samplerState: SamplerState.PointClamp)) {
        ///          DrawPassToScreen(DrawPassId.Pass1, batch);
        ///          DrawPassToScreen(DrawPassId.Pass2, batch);
        ///       }
        ///         </code>
        ///     </example>
        /// </summary>
        public void Draw() {
            using (var batch = Batch(1)) {
                DrawPassToScreen(DrawPassId.Test, batch);
                DrawPassToScreen(DrawPassId.Test2, batch);
            }

            using (var batch = Batch(2)) {
                DrawPassToScreen(DrawPassId.Blocks, batch);
            }
        }

        /// <summary>
        ///     Updates all the actors inside each DrawPass
        /// </summary>
        /// <remarks>
        ///     The flow of updating follow the drawPassIds chronologically.
        ///     This means drawPass 1 will be updated before drawPass 2
        /// </remarks>
        /// <param name="deltaTime">The time elapsed since last frame</param>
        public new void Update(float deltaTime) => base.Update(deltaTime);

        /// <summary>
        ///     Adds multiple actors to a selected drawPass.
        ///     If the actor is already added, an error message will be thrown.
        /// </summary>
        /// <remarks>
        ///     The function can either be called as:
        ///     <code>DrawPass.RegisterActors(id, actorA, actorB, ActorC...)</code>
        ///     Or alternatively with an array:
        ///     <code>DrawPass.RegisterActors(id, [actorA, actorB, actorC...])</code>
        /// </remarks>
        /// <param name="id">The drawPassId where the new actors will be assigned to</param>
        /// <param name="actors">The list of actors which will be added to the drawPass</param>
        public new void RegisterActors(DrawPassId id, params IActor[] actors) => base.RegisterActors(id, actors);

        /// <summary>
        ///     Removes multiple actors from a drawPass.
        ///     If an actor doesn't exist, and error message will be thrown.
        /// </summary>
        /// <remarks>
        ///     The function can either be called as:
        ///     <code>DrawPass.UnregisterActors(id, actorA, actorB, ActorC...)</code>
        ///     Or alternatively with an array:
        ///     <code>DrawPass.UnregisterActors(id, [actorA, actorB, actorC...])</code>
        /// </remarks>
        /// <param name="id">The id of the drawPass where the actors are located</param>
        /// <param name="actors">The list of actors which should be removed from the drawPass</param>
        public new void UnregisterActors(DrawPassId id, params IActor[] actors) => base.UnregisterActors(id, actors);

        /// <summary>
        ///     Clears and entire pass by the given DrawPassId
        /// </summary>
        /// <param name="id">The drawPassId that should get cleaned out</param>
        public new void ClearPass(DrawPassId id) => base.ClearPass(id);
    }
}