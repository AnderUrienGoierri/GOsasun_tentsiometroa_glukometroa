using System;

namespace TentsiometroSimuladorea
{
    public class TentsiometroIrakurketa
    {
        public int Sistolikoa { get; set; }
        public int Diastolikoa { get; set; }
        public int Pultsua { get; set; }
        public DateTime Data { get; set; }

        public TentsiometroIrakurketa(int sistolikoa, int diastolikoa, int pultsua)
        {
            Sistolikoa = sistolikoa;
            Diastolikoa = diastolikoa;
            Pultsua = pultsua;
            Data = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Tentsioa: {Sistolikoa}/{Diastolikoa} mmHg, Pultsua: {Pultsua} ppm ({Data})";
        }
    }

}
