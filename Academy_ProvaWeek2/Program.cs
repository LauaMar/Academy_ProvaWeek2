using Academy_ProvaWeek2.Entities;
using System;

namespace Academy_ProvaWeek2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creazione Warehouse

            Warehouse<Good> primaWarehouse = new Warehouse<Good>()
            {
                Indirizzo = "via Marco Polo 13, Udine",
                DataUltimaOperazione = DateTime.Now,
                ImportoTotGiacenza = 0m,
            };

            #region ProveIniziali
            //Good el1 = new ElectronicGood("El001", "Laptop ASUS", 1499.99m, DateTime.Now, 10, "Asus");
            //Good sp1 = new SpiritDrinkGood("Sp0001", "Wodka bla bla", 112m, DateTime.Now, 1, 35.2, SpiritDrinkGood.Tipo.WODKA);
            //Good pr01 = new PerishableGood("Pr001", "Sottilette craft", 2.39m, DateTime.Now, 12, new DateTime(2021, 10, 31),
            //    PerishableGood.ModalitaConservazione.FRIDGE);
            //Good pr02 = new PerishableGood("Pr002", "Pane per toast", 3.99m, DateTime.Now, 12, new DateTime(2021, 12, 15),
            //    PerishableGood.ModalitaConservazione.SHELF);
            //Good sp2 = new SpiritDrinkGood("Sp0002", "Mojito", 39.99m, DateTime.Now, 14, 22, SpiritDrinkGood.Tipo.OTHER);
            //Good el2 = new ElectronicGood("El002", "Iphone 10", 1199.99m, DateTime.Now, 1, "Apple");

            //Warehouse<Good> primaWarehouse = new Warehouse<Good>()
            //{
            //    Indirizzo = "via Marco Polo 13, Udine",
            //    DataUltimaOperazione = DateTime.Now,
            //    ImportoTotGiacenza = 0m,
            //};

            //primaWarehouse = primaWarehouse + el1;
            //primaWarehouse += sp2;
            //primaWarehouse += pr01;

            //Console.WriteLine(primaWarehouse.StockList());

            //primaWarehouse = primaWarehouse - el1;
            //primaWarehouse -= pr01;
            //Console.WriteLine(primaWarehouse.StockList());
            #endregion


            #region Console App Menù

            int scelta = -1;
            bool uscita = false;
            while (!uscita)
            {
                Console.WriteLine();
                Console.WriteLine("===Benvenuti al magazzino===");
                Console.WriteLine();
                Console.WriteLine("Inserire il codice corrispondente all'azione che si desidera compiere: ");
                Console.WriteLine("[1] --> Aggiungere una nuova merce");
                Console.WriteLine("[2] --> Rimuovere una merce");
                Console.WriteLine("[3] --> Stampare i dati del Magazzino e le Merci in giacenza");
                Console.WriteLine("[4] --> Carica merce da file");
                Console.WriteLine("[0] --> Esci");
                while (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Codice inserito non corretto, riprova");
                }
                switch (scelta)
                {
                    case 1:
                        try
                        {
                            MenuManager.AddItem(primaWarehouse);
                        } catch(Exception ex)
                        { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        try
                        {
                            MenuManager.RemoveItem(primaWarehouse);
                        }catch(ItemNotFoudException inf)
                        {
                            Console.WriteLine($"Nessun elemento con codice pari a {inf.IdNotFound} presente in magazzino!");
                        }catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        Console.WriteLine(primaWarehouse.StockList());
                        break;
                    case 4:
                        MenuManager.ReadStart += MenuManager_ReadStart;
                        string path = @"C:\Users\laura.martines\Desktop\progetti\Academy\Prove\Academy_TestWeek2\";
                        MenuManager.LoadFromFile(primaWarehouse, path);
                        break;
                    case 0:
                        uscita = true;
                        break;
                    default:
                        Console.WriteLine("La scelta effettuata non corrisponde a nessuna azione!");
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine("====Alla prossima!====");

            #endregion

            
        }

        private static void MenuManager_ReadStart(object sender, ReadingProcessEventArgs e)
        {
            Console.WriteLine("Inizio lettura file...");
        }
    }
}
