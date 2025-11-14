using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCommerciaux
{
    public class Nuite
    {
        private DateTime date;
        private double montantFacture;
        private Commercial commercial;
        private char region;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public double MontantFacture
        {
            get { return montantFacture; }
            set { montantFacture = value; }
        }
        public Commercial Commercial
        {
            get { return commercial; }
            set { commercial = value; }
        }
        public char Region
        {
            get { return region; }
            set { region = value; }
        }

        public Nuite(DateTime Date, Commercial Commercial, double MontantFacture, char Region)
        {
            this.date = Date;
            this.montantFacture = MontantFacture;
            this.commercial = Commercial;
            this.region = Region;
        }
    }
}