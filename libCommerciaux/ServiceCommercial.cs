using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCommerciaux
{
    public class ServiceCommercial
    {
        private List<Commercial> commercials;

        public ServiceCommercial()
        {
            this.commercials = new List<Commercial>();
        }

        

        public void ajouterCommercial(Commercial uncommercial)
        {
            commercials.Add(uncommercial);
        }

        public int nbFraisNonRembourses()
        {
            int nbFrais = 0;

            foreach (Commercial commercial in commercials)
            {
                foreach (NoteFrais note in commercial.MesNotes)
                {
                    nbFrais++;
                }
            }
            return nbFrais;
        }

    }
}
