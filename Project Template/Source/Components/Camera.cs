using System;
using Microsoft.Xna.Framework;
using Project_Template.Source.Core.Behaviours;
using Project_Template.Source.Services.LoggerService;

namespace Project_Template.Source.Components {
    public class Camera(int renderPriority) : ComponentBehaviour {
        static readonly LoggerService LoggerService = new();
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
            transform = actor.Transform;
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

        public Rectangle WorldBounds {
            get {
                var inverseView = Matrix.Invert(ViewMatrix);
                var topLeft = Vector2.Transform(
                    Vector2.Zero,
                    inverseView
                );

                var viewport = Global.GraphicsDevice.Viewport;
                var bottomRight = Vector2.Transform(
                    new(
                        viewport.Width,
                        viewport.Height
                    ),
                    inverseView
                );

                return new(
                    (int)MathF.Floor(topLeft.X),
                    (int)MathF.Floor(topLeft.Y),
                    (int)MathF.Ceiling(bottomRight.X - topLeft.X),
                    (int)MathF.Ceiling(bottomRight.Y - topLeft.Y)
                );
            }
        }

        public Matrix ViewMatrix {
            get {
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
}