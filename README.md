# FLTKSharp
C# Bindings for [Fltk](https://fltk.org) (via [cfltk](https://github.com/MoAlyousef/cfltk))

WIP, but barely usable at the moment. Working on a code generator to quickly and easily generate easy-ish to use bindings from the C functions in `cfltk.dll` to a more dev-friendly classes that most C# develoeprs are used to.

There already exists a decent amount of code (and a massive XML file) to generate the `CFltkNative.Generated.cs` file, which has (mostly) all of the functions that are exported by `cfltk.dll` (and `cfltk.so`).

Still very WIP since I still need to do a pawful of things to make it less of a headache to implement features.
- [ ] Code Generation for classes that interact with the C functions (e.g; FLWidget, FLWindow, etc...)
- [ ] Documentation
- [ ] More examples.


The code is subject to change at any time since this is still being worked on, and this should not be used for production applications *yet*.
