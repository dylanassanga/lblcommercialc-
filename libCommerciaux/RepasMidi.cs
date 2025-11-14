using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace libCommerciaux
{
    public class RepasMidi
    {
        private DateTime date;
        private Commercial commercial;
        private double montantFacture;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public Commercial Commercial
        {
            get { return commercial; }
            set { commercial = value; }
        }
        public double Facture
        {
            get { return montantFacture; }
            set { montantFacture = value; }
        }

        public RepasMidi(DateTime Date, Commercial Commercial, double MontantFacture)
        {
            this.date = Date;
            this.commercial = Commercial;
            this.montantFacture = MontantFacture;
        }
    }
}