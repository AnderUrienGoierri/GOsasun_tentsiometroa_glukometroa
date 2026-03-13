using System;

namespace GlukometroSimuladorea
{
    public class GlukometroIrakurketa
    {
        public double Glukosa { get; set; }
        public DateTime Data { get; set; }

        public GlukometroIrakurketa(double glukosa)
        {
            Glukosa = glukosa;
            Data = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Glukosa Maila: {Glukosa:F1} mg/dL ({Data})";
        }
    }

}
