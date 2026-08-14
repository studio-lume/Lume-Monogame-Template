using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project_Quarry.Source.Data.Enums;
using Project_Quarry.Source.Data.Interfaces;
using Project_Quarry.Source.Temp;

namespace Project_Quarry.Source.Core.DrawPass {
    public class DrawPassCore {
        readonly Dictionary<DrawPassId, List<IActor>> passes = [];
        readonly Dictionary<ushort, SpriteBatch> batchBuffer = [];

        protected void Update(float deltaTime) {
            foreach (var pass in passes)
            foreach (var actor in pass.Value) {
                actor.UpdateComponents(deltaTime);
                actor.Update(deltaTime);
            }
        }

        protected void RegisterActors(DrawPassId drawPass, params IActor[] actors) {
            passes.TryAdd(drawPass, []);
            foreach (var actor in actors) {
                if (passes[drawPass].Contains(actor)) {
                    continue;
                }

                passes[drawPass].Add(actor);
                actor.Start();
            }
        }

        protected void UnregisterActors(DrawPassId drawPass, params IActor[] actors) {
            if (!passes.TryGetValue(drawPass, out var passActors)) {
                return;
            }

            foreach (var actor in actors) {
                if (passActors.Remove(actor)) {
                    actor.End();
                }
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

        SpriteBatch GetBatch(ushort batchId) {
            if (batchBuffer.TryGetValue(batchId, out var batch)) {
                return batch;
            }

            batch = new(DeviceService.Service);
            batchBuffer.Add(batchId, batch);

            return batch;
        }

        protected BatchScope Batch(
            ushort batchId,
            SpriteSortMode spriteSortMode = SpriteSortMode.Deferred,
            BlendState blendState = null,
            SamplerState samplerState = null
        ) {
            var batch = GetBatch(batchId);
            batch.Begin(
                spriteSortMode,
                blendState ?? BlendState.AlphaBlend,
                samplerState ?? SamplerState.PointClamp
            );

            return new(batch);
        }

        protected sealed class BatchScope : IDisposable {
            readonly SpriteBatch batch;

            internal BatchScope(SpriteBatch batch) => this.batch = batch;

            public static implicit operator SpriteBatch(BatchScope scope)
                => scope.batch;

            public void Dispose() {
                batch.End();
            }
        }
    }
}