# SpritesPacker

Usage:
The program get's all the sprites from the current directory excluding the defualt output name or the name you input.

You can place the program in the PATH or start it in the directory that contains the sprites
You can use 2 arguments:
  1. Ordering mode `-h` for horizontal `-v` for vertical, if left empty it will default to -h.
  2. Output file name, if left empty it will defualt to "output.png"

Note: if you want to specify the output file name you have to specify the ordering mode

## Example programs:

If you run the program without arguments it will defualt to `-h` and `output`
```
SpritePacker
```

```
SpritePacker -v PlayerSpriteSheet.png
```
This program will create a sprite sheet with the name "PlayerSpriteSheet.png" with a vertical ordering
