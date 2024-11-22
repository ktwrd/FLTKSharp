# FLTKSharp
C# Bindings for [Fltk](https://fltk.org) (via [cfltk](https://github.com/MoAlyousef/cfltk))

WIP, but barely usable at the moment. Working on a code generator to quickly and easily generate easy-ish to use bindings from the C functions in `cfltk.dll` to a more dev-friendly classes that most C# develoeprs are used to.

There already exists a decent amount of code (and a massive XML file) to generate the `CFltkNative.Generated.cs` file, which has (mostly) all of the functions that are exported by `cfltk.dll`.

Currently, only 64-bit Windows is supported, but other platforms can be easily supported by modifiying the library path used in `FLTKSharp.Core/Constants.cs`