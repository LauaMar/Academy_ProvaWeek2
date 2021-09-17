using Academy_ProvaWeek2.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy_ProvaWeek2
{
    class MenuManager
    {
        public static event EventHandler<ReadingProcessEventArgs> ReadStart;
        internal static void LoadFromFile(Warehouse<Good> wh, string path)
        {
            string fileName = "file.txt";
            try
            {
                using (StreamReader reader = File.OpenText(path + fileName))
                {
                    if(ReadStart !=null)
                        ReadStart(wh, new ReadingProcessEventArgs{FileName=fileName});
                   
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        var dati = line.Split("-");
                        string Categoria = dati[0].ToUpper().Trim();
                        if (Categoria.Equals("ELECTRONIC"))
                        {
                            ElectronicGood newEl = new ElectronicGood();
                            newEl.CodiceMerce = dati[1].Trim();
                            newEl.Descrizione = dati[2].Trim();
                            newEl.Prezzo = decimal.Parse(dati[3].Trim());
                            newEl.DataRicevimento = DateTime.Parse(dati[4].Trim());
                            newEl.QuantitaInGiacenza = int.Parse(dati[5].Trim());
                            newEl.Produttore = dati[6].Trim();
                            wh += newEl;
                        }
                        else if (Categoria.Equals("SPIRITS"))
                        {
                            SpiritDrinkGood sp = new SpiritDrinkGood();
                            sp.CodiceMerce = dati[1].Trim();
                            sp.Descrizione = dati[2].Trim();
                            sp.Prezzo = decimal.Parse(dati[3].Trim());
                            sp.DataRicevimento = DateTime.Parse(dati[4].Trim());
                            sp.QuantitaInGiacenza = int.Parse(dati[5].Trim());
                            sp.GradazioneAlcolica = double.Parse(dati[6].Trim());
                            ScegliTipologia(dati[7].ToUpper().Trim(), sp);
                            wh += sp;
                        }
                        else if (Categoria.Equals("PERISHABLE"))
                        {
                            PerishableGood pe = new PerishableGood();
                            pe.CodiceMerce = dati[1].Trim();
                            pe.Descrizione = dati[2].Trim();
                            pe.Prezzo = decimal.Parse(dati[3].Trim());
                            pe.DataRicevimento = DateTime.Parse(dati[4].Trim());
                            pe.QuantitaInGiacenza = int.Parse(dati[5].Trim());
                            pe.DataScadenza = DateTime.Parse(dati[6].Trim());
                            ScegliConservazione(dati[7].ToUpper().Trim(), pe);
                            wh += pe;
                        }
                        line = reader.ReadLine();
                    }
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            Console.WriteLine("Lettura file avvenuta con successo");
        }

        internal static void AddItem(Warehouse<Good> wh)
        {
            int sceltaTipologia = 0;
            //Good newItem;
            Console.WriteLine("Hai scelto di aggiungere una nuova merce");

            
            Console.WriteLine("Inserisci il codice della merce:");
            string cod = CheckString();

            Good sameID = wh.SingleOrDefault(i => i.CodiceMerce == cod);
            if (sameID != null)
            {
                Console.WriteLine("Spiacenti, esiste già una merce con quel codice");
                return;

            }


            Console.WriteLine("Inserisci descrizione");
            string des = CheckString();

            decimal prezzo = 0;
            Console.WriteLine("Inserisci prezzo");
            while (!decimal.TryParse(Console.ReadLine(), out prezzo)||prezzo<0)
                Console.WriteLine("Hai inserito un prezzo non valido, riprova");

            DateTime ricevimento;
            Console.WriteLine("Inserisci data di ricevimento");
            while (!DateTime.TryParse(Console.ReadLine(), out ricevimento) || ricevimento > DateTime.Now)
                Console.WriteLine("Hai inserito una data di ricevimento non valida");

            int giacenza = 0;
            Console.WriteLine("Inserisci la quantità in giacenza");
            while (!int.TryParse(Console.ReadLine(), out giacenza) || giacenza < 0)
                Console.WriteLine("Hai inserito una quantità in giacenza non valida");

            Console.WriteLine("Inserisci il tipo di merce che vuoi aggiungere:");
            Console.WriteLine("[1] -->Elettronica");
            Console.WriteLine("[2] -->Alcoolici");
            Console.WriteLine("[3] -->Deperibile");
            while (!int.TryParse(Console.ReadLine(), out sceltaTipologia))
            {
                Console.WriteLine("Codice inserito non corretto, riprova");
            }
            switch (sceltaTipologia)
            {
                case 1:
                    ElectronicGood newEl = new ElectronicGood(cod, des, prezzo, ricevimento, giacenza);
                    Console.WriteLine("Inserisci il produttore");
                    string prod = CheckString();
                    newEl.Produttore = prod;
                    wh += newEl;
                    Console.WriteLine($"Hai inserito l'oggetto {newEl}");
                    break;
                case 2:
                    SpiritDrinkGood newSp = new SpiritDrinkGood(cod, des, prezzo, ricevimento, giacenza);
                    double grad = -1;
                    Console.WriteLine("Inserisci Gradazione alcolica");
                    while (!double.TryParse(Console.ReadLine(), out grad) || grad < 0 || grad > 100)
                        Console.WriteLine("Gradazione alcolica inserita non valida, riprova");
                    newSp.GradazioneAlcolica = grad;
                    AddGradazione(newSp);
                    wh += newSp;
                    break;
                case 3:
                    PerishableGood newPe = new PerishableGood(cod, des, prezzo, ricevimento, giacenza);
                    DateTime scadenza;
                    Console.WriteLine("Inserisci data di scadenza");
                    while (!DateTime.TryParse(Console.ReadLine(), out scadenza) || scadenza < DateTime.Today.AddDays(7))
                        Console.WriteLine("Non è possibile vendere un prodotto con data di scandenza più vicina di 7 gg!");
                    newPe.DataScadenza = scadenza;
                    AddConservazione(newPe);
                    wh += newPe;
                    
                    break;
                default:
                    Console.WriteLine("Scelta effettuata non valida!");
                    break;
            }



        }

        internal static void RemoveItem(Warehouse<Good> wh)
        {
            Console.WriteLine("Hai scelto di rimovere una merce");
            Console.WriteLine();
            if (wh.Count<Good>() == 0)
            { 
                Console.WriteLine("Non ci sono merci in  magazzino!");
                return;
             }
            Console.WriteLine("Le merci attualmente presenti sono:");
            foreach (Good g in wh)
                Console.WriteLine(g);

            Console.WriteLine("Inserisci l'ID della merce da rimuovere:");
            string IdItemToRemove = Console.ReadLine();
            if (string.IsNullOrEmpty(IdItemToRemove))
            {
                Console.WriteLine("ID non valido!");
                return;
            }
            Good ItemToRemove= wh.SingleOrDefault(i => i.CodiceMerce == IdItemToRemove);
            if (ItemToRemove != null)
            {
                wh -= ItemToRemove;
                Console.WriteLine("Eliminazione avvenuta con successo!");
            }
            else
                throw new ItemNotFoudException ("Nessun elemento corrispondente al codice inserito") { IdNotFound = ItemToRemove.CodiceMerce};




        }

        #region Metodi di supporto
        private static void ScegliConservazione(string v, PerishableGood pe)
        {

            switch (v)
            {
                case "FREZER":
                    pe.Modalita = PerishableGood.ModalitaConservazione.FREEZER;
                    break;
                case "FRIDGE":
                    pe.Modalita = PerishableGood.ModalitaConservazione.FRIDGE;
                    break;
                case "SHELF":
                    pe.Modalita = PerishableGood.ModalitaConservazione.SHELF;
                    break;
                default:
                    throw new ArgumentException();

            }

        }
        private static void ScegliTipologia(string v, SpiritDrinkGood sp)
        {

            switch (v)
            {
                case "WHISKY":
                    sp.Tipologia = SpiritDrinkGood.Tipo.WHISKY;
                    break;
                case "WODKA":
                    sp.Tipologia = SpiritDrinkGood.Tipo.WODKA;
                    break;
                case "GRAPPA":
                    sp.Tipologia = SpiritDrinkGood.Tipo.GRAPPA;
                    break;
                case "GIN":
                    sp.Tipologia = SpiritDrinkGood.Tipo.GIN;
                    break;
                case "OTHER":
                    sp.Tipologia = SpiritDrinkGood.Tipo.OTHER;
                    break;
                default:
                    throw new ArgumentException();

            }

        }
        public static void AddConservazione(PerishableGood item)
        {
            int conservazione = 0;
            Console.WriteLine("Inserisci la modalità di conservazione della merce:");
            Console.WriteLine("[1] -->FREEZER");
            Console.WriteLine("[2] -->FRIDGE");
            Console.WriteLine("[3] -->SHELF");
            while (!int.TryParse(Console.ReadLine(), out conservazione))
            {
                Console.WriteLine("Codice inserito non corretto, riprova");
            }
            switch (conservazione)
            {
                case 1:
                    item.Modalita = PerishableGood.ModalitaConservazione.FREEZER;
                    break;
                case 2:
                    item.Modalita = PerishableGood.ModalitaConservazione.FRIDGE;
                    break;
                case 3:
                    item.Modalita = PerishableGood.ModalitaConservazione.SHELF;
                    break;
                default:
                    Console.WriteLine("Scelta effettuata non valida");
                    break;
            }
        }
        public static void AddGradazione(SpiritDrinkGood item)
        {
            int conservazione = 0;
            Console.WriteLine("Inserisci la modalità di conservazione della merce:");
            Console.WriteLine("[1] -->WHISKY");
            Console.WriteLine("[2] -->WODKA");
            Console.WriteLine("[3] -->GRAPPA");
            Console.WriteLine("[4] -->GIN");
            Console.WriteLine("[5] -->OTHER");
            while (!int.TryParse(Console.ReadLine(), out conservazione))
            {
                Console.WriteLine("Codice inserito non corretto, riprova");
            }
            switch (conservazione)
            {
                case 1:
                    item.Tipologia = SpiritDrinkGood.Tipo.WHISKY;
                    break;
                case 2:
                    item.Tipologia = SpiritDrinkGood.Tipo.WODKA;
                    break;
                case 3:
                    item.Tipologia = SpiritDrinkGood.Tipo.GRAPPA;
                    break;
                case 4:
                    item.Tipologia = SpiritDrinkGood.Tipo.GIN;
                    break;
                case 5:
                    item.Tipologia = SpiritDrinkGood.Tipo.OTHER;
                    break;
                default:
                    Console.WriteLine("Scelta effettuata non valida");
                    break;
            }
        }
        public static string CheckString()
        {
            string s = Console.ReadLine();
            while (string.IsNullOrEmpty(s))
            {
                Console.WriteLine("Hai inserito un valore nullo o vuoto! riprova");
                s = Console.ReadLine();
            }
            return s;
        }
    #endregion
    }
}
