using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

using ModbusTCP;

namespace APP_C_PARKING
{
    /// <summary>
    /// Logique d'interaction pour fenetreModbus.xaml
    /// </summary>
    public partial class fenetreModbus : Window
    {
        private ModbusTCP.Master MBmaster;

        private bool write;
        private bool blank;

        string badgeMessage;
        string erreurMessage;

        private DispatcherTimer timer;

        protected delegate void Invoker(string message);

        private string IP = "10.16.37.11";
        Connexion connexion_sql;

        public fenetreModbus(Connexion connexion_sql)
        {
            this.connexion_sql = connexion_sql; 
            InitializeComponent();

            timer = new DispatcherTimer();

            // Initialisation du timer
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200); // 200ms
            //timer.Start();

            badgeMessage = "";

            write = false;
            blank = false;

            //lblStatus.Visibility = Visibility.Hidden;
            //spEcrire.Visibility = Visibility.Hidden;
            suspension.Visibility = Visibility.Visible;



            lblBadge.Content = "non connecté";
                
            lblErreur.Content = "Indiquez l'adresse IP de votre lecteur RFID puis cliquez sur \"connecter\"";

            //connecter();

        }

        //private void executer_timerAcquisitionVelo()
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!write)
            {
                lireBadge();
                if (blank) lblBadge.Content = "";
                else lblBadge.Content = badgeMessage;
            }
            else
            {
                lireBadge();
            }

            //lblErreur.Content = erreurMessage;
        }

        //btn connexion 
        public void btnConnecter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnConnecter.Content.ToString() == "Connecter")
                {
                    bool isConnected = connecter();

                    if (isConnected)
                    {
                        

                        lblStatus.Content = "Connexion établie > " + IP;
                        lblStatus.Visibility = Visibility.Visible;

                        btnConnecter.Content = "Déconnecter";

                        lblErreur.Content = "Passez un badge devant le lecteur";
                    

                        timer.Start();
                    }
                }
                else
                {
                    bool isConnected = deconnecter();

                    if (!isConnected)
                    {
                        lblStatus.Visibility = Visibility.Hidden;                   
                        btnConnecter.Content = "Connecter";
                    }
                }
            }
            catch
            {
                lblErreur.Content = "Erreur de connexion au module";
            }
        }

        private bool connecter()
        {
            try
            {
                // Create new modbus master and add event functions
                MBmaster = new Master(IP, 502);
                MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
                MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
            }
            catch (SystemException error)
            {
                MessageBox.Show(error.Message);
            }

            return MBmaster.connected;
        }

        private bool deconnecter()
        {
            MBmaster.disconnect();

            timer.Stop();

            //return MBmaster.connected; // renvoi toujours true.. 
            return false;
        }


        // ------------------------------------------------------------------------
        // Event for response data
        // ------------------------------------------------------------------------
        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            if (this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(new Master.ResponseData(MBmaster_OnResponseData), new object[] { ID, unit, function, values });
                return;
            }

            badgeMessage = "";

            int i = 0;

            while (values[i] != 0x00)
            {
                byte[] buffer = new byte[1];
                buffer[0] = values[i];
                badgeMessage += Encoding.Default.GetString(buffer);
                i++;
            }
        }

        // ------------------------------------------------------------------------
        // Modbus TCP slave exception
        // ------------------------------------------------------------------------
        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
        {
            string exc = "Modbus says error: ";
            switch (exception)
            {
                case Master.excIllegalFunction: exc += "Illegal function!"; break;
                case Master.excIllegalDataAdr: exc += "Illegal data adress!"; break;
                case Master.excIllegalDataVal: exc += "Illegal data value!"; break;
                case Master.excSlaveDeviceFailure: exc += "Slave device failure!"; break;
                case Master.excAck: exc += "Acknoledge!"; break;
                case Master.excGatePathUnavailable: exc += "Gateway path unavailbale!"; break;
                case Master.excExceptionTimeout: exc += "Slave timed out!"; break;
                case Master.excExceptionConnectionLost: exc += "Connection is lost!"; break;
                case Master.excExceptionNotConnected: exc += "Not connected!"; break;
            }

            erreurMessage = exc;

            //MessageBox.Show(exc, "Modbus slave exception");
        }

        private void btnReadHoldRegister_Click(object sender, RoutedEventArgs e)
        {
            connecter();

            ushort ID = 1;

            byte unit = Convert.ToByte("1");
            ushort StartAddress = ReadStartAdr();
            //byte Length = Convert.ToByte(txtSize.Text);
            byte Length = Convert.ToByte("3");

            MBmaster.ReadHoldingRegister(ID, unit, StartAddress, Length);
        }

        public void lireBadge()
        {
            ushort ID = 1;

            byte unit = Convert.ToByte("1");
            ushort StartAddress = ReadStartAdr();
            //byte Length = Convert.ToByte(txtSize.Text);
            byte Length = Convert.ToByte("32");



            MBmaster.ReadHoldingRegister(ID, unit, StartAddress, Length);
        }




        // ------------------------------------------------------------------------
        // Read start address
        // ------------------------------------------------------------------------
        private ushort ReadStartAdr()
        {
            ushort hex = Convert.ToUInt16("0x00", 16);
            return hex;
        }

        // add users 
        private void add_Click(object sender, RoutedEventArgs e)
        {
            Add_user add_user = new Add_user(connexion_sql);
            add_user.Set_TextBox_Badge(badgeMessage);
            add_user.ShowDialog();

        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            connexion_sql.delete_user(badgeMessage);
        }

        private void suspension_Click(object sender, RoutedEventArgs e)
        {
            connexion_sql.suspension(badgeMessage);
        }
    }
}