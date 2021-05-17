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

        public fenetreModbus()
        {
            InitializeComponent();

            timer = new DispatcherTimer();

            // Initialisation du timer
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200); // 200ms
            //timer.Start();

            badgeMessage = "";

            write = false;
            blank = false;

            lblStatus.Visibility = Visibility.Hidden;
            spEcrire.Visibility = Visibility.Hidden;

            lblBadge.FontSize = 16;
            lblBadge.Content = "non connecté";
            lblBadge.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#cccccc"));
            btnEcrireUnBadge.Visibility = Visibility.Hidden;
            lblErreur.Content = "Indiquez l'adresse IP de votre lecteur RFID puis cliquez sur \"connecter\"";

            //connecter();

        }

        //private void executer_timerAcquisitionVelo()
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!write)
            {
                lireBadge();

                lblBadge.FontSize = 48;
                lblBadge.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0000ff"));

                if (blank) lblBadge.Content = "";
                else lblBadge.Content = badgeMessage;
            }
            else
            {
                ecrireBadge();
                lireBadge();

                if (badgeMessage == txtMessage.Text)
                {
                    write = false;
                    blank = false;

                    spEcrire.Visibility = Visibility.Hidden;
                    btnEcrireUnBadge.Content = "Ecrire un badge";
                    btnEcrireUnBadge.Visibility = Visibility.Visible;

                    txtMessage.Text = "";

                    lblErreur.Content = "Passez un badge devant le lecteur";
                }
            }

            //lblErreur.Content = erreurMessage;
        }

        private void btnConnecter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnConnecter.Content.ToString() == "Connecter")
                {
                    bool isConnected = connecter();

                    if (isConnected)
                    {
                        txtIP.Visibility = Visibility.Hidden;

                        lblStatus.Content = "Connexion établie > " + txtIP.Text;
                        lblStatus.Visibility = Visibility.Visible;

                        btnConnecter.Content = "Déconnecter";

                        lblErreur.Content = "Passez un badge devant le lecteur";
                        btnEcrireUnBadge.Visibility = Visibility.Visible;

                        timer.Start();
                    }
                }
                else
                {
                    bool isConnected = deconnecter();

                    if (!isConnected)
                    {
                        lblStatus.Visibility = Visibility.Hidden;
                        txtIP.Visibility = Visibility.Visible;

                        lblBadge.FontSize = 16;
                        lblBadge.Content = "non connecté";
                        lblBadge.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#cccccc"));
                        btnEcrireUnBadge.Visibility = Visibility.Hidden;
                        lblErreur.Content = "Indiquez l'adresse IP de votre lecteur RFID puis cliquez sur \"connecter\"";

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
                MBmaster = new Master(txtIP.Text, 502);
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

        private void lireBadge()
        {
            ushort ID = 1;

            byte unit = Convert.ToByte("1");
            ushort StartAddress = ReadStartAdr();
            //byte Length = Convert.ToByte(txtSize.Text);
            byte Length = Convert.ToByte("32");



            MBmaster.ReadHoldingRegister(ID, unit, StartAddress, Length);
        }

        private void ecrireBadge()
        {
            try
            {
                byte[] message = Encoding.Default.GetBytes(txtMessage.Text + "\0");

                ushort ID = 1;
                byte unit = Convert.ToByte("1");
                ushort StartAddress = Convert.ToUInt16("0x00", 16);

                MBmaster.WriteMultipleRegister(ID, unit, StartAddress, message);
            }
            catch
            {
                MessageBox.Show("Erreur écriture badge.");
            }
        }



        // ------------------------------------------------------------------------
        // Read start address
        // ------------------------------------------------------------------------
        private ushort ReadStartAdr()
        {
            ushort hex = Convert.ToUInt16("0x00", 16);
            return hex;
        }

        private void btnEcrireUnBadge_Click(object sender, RoutedEventArgs e)
        {
            if (!write)
            {
                btnEcrireUnBadge.Visibility = Visibility.Hidden;
                spEcrire.Visibility = Visibility.Visible;
                blank = true;
                lblErreur.Content = "Saisissez le texte à transférer sur le badge puis validez.";
            }
            else
            {
                spEcrire.Visibility = Visibility.Hidden;
                btnEcrireUnBadge.Content = "Ecrire un badge";
                btnEcrireUnBadge.Visibility = Visibility.Visible;

                write = false;
                blank = false;

                txtMessage.Text = "";
                badgeMessage = "";

                lblErreur.Content = "Passez un badge devant le lecteur";
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            spEcrire.Visibility = Visibility.Hidden;
            btnEcrireUnBadge.Content = "Ecrire un badge";
            btnEcrireUnBadge.Visibility = Visibility.Visible;

            write = false;
            blank = false;

            txtMessage.Text = "";
            badgeMessage = "";

            lblErreur.Content = "Passez un badge devant le lecteur";
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            if (!write)
            {
                spEcrire.Visibility = Visibility.Hidden;
                lblBadge.Content = "Veuillez présenter un badge...";
                lblBadge.FontSize = 16;

                lblBadge.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff0000"));

                write = true;

                btnEcrireUnBadge.Content = "Annuler";
                btnEcrireUnBadge.Visibility = Visibility.Visible;

                lblErreur.Content = "";
            }
        }
    }
}