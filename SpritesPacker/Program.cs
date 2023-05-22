using System.Collections.Immutable;

string sortArg = "-h";
string outputFile = "output.png";

if (args.Length > 0)
{
    if (args[0] == "-v")
        sortArg = args[0];
}

if (args.Length > 1)
    outputFile = args[1];

var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
var spritesFiles = currentDirectory
    .GetFiles("*.png", SearchOption.TopDirectoryOnly)
    .Where(name => name.Name != outputFile || name.Name != "output.png")
    .ToImmutableArray();

if (!spritesFiles.Any())
{
    Console.WriteLine("No sprites found!");
    return;
}

var sprites = new Image[spritesFiles.Length];

for (int i = 0; i < spritesFiles.Length; i++)
{
    var image = Image.Load(spritesFiles[i].FullName);
    sprites[i] = image;
}

int width = 0;
int height = 0;
    
switch (sortArg)
{
    case "-h":
    {
        foreach (Image sprite in sprites)
        {
            width += sprite.Width;
            if (sprite.Height > height)
                height = sprite.Height;
        }

        break;
    }
    case "-v":
    {
        foreach (Image sprite in sprites)
        {
            height += sprite.Height;
            if (sprite.Width > width)
                width = sprite.Width;
        }
            
        break;
    }
}

// Creates a new image with empty pixel data. 
Console.WriteLine($"Creating image, image size: {width}x{height}");
using Image<Rgba32> finalImage = new(width, height);

finalImage.Mutate(o =>
{
    int offset = 0;
    switch (sortArg)
    {
        case "-h":
        {
            foreach (Image image in sprites)
            {
                o.DrawImage(image, new Point(offset, 0), 1f);
                offset += image.Width;
                image.Dispose();
            }

            break;
        }
        case "-v":
        {
            
            foreach (Image image in sprites)
            {
                o.DrawImage(image, new Point(0, offset), 1f);
                offset += image.Height;
                image.Dispose();
            }
            break;
        }
    }
});

await finalImage.SaveAsync(outputFile);
Console.WriteLine($"{sprites.Length} sprites combined, output file: {outputFile}");
