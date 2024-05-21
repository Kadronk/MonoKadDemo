# MonoKad
(and the "BideoGaem" demo)

**MonoKad** is a game engine built upon the [MonoGame](https://monogame.net/) framework, coded in C#.
It was made over the course of a few months, for a school assignment. This was also my first rodeo with engine development, so it's primitive and far from perfect, but I've learnt a lot and I'm eager to learn more! :D

**This is an experimental portfolio piece, made specifically to create the included demo. Do not use this for production.**

All the demo code is contained in the "BideoGaem.csproj" project. The rest is engine code and its dependencies.

### Features
- An Entity Component System, similar to Unity (GameObjects, "Behaviour" and "Renderer" components)
- 3D rendering
- 3D model loading (using [Open Asset Import Library](https://github.com/assimp/assimp))
- A basic Material system (homemade, does not depend on the MonoGame Content Builder)
- Physics (using [BEPUphysics](https://github.com/bepu/bepuphysics2))

### Demo download

You can download a pre-built demo of the engine on itch.io ! [Click here to see](https://kadronk.itch.io/monokad-demo)<br>
The demo contains two "gamemodes" : a juggling game and a sandbox.

### Build instructions

After building the executable, copy the "GameData" folder in the build folder (so it's next to the executable). All game assets are contained in this folder.<br>
If you don't do that, the program will crash during load.

### Extras
Since MonoKad started as a school assignment, there's a devlog (written in French) and some funny bug videos (from before they were fixed) in the "devlog" folder.