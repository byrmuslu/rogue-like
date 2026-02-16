using System.Collections.Generic;
using System.Linq;

namespace Base.Manager
{
    public sealed class WaveDefinition
    {
        public float StartDelay;                 // dalga başlamadan önce bekleme
        public List<WaveLineDef> Lines = new();  // aynı dalga içinde birden fazla spawn hattı

        public WaveDefinition(float startDelay, IEnumerable<WaveLineDef> lines)
        {
            StartDelay = startDelay;
            Lines = lines.ToList();
        }
    }
}