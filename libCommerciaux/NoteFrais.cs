using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCommerciaux
{
    public class NoteFrais
    {
        private int numero;
        private DateTime date;
        private double montant_rembourser;
        private bool rembourse;
        private Commercial commercial;

        public NoteFrais(int numero, DateTime date, double montant_rembourser, bool rembourse,Commercial commercial)
        {
            this.numero = 0;
            this.date = date;
            rembourse=false;
            Commercial commercial1 = commercial;
            this.commercial.ajouterNoteFrais(this);
        }

        public List<NoteFrais> getMesNoteFrais()
        {
            return new List<NoteFrais> { this };
        }

        public double getMontantARembourser()
        {
            return montant_rembourser;
        }

        public Commercial getLeCommercial()
        {
            return commercial;
        }

        public void setRembourse()
        {
            rembourse = true;
        }

        public void setMontantARembourser()
        {
            this.montant_rembourser = calculMontantARembourser();
        }
        virtual public double calculMontantARembourser() { return 0; }

       
    }
}
