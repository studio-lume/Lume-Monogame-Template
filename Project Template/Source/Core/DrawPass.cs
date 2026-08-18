using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Components;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core {
    enum DrawPassOrderType {
        DrawPass,
        SpriteBatch
    }

    readonly record struct DrawPassDefinition(
        DrawPassOrderType Type,
        DrawPassId DrawPassId,
        SpriteSortMode SpriteSortMode,
        BlendState BlendState,
        SamplerState SamplerState
    );

    public class DrawPass {
        readonly Dictionary<DrawPassId, List<IActor>> passes = [];
        readonly List<DrawPassDefinition> definitions = [];
        SpriteBatch spriteBatch;

        /// <summary>
        ///     Registers a new actor pass using the specified <see cref="DrawPassId" />.
        ///     Actors registered with this ID will be processed at the position where the
        ///     pass was added.
        /// </summary>
        /// <param name="drawPassId">The identifier of the actor pass to register.</param>
        /// <returns>The current <see cref="DrawPass" /> instance for method chaining.</returns>
        public DrawPass AddPass(DrawPassId drawPassId) {
            foreach (var definition in definitions) {
                if (definition.Type == DrawPassOrderType.DrawPass &&
                    definition.DrawPassId == drawPassId) {
                    throw new InvalidOperationException(
                        $"Draw pass '{drawPassId}' has already been defined."
                    );
                }
            }

            definitions.Add(new() {
                Type = DrawPassOrderType.DrawPass,
                DrawPassId = drawPassId,
                SpriteSortMode = default,
                BlendState = null,
                SamplerState = null
            });
            return this;
        }

        /// <summary>
        ///     Ends the current SpriteBatch batch and starts a new batch.
        /// </summary>
        /// <param name="spriteSortMode">
        ///     The sorting mode used when drawing sprites in the new batch.
        /// </param>
        /// <param name="blendState">
        ///     The blend state used when drawing sprites in the new batch.
        /// </param>
        /// <param name="samplerState">
        ///     The sampler state used when drawing sprites in the new batch.
        /// </param>
        /// <returns>The current <see cref="DrawPass" /> instance for method chaining.</returns>
        public DrawPass NewBatch(
            SpriteSortMode spriteSortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null
        ) {
            definitions.Add(new() {
                Type = DrawPassOrderType.SpriteBatch,
                DrawPassId = default,
                SpriteSortMode = spriteSortMode,
                BlendState = blendState,
                SamplerState = samplerState
            });
            return this;
        }

        /// <summary>
        ///     Updates all the actors inside each DrawPass
        /// </summary>
        /// <remarks>
        ///     The flow of updating follow the drawPassIds chronologically.
        ///     This means drawPass 1 will be updated before drawPass 2
        /// </remarks>
        /// <param name="deltaTime">The time elapsed since last frame</param>
        public void Update(float deltaTime) {
            foreach (var definition in definitions) {
                if (definition.Type != DrawPassOrderType.DrawPass) {
                    continue;
                }

                if (!passes.TryGetValue(definition.DrawPassId, out var actors)) {
                    continue;
                }

                foreach (var actor in actors) {
                    ((IActorInternal)actor).CoreUpdateComponents(deltaTime);
                    actor.Update(deltaTime);
                }
            }
        }

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
        /// <param name="drawPass">The drawPassId where the new actors will be assigned to</param>
        /// <param name="actors">The list of actors which will be added to the drawPass</param>
        public void RegisterActors(DrawPassId drawPass, params IActor[] actors) {
            passes.TryAdd(drawPass, []);
            foreach (var actor in actors) {
                if (passes[drawPass].Contains(actor)) {
                    // TODO: Make loggerService for these errors
                    throw new("Actor already exists.");
                }

                passes[drawPass].Add(actor);
                actor.Start();
            }
        }

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
        /// <param name="drawPassId">The id of the drawPass where the actors are located</param>
        /// <param name="actors">The list of actors which should be removed from the drawPass</param>
        public void UnregisterActors(DrawPassId drawPassId, params IActor[] actors) {
            if (!passes.TryGetValue(drawPassId, out var passActors)) {
                // TODO: Make loggerService for these errors
                throw new("Actor doesn't exist.");
            }

            foreach (var actor in actors) {
                if (!passActors.Remove(actor)) {
                    continue;
                }

                ((IActorInternal)actor).CoreEndComponents();
                actor.End();
            }

            if (passActors.Count == 0) {
                passes.Remove(drawPassId);
            }
        }

        /// <summary>
        ///     Empties all the passes within the current DrawPass Object
        /// </summary>
        public void ClearPasses() {
            foreach (var pass in passes)
            foreach (var actor in pass.Value) {
                ((IActorInternal)actor).CoreEndComponents();
                actor.End();
            }

            passes.Clear();
        }

        /// <summary>
        ///     Draws all the actors in the order defined when creating the pass
        /// </summary>
        public void Draw() {
            spriteBatch ??= new(Global.GraphicsDevice);
            var batchIsActive = false;

            foreach (var passDefinition in definitions) {
                if (passDefinition.Type == DrawPassOrderType.DrawPass) {
                    DrawPassToScreen(passDefinition.DrawPassId, spriteBatch);
                } else {
                    if (batchIsActive) {
                        spriteBatch.End();
                    }

                    batchIsActive = true;
                    spriteBatch.Begin(
                        passDefinition.SpriteSortMode,
                        passDefinition.BlendState ?? BlendState.AlphaBlend,
                        passDefinition.SamplerState ?? SamplerState.PointClamp,
                        transformMatrix: Camera.Current?.ViewMatrix ?? new Matrix()
                    );
                }
            }

            spriteBatch?.End();
        }

        void DrawPassToScreen(DrawPassId drawPass, SpriteBatch spriteBatch) {
            if (!passes.TryGetValue(drawPass, out var passActors)) {
                return;
            }

            foreach (var actor in passActors) {
                actor.Draw(spriteBatch);
            }
        }
    }
}