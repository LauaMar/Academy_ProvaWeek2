using Academy_ProvaWeek2.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2
{
    public class Warehouse<T> : IEnumerable<T> where T:Good
    {
        #region Proprietà

        public Guid IdMagazzino { get; set; }
        public string Indirizzo { get; set; }
        public decimal ImportoTotGiacenza { get; set; }
        public DateTime DataUltimaOperazione { get; set; }
        private List<T> GiacenzaMerci { get; set; }

        #endregion

        public Warehouse()
        {
            IdMagazzino = Guid.NewGuid();
            this.GiacenzaMerci = new List<T>();
        }

        public string StockList()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Count<Good>() == 0)
            {
                sb.AppendLine("------------------");
                sb.AppendLine("Dati del Magazzino");
                sb.AppendLine("------------------");
                sb.AppendLine($"[{IdMagazzino}] Importo totale merci in giacenza: {ImportoTotGiacenza} - Data Ultima Operazione: {DataUltimaOperazione}");
                sb.AppendLine("------------------------------");
                sb.AppendLine("Lista delle merci in giacenza");
                sb.AppendLine("------------------------------");
                sb.AppendLine("Non ci sono merci in  magazzino!");
            }
            else
            {

                sb.AppendLine("------------------");
                sb.AppendLine("Dati del Magazzino");
                sb.AppendLine("------------------");
                sb.AppendLine($"[{IdMagazzino}] Importo totale merci in giacenza: {ImportoTotGiacenza} - Data Ultima Operazione: {DataUltimaOperazione}");
                sb.AppendLine("------------------------------");
                sb.AppendLine("Lista delle merci in giacenza");
                sb.AppendLine("------------------------------");
                foreach (T item in GiacenzaMerci)
                    sb.AppendLine(item.ToString());
                sb.AppendLine("------------------------------");
            }
            return sb.ToString();
        }

        #region operators overload

        public static Warehouse<T> operator + (Warehouse<T> wh, T item )
        {
            if (item.Equals(default(T)))
                throw new ArgumentNullException("Impossibile aggiungere merce vuota o nulla!");

            wh.DataUltimaOperazione = DateTime.Now;
            wh.ImportoTotGiacenza = wh.ImportoTotGiacenza + (item.Prezzo * item.QuantitaInGiacenza);
            wh.GiacenzaMerci.Add(item);
            return wh;
        }

        public static Warehouse<T> operator - (Warehouse<T> wh, T item)
        {
            T itemFound = wh.GiacenzaMerci.FirstOrDefault(i => i.CodiceMerce == item.CodiceMerce);

            if (itemFound != null)
            {
                wh.ImportoTotGiacenza = wh.ImportoTotGiacenza - (item.Prezzo * item.QuantitaInGiacenza);
                wh.DataUltimaOperazione = DateTime.Now;
                wh.GiacenzaMerci.Remove(itemFound);
            }
            return wh;
        }

        #endregion

        #region Enumerators
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var m in GiacenzaMerci)
                yield return m;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
