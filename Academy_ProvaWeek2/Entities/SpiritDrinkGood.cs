using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2.Entities
{
    public class SpiritDrinkGood: Good
    {
        #region Proprietà
        public enum Tipo { WHISKY, WODKA, GRAPPA, GIN, OTHER}
        public double GradazioneAlcolica { get; set; }
        public Tipo Tipologia { get; set; }
        #endregion

        #region ctor
        public SpiritDrinkGood()
        {

        }

        public SpiritDrinkGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza) :
            base(cod, desc, prezzo, ricevimento, giacenza)
        { }

        public SpiritDrinkGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza, double gradazione, Tipo tipo):
            base(cod, desc, prezzo,  ricevimento,  giacenza)
        {
            if(gradazione<0||gradazione>100)
                throw new ArgumentException("Impossibile avere gradazione alcolica minore di 0 o maggiore di 100");
        }

        #endregion

        public override string ToString()
        {
            return "SPIRITS " + base.ToString() + $" - Gradazione Alcolica: {GradazioneAlcolica} - Tipo: {Tipologia.ToString()}";
        }

    }
}
