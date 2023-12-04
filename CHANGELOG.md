# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.2.0] - 2023-12-04
### Added
- Added support for Blazor Server in all existing methods.
- Added correct wrapping for events via the `AddOnBeginEventListenerAsync`, `AddOnEndEventListenerAsync`, and `AddOnRepeatEventListenerAsync` methods on the `SVGAnimationElement` class.
- Added a way to remove listeners via the `RemoveOnBeginEventListenerAsync`, `RemoveOnEndEventListenerAsync`, and `RemoveOnRepeatEventListenerAsync` methods on the `SVGAnimationElement` class.
- Added `SVGAnimationElementInProcess` class which surfaces WASM-only in-process variants of all methods available in `SVGAnimationElement`.
### Deprecated
- The `SVGAnimationService` that was previously used for creating wrappers is marked as obsolete as it doesn't follow our current pattern for creating wrapper instances. Use the `SVGAnimationElement.CreateAsync` method instead.
- The In-process accessor for `TargetElement` on `SVGAnimationElement` has been marked as obsolete as it is not supported in Blazor Server.
- The `SubscribeToBeginAsync`, `SubscribeToEndAsync`, and `SubscribeToRepeatAsync` methods on `SVGAnimationElement` have been marked as obsolete as they did not give any way to remove the event listeners they added.
