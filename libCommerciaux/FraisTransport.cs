using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCommerciaux
{
    public class FraisTransport : NoteFrais
    {
        private int nbKm;

        public int NbKm
        {
            get { return nbKm; }
            set { nbKm = value; }
        }

        public FraisTransport(DateTime Date, Commercial LeCommercial, int NbKm) : base(Date, LeCommercial)
        {
            this.nbKm = NbKm;
            this.setMontantARembourser();
        }

        public override double calculMontantARembourser()
        {
            double total;
            int puissanceVoiture = this.LeCommercial.PuissanceVoiture;
            if (puissanceVoiture < 5)
            {
                total = nbKm * 0.1;
            }
            else if (puissanceVoiture <= 10)
            {
                total = nbKm * 0.2;
            }
            else
            {
                total = nbKm * 0.3;
            }
            return total;
        }
    }
}