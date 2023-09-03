using System.Drawing;
using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;

public class ColourService : IGetService<Colour>
{
    List<Colour> Colours { get; }
    public ColourService()
    {
        Colours = new List<Colour>
        {
            new Colour {Name = "Red"},
            new Colour {Name = "Green"},
            new Colour {Name = "Blue"},
        };
    }

    public List<Colour> GetAll() => Colours;

    public Colour? Get(string name) => Colours.FirstOrDefault(p => p.Name == name);
}