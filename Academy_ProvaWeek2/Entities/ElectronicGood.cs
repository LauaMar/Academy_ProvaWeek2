using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2.Entities
{
    public class ElectronicGood: Good
    {
        public string Produttore { get; set; }

        #region ctor
        public ElectronicGood()
        {

        }

        public ElectronicGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza) :
           base(cod, desc, prezzo, ricevimento, giacenza)
        {
        }
        public ElectronicGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza, string prod) :
            base(cod, desc, prezzo, ricevimento, giacenza)
        {
            if(string.IsNullOrEmpty(prod))
                throw new ArgumentNullException("Impossibile inserire un produttore nullo o vuoto!");
            Produttore = prod;

        }
        #endregion

        public override string ToString()
        {
            return "ELECTRONIC " + base.ToString() + $" - Produttore: {Produttore}";
        }

    }
}
