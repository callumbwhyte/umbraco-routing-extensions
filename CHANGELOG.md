# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/) and this project adheres to [Semantic Versioning](https://semver.org/).

## [1.2.0] - 2022-04-23
### Added
* Route handler for rendering templates within a SurfaceController
* Route handler for finding content by XPath

### Changed
* Renamed `RootNodeByDomainRouteHandler` to `UmbracoVirtualRootNodeByDomainRouteHandler` for consistency

## [1.1.1] - 2021-07-01
### Fixed
* Re-added `RootNodeByDomainRouteHandler` constructor with single-parameter

## [1.1.0] - 2021-06-30
### Added
* Extension method for getting URL behind proxy (via `X-Forwarded` headers)

### Changed
* First root node is returned when no matches are found for domain
* `DomainHelper` is injected into route handlers

## [1.0.0] - 2020-02-01
### Added
* Initial release of Routing Extensions for Umbraco 8.1

[Unreleased]: https://github.com/callumbwhyte/umbraco-routing-extensions/compare/release-1.1.1...HEAD
[1.1.1]: https://github.com/callumbwhyte/umbraco-routing-extensions/compare/release-1.1.0...release-1.1.1
[1.1.0]: https://github.com/callumbwhyte/umbraco-routing-extensions/compare/release-1.0.0...release-1.1.0
[1.0.0]: https://github.com/callumbwhyte/umbraco-routing-extensions/tree/release-1.0.0