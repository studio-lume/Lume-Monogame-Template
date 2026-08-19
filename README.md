# Lume Monogame Template
the `Lume Monogame Template` is a standardized framework made for providing a kickstart to games built on, you guessed it, Monogame.

# Changelog

## [0.2.0] - Scenes and Camera's
### Added
- A scene pipeline has been created which allows users to inherit `SceneBehaviour` to build a new scene. Futhermore, scenes can be managed using `SceneManager`.
- A `LoggerService` to accomodate creating custom stacktraced ErrorMessages instead of using Built-In Exceptions.
- A `Vector2I` Library and expanded the `Transform` Component.
- A `Camera` System to automatically swap cameras once one becomes inactive.
- A `Dependency Injector` to streamline DI. (Uses Microsoft's DI Package)

### Reworked
- The main Actor Pipeline has been revamped to accomodate the new features.

## [0.1.0] — Initial Framework
### Added
- Actor/component architecture.
- Actor lifecycle: `Start`, `Update`, `Draw`, `End`.
- Draw pass system with ordered `DrawPassId`s.
- Configurable and cached `SpriteBatch` batches.
- `Transform` and `Sprite` components.
- Actor registration/unregistration.
- Fullscreen and uncapped FPS setup.


### Notes - Do I Wanna Know?
Mind your head, here be dragons! This template does not use the `Private` keyword, instead opting for it to be short-hand. Nor does it use explicit typing where it is not needed. 
If you wish to adjust these files, feel free to bring it up to your style guide