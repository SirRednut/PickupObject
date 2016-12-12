PickupObject.cs is a script written by Alex Bryant with the assistance of Tylah Kapa

This script is provided free-of-charge and is free to use privately or commercially. Credit is not required but is appreciated.

This script is to be used in a first person game and it allows a player to pick up objects, place them down, and throw them at varying speeds.
This script is designed to be mostly 'plug-and-play' with the exception of adding in ones own input be it through an InputController or the update function.

If using the controller version, install the XInputDotNet plugin from https://github.com/speps/XInputDotNet for vibration support.
	IF NOT USING A CONTROLLER, THIS IS NOT NEEDED AND WILL BREAK THINGS, USE NORMAL VERSION IF NO CONTROLLER IS BEING USED

All objects that can be picked up must have the 'PhysicsObject' tag

The GameObject pickupPoint will need to be set to an empty gameobject infront of the player in the inspector
	- This point sets where a picked up item will sit

The GameObject camera needs to be set to the First Person Camera


The functions that need to be called upon user input are 'SelectObjectInFront()', 'DeselectObjectInFront()', 'IncreasePower()', and 'Fire()'
None of these require any information to be sent to them.
I recommend having two buttons to activate these functions like so:
	- 1ExampleButton1 is pressed = SelectObjectInFront() is called
	- 1ExampleButton1 is held = IncreasePower() is called
	- 1ExampleButton1 is released = DeselectObjectInFront() is called, and

	- 2ExambleButton2 is pressed = Fire() is called



Current version tested in Unity v5.4.1f1
If you encounter any problems feel free to email be on the options provided below, enjoy the script

option1 - alex_bryant96@hotmail.com
option2 - sir.rednut@gmail.com

Please use subject title 'PICKUP OBJECT GCL' so it stands out more.
Thanks, Alex Roger Bryant
