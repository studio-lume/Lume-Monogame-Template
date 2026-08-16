using System;
using Microsoft.Xna.Framework;
using Project_Template.Source.Core.Behaviours;

namespace Project_Template.Source.Components {
    public class Camera(int renderPriority) : ComponentBehaviour {
        static Camera currentScoped;
        static event Action OnCameraChanged;

        public static Camera Current {
            get => currentScoped;
            private set {
                if (value is null) {
                    currentScoped = null;
                    OnCameraChanged?.Invoke();
                    return;
                }

                if (!value.IsActive) {
                    return;
                }

                if (value != currentScoped &&
                    (currentScoped is null ||
                     value.RenderPriority > currentScoped.RenderPriority)) {
                    currentScoped = value;
                }
            }
        }

        public int RenderPriority { get; } = renderPriority;
        public bool IsActive { get; private set; }
        public float Zoom = 1f;

        Transform transform;

        public override void Initialize(ActorBehaviour actor) {
            if (!actor.TryGetComponent(out Transform transform)) {
                throw new("Cannot add Camera component. Transform component is needed");
            }

            this.transform = transform;
            OnCameraChanged += OnCameraChangedComponent;

            SetActive(true);
        }

        public override void End() {
            SetActive(false);
            OnCameraChanged -= OnCameraChangedComponent;
        }

        void OnCameraChangedComponent() => Current = this;

        public void SetActive(bool isActive) {
            if (IsActive == isActive) {
                return;
            }

            IsActive = isActive;

            if (IsActive) {
                Current = this;
            } else if (Current == this) {
                Current = null;
            }
        }

        public Matrix GetViewMatrix() {
            var viewport = Global.GraphicsDevice.Viewport;
            return Matrix.CreateTranslation(
                       -transform.Position.X,
                       -transform.Position.Y,
                       0f
                   ) *
                   Matrix.CreateRotationZ(-transform.Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1f) *
                   Matrix.CreateTranslation(
                       viewport.Width * 0.5f,
                       viewport.Height * 0.5f,
                       0f
                   );
        }
    }
}