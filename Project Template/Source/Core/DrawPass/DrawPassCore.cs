using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.DrawPass {
    enum DrawPassOrderType {
        DrawPass,
        Batch
    }

    readonly struct DrawPassDefinition {
        public SpriteSortMode SpriteSortMode { get; init; }
        public BlendState BlendState { get; init; }
        public SamplerState SamplerState { get; init; }
        public DrawPassId DrawPassId { get; init; }
        public DrawPassOrderType OrderType { get; init; }
    }

    public class DrawPassCore {
        readonly Dictionary<DrawPassId, List<IActor>> passes = [];
        readonly List<DrawPassDefinition> drawPassOrder = [];
        SpriteBatch spriteBatch;

        protected void Update(float deltaTime) {
            foreach (var pass in passes)
            foreach (var actor in pass.Value) {
                ((IActorInternal)actor).CoreUpdateComponents(deltaTime);
                actor.Update(deltaTime);
            }
        }

        protected void RegisterPass(DrawPassId drawPassId) => drawPassOrder.Add(new() {
            DrawPassId = drawPassId,
            OrderType = DrawPassOrderType.DrawPass
        });

        protected void RegisterBatch(
            SpriteSortMode spriteSortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null
        ) => drawPassOrder.Add(new() {
            SamplerState = samplerState,
            SpriteSortMode = spriteSortMode,
            BlendState = blendState,
            OrderType = DrawPassOrderType.Batch
        });

        protected void DrawOrder() {
            spriteBatch ??= new(Global.GraphicsDevice);
            var batchIsActive = false;
            foreach (var definition in drawPassOrder) {
                if (definition.OrderType == DrawPassOrderType.DrawPass) {
                    DrawPassToScreen(definition.DrawPassId, spriteBatch);
                } else {
                    if (batchIsActive) {
                        spriteBatch.End();
                    }

                    batchIsActive = true;
                    spriteBatch.Begin(definition.SpriteSortMode, definition.BlendState, definition.SamplerState);
                }
            }

            spriteBatch?.End();
        }

        protected void RegisterActors(DrawPassId drawPass, params IActor[] actors) {
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

        protected void UnregisterActors(DrawPassId drawPass, params IActor[] actors) {
            if (!passes.TryGetValue(drawPass, out var passActors)) {
                // TODO: Make loggerService for these errors
                throw new("Actor doesn't exists.");
            }

            foreach (var actor in actors) {
                if (passActors.Remove(actor)) {
                    actor.End();
                }
            }

            if (passActors.Count == 0) {
                passes.Remove(drawPass);
            }
        }

        protected void ClearPass(DrawPassId drawPass) {
            if (!passes.TryGetValue(drawPass, out var passActors)) {
                return;
            }

            foreach (var actor in passActors) {
                actor.End();
            }

            passActors.Clear();
            passes.Remove(drawPass);
        }

        protected void DrawPassToScreen(DrawPassId drawPass, SpriteBatch spriteBatch) {
            if (!passes.TryGetValue(drawPass, out var passActors)) {
                return;
            }

            foreach (var actor in passActors) {
                actor.Draw(spriteBatch);
            }
        }
    }
}