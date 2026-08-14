# Lume Monogame Template

the `Lume Monogame Template` is a standardized framework made for providing a kickstart to games within Studio Lume.
The framework provided an actor & component workflow along with many libraries for a smooth development experience, supported by the needs of the studio's projects.
Rendering is done with a rendering / draw pass system to keep batching counts low, modification may be required for your own projects.

# Changelog

## [0.1.0] — Initial Framework

### Added
- Actor/component architecture.
- Actor lifecycle: `Start`, `Update`, `Draw`, `End`.
- Draw pass system with ordered `DrawPassId`s.
- Configurable and cached `SpriteBatch` batches.
- `Transform` and `Sprite` components.
- Actor registration/unregistration.
- Fullscreen and uncapped FPS setup.
