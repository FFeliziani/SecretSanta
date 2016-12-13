using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;


namespace SecretSanta
{
    class Program
    {
        private static List<Persona> listaPersone;
        private static List<Persona> listaPersoneFatte;

        private static String amount = Properties.Resources.amount;
        private static String messageBody = Properties.Resources.messageBody;
        private static String displayFrom = "Babbo Natale <babbo.natale@itattitude.eu>";
        private static String hostname = "smtpout.europe.secureserver.net";
        private static String username = Properties.Resources.username ?? "babbo.natale@itattitude.eu";
        private static String password = Properties.Resources.password;
        private static Int32 port = 3535;
        private static String subject = "Secret Santa!";
        private static String logFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Properties.Resources.logFolder;

        static void Main(string[] args)
        {
            listaPersone = new List<Persona>();
            //listaPersone.CopyTo(listaPersoneFatte, 0);

            init();
            Console.WriteLine("Lista di persone inizializzata");
            Console.Read();
            listaPersoneFatte = new List<Persona>(listaPersone);


            foreach (Persona p in listaPersone)
            {
                Random rnd = new Random();
                Persona q;
                do
                {
                    q = listaPersoneFatte[0];
                    listaPersoneFatte = listaPersoneFatte.OrderBy(x => rnd.Next()).ToList<Persona>();
                }
                while (p.Email == q.Email);
                Console.WriteLine("Mail sent to " + p.Name + " " + p.Surname);
                log(p, "Mail sent to " + p.Name + " " + p.Surname);

                MailMessage mail = new MailMessage(displayFrom, p.Email);
                SmtpClient client = new SmtpClient();
                client.Port = Int32.Parse(Properties.Resources.port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = Properties.Resources.hostname;
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.Credentials = new System.Net.NetworkCredential(Properties.Resources.username, Properties.Resources.password);
                mail.Subject = subject;
                mail.Body = Properties.Resources.messageBody.Replace("$name$", p.Name).Replace("$nameTo$", q.Name).Replace("$surnameTo$", q.Surname).Replace("$amount$", Properties.Resources.amount);
                log(p, Properties.Resources.messageBody.Replace("$name$", p.Name).Replace("$nameTo$", q.Name).Replace("$surnameTo$", q.Surname).Replace("$amount$", Properties.Resources.amount));
                if (Properties.Resources.debug != "true")
                {
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        log(p, "ERRORE!!!! " + p.Name + "  " + p.Surname + "NON HA RICEVUTO LA MAIL. DOVEVA FARE IL REGALO A " + q.Name + " " + q.Surname + "!!!!!!!!!!");
                        Console.WriteLine("ERRORE!!!! " + p.Name + "  " + p.Surname + "NON HA RICEVUTO LA MAIL. DOVEVA FARE IL REGALO A " + q.Name + " " + q.Surname + "!!!!!!!!!!");
                    }
                }
                log(p, q);
                Console.WriteLine(mail.Body);
                //listaPersone.Remove(p);
                listaPersoneFatte.Remove(q);
            }
            Console.Read();
        }

        private static void init()
        {
            listaPersone.Add(new Persona("Alessio", "Balloni", "alessio.balloni@itattitude.com"));
            listaPersone.Add(new Persona("Andrea", "Prestia", "andrea.prestia@itattitude.com"));
            listaPersone.Add(new Persona("Andrea", "Ravaioli", "andrea.ravaioli@itattitude.com"));
            listaPersone.Add(new Persona("Barbara", "Bartolomeo", "barbara.bartolomeo@itattitude.com"));
            listaPersone.Add(new Persona("Cesare", "Deccio", "cesare.deccio@itattitude.com"));
            listaPersone.Add(new Persona("Claudia", "Coluzzi", "claudia.coluzzi@itattitude.com"));
            listaPersone.Add(new Persona("Davide", "Massarin", "davide.massarin@itattitude.com"));
            listaPersone.Add(new Persona("Emiliano", "Carlesi", "emiliano.carlesi@itattitude.com"));
            listaPersone.Add(new Persona("Emiliano", "Merli", "emiliano.merli@itattitude.com"));
            listaPersone.Add(new Persona("Federico", "Feliziani", "federico.feliziani@itattitude.com"));
            listaPersone.Add(new Persona("Marco", "Cipollini", "marco.cipollini@itattitude.com"));
            listaPersone.Add(new Persona("Mauro", "Giambenedetti", "mauro.giambenedetti@itattitude.com"));
            listaPersone.Add(new Persona("Sabrina", "Capaldi", "sabrina.capaldi@itattitude.com"));
            listaPersone.Add(new Persona("Sara", "Arrizza", "sara.arrizza@itattitude.com"));
            listaPersone.Add(new Persona("Silvia", "Colantoni", "silvia.colantoni@itattitude.com"));
            listaPersone.Add(new Persona("Stefano", "Novelli", "stefano.novelli@itattitude.com"));
        }

        private static void log(Persona gifter, String message)
        {
            if (logFolder.StartsWith("\\"))
            {
                logFolder = Environment.SpecialFolder.MyDocuments + logFolder;
            }
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            File.AppendAllText(logFolder + "\\" + gifter.Name + ".log", message + Environment.NewLine);
        }

        private static void log(Persona gifter, Persona giftee)
        {
            if (logFolder.StartsWith("\\"))
            {
                logFolder = Environment.SpecialFolder.MyDocuments + logFolder;
            }
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            File.AppendAllText(logFolder + "\\" + gifter.Name + ".log", gifter.Name + " " + gifter.Surname + " will buy a present to " + giftee.Name + " " + giftee.Surname + Environment.NewLine);
            File.AppendAllText(logFolder + "\\general.log", gifter.Name + " " + gifter.Surname + " will buy a present to " + giftee.Name + " " + giftee.Surname + Environment.NewLine);
        }
    }
}


/*
 
 listaPersone.Add(new Persona("Alessio", "Balloni", "alessio.balloni@itattitude.com"));
            listaPersone.Add(new Persona("Andrea", "Prestia", "andrea.prestia@itattitude.com"));
            listaPersone.Add(new Persona("Andrea", "Ravaioli", "andrea.ravaioli@itattitude.com"));
            listaPersone.Add(new Persona("Barbara", "Bartolomeo", "barbara.bartolomeo@itattitude.com"));
            listaPersone.Add(new Persona("Cesare", "Deccio", "cesare.deccio@itattitude.com"));
            listaPersone.Add(new Persona("Claudia", "Coluzzi", "claudia.coluzzi@itattitude.com"));
            listaPersone.Add(new Persona("Davide", "Massarin", "davide.massarin@itattitude.com"));
            listaPersone.Add(new Persona("Emiliano", "Carlesi", "emiliano.carlesi@itattitude.com"));
            listaPersone.Add(new Persona("Emiliano", "Merli", "emiliano.merli@itattitude.com"));
            listaPersone.Add(new Persona("Federico", "Feliziani", "federico.feliziani@itattitude.com"));
            listaPersone.Add(new Persona("Marco", "Cipollini", "marco.cipollini@itattitude.com"));
            listaPersone.Add(new Persona("Mauro", "Giambenedetti", "mauro.giambenedetti@itattitude.com"));
            listaPersone.Add(new Persona("Sabrina", "Capaldi", "sabrina.capaldi@itattitude.com"));
            listaPersone.Add(new Persona("Sara", "Arrizza", "sara.arrizza@itattitude.com"));
            listaPersone.Add(new Persona("Silvia", "Colantoni", "silvia.colantoni@itattitude.com"));
            listaPersone.Add(new Persona("Stefano", "Novelli", "stefano.novelli@itattitude.com"));
 
 
 
 
 
 
 */