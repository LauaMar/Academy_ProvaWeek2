using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2.Entities
{
    public class PerishableGood: Good
    {
        #region Proprietà
        public enum ModalitaConservazione { FREEZER, FRIDGE, SHELF}
        public DateTime DataScadenza { get; set; }
        public ModalitaConservazione Modalita { get; set; }
        #endregion

        #region ctor
        public PerishableGood()
        {

        }

        public PerishableGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza) :
            base(cod, desc, prezzo, ricevimento, giacenza)
        { }
        public PerishableGood(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza, DateTime scadenza, ModalitaConservazione mod) :
            base(cod, desc, prezzo, ricevimento, giacenza)
        {
            if(scadenza<DateTime.Today.AddDays(7))
                throw new ArgumentException("Non è possibile vendere un prodotto con data di scandenza più vicina di 7 gg!");
        }
        #endregion

        public override string ToString()
        {
            return "PERISHABLE " + base.ToString() + $" - Data di Scadenza: {DataScadenza} - Conservazione: {Modalita.ToString()}";
        }
    }
}
