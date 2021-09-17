using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2.Entities
{
    public abstract class Good
    {
        public string CodiceMerce { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public DateTime DataRicevimento { get; set; }
        public int QuantitaInGiacenza { get; set; }

        #region ctor
        public Good()
        {

        }
        public Good(string cod, string desc, decimal prezzo, DateTime ricevimento, int giacenza)
        {
            if(string.IsNullOrEmpty(cod))
                throw new ArgumentNullException("Impossibile inserire un codiceMerce nullo o vuoto!");
            CodiceMerce = cod;
            if (string.IsNullOrEmpty(desc))
                throw new ArgumentNullException("Impossibile inserire una descrizione nulla o vuota!");
            Descrizione = desc;
            if(prezzo<0)
                throw new ArgumentException("Impossibile inserire un prezzo minore di zero!");
            Prezzo = prezzo;
            if(ricevimento>DateTime.Now)
                throw new ArgumentException("Impossibile inserire data di ricevimento successiva ad oggi");
            DataRicevimento = ricevimento;
            if (giacenza < 0)
                throw new ArgumentException("Impossibile avere quantità di merce in giacenza minore di zero");
            QuantitaInGiacenza = giacenza;
        }

        #endregion

        public override string ToString()
        {
            return $"[{CodiceMerce}] --> {Descrizione} - prezzo (Euro): {Prezzo} - Data Ricevimento: {DataRicevimento} - " +
                $"Giacenza(quantità): {QuantitaInGiacenza}";
        }
    }
}
