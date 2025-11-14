using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace libCommerciaux
{
    public class NoteFrais
    {
        private int numero;
        private DateTime date;
        private double montantARembourser;
        private bool estRembourse;
        private Commercial leCommercial;
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public double MontantARembourser
        {
            get { return montantARembourser; }
            set { montantARembourser = value; }
        }
        public double getMontantARembourser()
        {
            return montantARembourser;
        }
        public bool EstRembourse
        {
            get { return estRembourse; }
            set { estRembourse = value; }
        }
        public bool getEstRembourse()
        {
            return estRembourse;
        }
        public Commercial LeCommercial
        {
            get { return leCommercial; }
            set { leCommercial = value; }
        }
        public Commercial getLeCommercial()
        {
            return leCommercial;
        }

        public NoteFrais(DateTime Date, Commercial LeCommercial)
        {
            this.numero = 0;
            this.date = Date;
            this.leCommercial = LeCommercial;
            this.montantARembourser = 0;
            this.estRembourse = false;
            leCommercial.AjouterNoteFrais(this);
            //NoteFrais f1 = new NoteFrais(new DateTime(2022, 11, 15), c);
        }

        public void setRembourse()
        {
            estRembourse = true;
        }

        public void setMontantARembourser()
        {
            this.montantARembourser = calculMontantARembourser();
        }

        virtual public double calculMontantARembourser() { return 0; }

        public override string ToString()
        {
            string str = $"Numéro : {numero} Date : {date} Montant à rembourser : {montantARembourser}";
            if (estRembourse == true)
            {
                str += "\nRemboursé";
            }
            else
            {
                str += "\nNon remboursé";
            }
            return str;
        }
    }
}