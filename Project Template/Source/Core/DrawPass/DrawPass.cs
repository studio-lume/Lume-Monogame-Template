using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Components;
using Project_Template.Source.Core.Behaviours;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Services.LoggerService;

namespace Project_Template.Source.Core.DrawPass {
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
        static readonly LoggerService LoggerService = new();
        readonly List<IActor> registeredActors = [];
        readonly List<IActor> culledActors = [];
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
                    LoggerService.Log(
                        LogLevel.Error,
                        LogCategory.Core,
                        "Draw pass has already been defined",
                        new LoggerContext()
                            .AddSection("Draw Pass Information")
                            .Add("Draw pass ID", drawPassId));
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
        /// <param name="deltaTime">The time elapsed since last frame</param>
        public void Update(float deltaTime) {
            culledActors.Clear();
            foreach (var actor in registeredActors) {
                ((IActorInternal)actor).CoreUpdateComponents(deltaTime);
                actor.Update(deltaTime);

                var behaviour = (ActorBehaviour)actor;
                if (!Camera.Current.WorldBounds.Intersects(behaviour.Transform.AABB)) {
                    continue;
                }

                culledActors.Add(actor);
            }
        }

        /// <summary>
        ///     Registers multiple actors to the drawPass Pipeline
        ///     If the actor is already added, an error message will be thrown.
        /// </summary>
        /// <remarks>
        ///     The function can either be called as:
        ///     <code>DrawPass.RegisterActors(id, actorA, actorB, ActorC...)</code>
        ///     Or alternatively with an array:
        ///     <code>DrawPass.RegisterActors(id, [actorA, actorB, actorC...])</code>
        /// </remarks>
        /// ///
        /// <param name="actors">The list of actors which will be added to the drawPass</param>
        public void RegisterActors(params IActor[] actors) {
            foreach (var actor in actors) {
                if (registeredActors.Contains(actor)) {
                    LoggerService.Log(
                        LogLevel.Error,
                        LogCategory.Core,
                        "Actor already exists",
                        new LoggerContext()
                            .AddSection("Actor Information")
                            .Add("Actor Type", actor.GetType().Name));
                }

                registeredActors.Add(actor);
                actor.Start();
            }
        }

        /// <summary>
        ///     Removes multiple actors from a registeredActors list within the drawPass pipeline.
        ///     If an actor doesn't exist, and error message will be thrown.
        /// </summary>
        /// <remarks>
        ///     The function can either be called as:
        ///     <code>DrawPass.UnregisterActors(actorA, actorB, ActorC...)</code>
        ///     Or alternatively with an array:
        ///     <code>DrawPass.UnregisterActors([actorA, actorB, actorC...])</code>
        /// </remarks>
        /// <param name="actors">The list of actors which should be removed from the drawPass</param>
        public void UnregisterActors(params IActor[] actors) {
            foreach (var actor in actors) {
                if (!registeredActors.Remove(actor)) {
                    LoggerService.Log(
                        LogLevel.Error,
                        LogCategory.Core,
                        "Actor doesn't exists",
                        new LoggerContext()
                            .AddSection("Actor Information")
                            .Add("Actor type", actor.GetType().Name));
                }

                ((IActorInternal)actor).CoreEndComponents();
                actor.End();
            }
        }

        /// <summary>
        ///     Empties all the registered actors within the drawPass object
        ///     <summary>
        ///         Empties all the registered actors within the drawPass object
        ///     </summary>
        public void ClearPasses() {
            foreach (var actor in registeredActors) {
                ((IActorInternal)actor).CoreEndComponents();
                actor.End();
            }
        }

        /// <summary>
        ///     Draws all the actors in the order defined when creating the pass
        /// </summary>
        public void Draw() {
            spriteBatch ??= new(Global.GraphicsDevice);
            var batchIsActive = false;

            // we gather all the draw requirements from each actor
            // and store it in a pass object
            // Later we'll use the drawPassId of each call to sort and draw to the screen
            DrawPassPass pass = new();
            Drawer drawer = new(pass);
            Console.WriteLine(culledActors.Count);
            foreach (var actor in culledActors) {
                actor.Draw(drawer);
            }

            foreach (var passDefinition in definitions) {
                if (passDefinition.Type == DrawPassOrderType.DrawPass) {
                    if (!pass.DrawOrder.TryGetValue(passDefinition.DrawPassId, out var drawInstancesList)) {
                        continue;
                    }

                    foreach (var drawInstance in drawInstancesList) {
                        if (drawInstance.DestinationRectangle != default) {
                            spriteBatch.Draw(
                                drawInstance.Texture2D,
                                drawInstance.DestinationRectangle,
                                drawInstance.SourceRectangle,
                                drawInstance.Color,
                                drawInstance.Rotation,
                                drawInstance.Origin,
                                drawInstance.SpriteEffects,
                                drawInstance.DepthLayer);
                        } else {
                            spriteBatch.Draw(
                                drawInstance.Texture2D,
                                drawInstance.Position,
                                drawInstance.SourceRectangle,
                                drawInstance.Color,
                                drawInstance.Rotation,
                                drawInstance.Origin,
                                drawInstance.Scale,
                                drawInstance.SpriteEffects,
                                drawInstance.DepthLayer);
                        }
                    }
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
    }
}