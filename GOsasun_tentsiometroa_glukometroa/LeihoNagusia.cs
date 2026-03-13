using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TentsiometroSimuladorea;
using GlukometroSimuladorea;

namespace tentsiometro_digitala
{
    public partial class LeihoNagusia : System.Windows.Forms.Form
    {
        private enum Egoera { Itzalita, Piztuta, Neurtzen, Emaitzak }
        private Egoera unekoEgoera = Egoera.Itzalita;

        private enum EgoeraGluko { Itzalita, Piztuta, Neurtzen, Emaitzak }
        private EgoeraGluko unekoEgoeraGluko = EgoeraGluko.Itzalita;

        private int tickCount = 0;
        private int extraTicks = 0;
        private int phaseDelayTicks = 0;
        private Random random;
        private SoundPlayer motorSoinua;
        private SoundPlayer hustuSoinua;

        // Neurketa aldagaiak
        private int helburuSistolikoa = 0;
        private int helburuDiastolikoa = 0;
        private int helburuPultsua = 0;
        private int unekoPresioa = 0;
        private bool puzteaAmaituta = false;

        // Glukometro aldagaiak
        private double glukosaEmaitza = 0;
        private int glukoTickCount = 0;

        public LeihoNagusia()
        {
            InitializeComponent();
            random = new Random();
            EguneratuPantailaEgoerarenArabera();
        }

        private void LeihoNagusia_Load(object sender, EventArgs e)
        {
            string imgDir = Path.Combine(Application.StartupPath, "ui_irudiak");
            string audioDir = Path.Combine(Application.StartupPath, "audioak");
            
            if (!Directory.Exists(imgDir)) Directory.CreateDirectory(imgDir);
            if (!Directory.Exists(audioDir)) Directory.CreateDirectory(audioDir);

            motorSoinua = new SoundPlayer();
            string audioPath = Path.Combine(audioDir, "motor.wav");
            if (File.Exists(audioPath))
            {
                motorSoinua.SoundLocation = audioPath;
                try { motorSoinua.Load(); } catch { }
            }

            hustuSoinua = new SoundPlayer();
            string hustuPath = Path.Combine(audioDir, "hustu.wav");
            if (File.Exists(hustuPath))
            {
                hustuSoinua.SoundLocation = hustuPath;
                try { hustuSoinua.Load(); } catch { }
            }

            if (File.Exists(Path.Combine(imgDir, "GOsasun_logoa.png")))
                picLogo.Image = Image.FromFile(Path.Combine(imgDir, "GOsasun_logoa.png"));
                
            if (File.Exists(Path.Combine(imgDir, "ui_tentsiometroa.png")))
                picTentsiometroa.Image = Image.FromFile(Path.Combine(imgDir, "ui_tentsiometroa.png"));

            if (File.Exists(Path.Combine(imgDir, "ui_glukometro_digitala.png")))
                picGlukometroa.Image = Image.FromFile(Path.Combine(imgDir, "ui_glukometro_digitala.png"));

            aginteTimer.Start();
            EguneratuGlukoPantaila();
        }

        private void AginteTimer_Tick(object sender, EventArgs e)
        {
             if (unekoEgoera != Egoera.Itzalita)
                 lblOrduaData.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
        }

        private void EguneratuPantailaEgoerarenArabera()
        {
            switch (unekoEgoera)
            {
                case Egoera.Itzalita:
                    lblOrduaData.Text = "";
                    lblMezua.Text = "";
                    lblSysDiaPul.Text = "";
                    lblEmaitzak.Text = "";
                    progressBar.Visible = false;
                    progressBar.Value = 0;
                    break;

                case Egoera.Piztuta:
                    lblOrduaData.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
                    lblMezua.Text = "Jarri manguitoa besoan\neta sakatu HASI";
                    lblSysDiaPul.Text = "SYS: ---\nDIA: ---\nPUL: ---";
                    lblEmaitzak.Text = "";
                    progressBar.Visible = false;
                    break;

                case Egoera.Neurtzen:
                    lblMezua.Text = "Neurtzen...";
                    lblSysDiaPul.Text = "SYS: ---\nDIA: ---\nPUL: ---";
                    lblEmaitzak.Text = "";
                    progressBar.Visible = true;
                    progressBar.Value = 0;
                    break;

                case Egoera.Emaitzak:
                    lblMezua.Text = "Neurketa burututa!";
                    lblEmaitzak.ForeColor = Color.Black;
                    progressBar.Visible = false;
                    break;
            }
        }

        private void EguneratuGlukoPantaila()
        {
            switch (unekoEgoeraGluko)
            {
                case EgoeraGluko.Itzalita:
                    lblGlukoPantaila.Text = "";
                    break;
                case EgoeraGluko.Piztuta:
                    lblGlukoPantaila.Text = "PIZTEN...\nPREST";
                    break;
                case EgoeraGluko.Neurtzen:
                    lblGlukoPantaila.Text = "NEURTZEN...";
                    break;
                case EgoeraGluko.Emaitzak:
                    lblGlukoPantaila.Text = glukosaEmaitza.ToString("F1") + "\nmg/dL";
                    break;
            }
        }

        private void BtnGlukoOK_Click(object sender, EventArgs e)
        {
            if (unekoEgoeraGluko == EgoeraGluko.Itzalita || unekoEgoeraGluko == EgoeraGluko.Emaitzak)
            {
                unekoEgoeraGluko = EgoeraGluko.Piztuta;
                EguneratuGlukoPantaila();
            }
            else if (unekoEgoeraGluko == EgoeraGluko.Piztuta)
            {
                unekoEgoeraGluko = EgoeraGluko.Neurtzen;
                EguneratuGlukoPantaila();
                glukoTickCount = 0;
                glukosaEmaitza = random.Next(70, 151) + random.NextDouble();
                btnGlukoOK.Enabled = false;
                StartGlukoTimer();
            }
        }

        private async void StartGlukoTimer()
        {
            // Simulatu neurketa 3 segundoz
            await Task.Delay(3000);
            unekoEgoeraGluko = EgoeraGluko.Emaitzak;
            EguneratuGlukoPantaila();
            btnGlukoOK.Enabled = true;

            // Gorde datuak XML berriarekin (glukosa bakarrik momentuz)
            GlukometroIrakurketa irakurketa = new GlukometroIrakurketa(glukosaEmaitza);
            GordeDatuakXML(null, irakurketa);
        }

        private void BtnHasi_Click(object sender, EventArgs e)
        {
            if (unekoEgoera == Egoera.Itzalita || unekoEgoera == Egoera.Emaitzak)
            {
                unekoEgoera = Egoera.Piztuta;
                EguneratuPantailaEgoerarenArabera();
            }
            else if (unekoEgoera == Egoera.Piztuta)
            {
                unekoEgoera = Egoera.Neurtzen;
                EguneratuPantailaEgoerarenArabera();
                btnHasi.Enabled = false;
                tickCount = 0;
                extraTicks = 0;
                phaseDelayTicks = 0;
                
                // Helburuko balioak prestatu aldez aurretik
                helburuSistolikoa = random.Next(100, 151); // 100 - 150
                helburuDiastolikoa = random.Next(60, 101); // 60 - 100
                if (helburuDiastolikoa >= helburuSistolikoa) helburuDiastolikoa = helburuSistolikoa - 20;
                helburuPultsua = random.Next(60, 101);

                unekoPresioa = 0;
                puzteaAmaituta = false;

                if (motorSoinua != null && !string.IsNullOrEmpty(motorSoinua.SoundLocation))
                {
                    try { motorSoinua.PlayLooping(); } catch { }
                }
                timer1.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            tickCount++;
            int unekoPultsua = random.Next(helburuPultsua - 3, helburuPultsua + 4); 

            // PUZTE FASEA (Uneko presioa Sistolikoaren lerroan agertzen da igotzen)
            if (!puzteaAmaituta)
            {
                if (tickCount % 2 == 0) 
                    unekoPresioa += 3;

                if (phaseDelayTicks < 40) // 2 segundoko itxaronaldia hasieran
                {
                    phaseDelayTicks++;
                    lblSysDiaPul.Text = $"SYS: ---\nDIA: ---\nPUL: {unekoPultsua,3}";
                }
                else
                {
                    lblSysDiaPul.Text = $"SYS: {unekoPresioa,3}\nDIA: ---\nPUL: {unekoPultsua,3}";
                }
                
                lblEmaitzak.Text = "";

                if (unekoPresioa >= helburuSistolikoa)
                {
                    unekoPresioa = helburuSistolikoa;
                    puzteaAmaituta = true;
                    phaseDelayTicks = 0; // Reset delay for the next phase
                    if (motorSoinua != null) motorSoinua.Stop();

                    if (hustuSoinua != null && !string.IsNullOrEmpty(hustuSoinua.SoundLocation))
                    {
                        try { 
                            hustuSoinua.PlayLooping(); 
                        } catch { } // Error control for Soundplayer missing files
                    }
                }
                
                int prog = (int)(50.0 * unekoPresioa / helburuSistolikoa);
                if (prog > 100) prog = 100;
                progressBar.Value = prog;
            }
            // HUSTETZE FASEA (Sistolikoa finkatu, presio-jaitsiera Diastolikoan erakutsi)
            else
            {
                if (unekoPresioa > helburuDiastolikoa)
                {
                    if (tickCount % 2 == 0) 
                        unekoPresioa -= 2;
                }
                else
                {
                    unekoPresioa = helburuDiastolikoa;
                    extraTicks++;
                }

                if (phaseDelayTicks < 40) // 2 segundoko itxaronaldia hustutzen hastean
                {
                    phaseDelayTicks++;
                    lblSysDiaPul.Text = $"SYS: {helburuSistolikoa,3}\nDIA: ---\nPUL: {unekoPultsua,3}";
                }
                else
                {
                    lblSysDiaPul.Text = $"SYS: {helburuSistolikoa,3}\nDIA: {unekoPresioa,3}\nPUL: {unekoPultsua,3}";
                }
                
                lblEmaitzak.Text = "";

                if (extraTicks >= 60) // 3 segundo gehiago (50ms * 60 = 3000ms)
                {
                    progressBar.Value = 100;
                    timer1.Stop();
                    if (hustuSoinua != null) hustuSoinua.Stop();
                    btnHasi.Enabled = true;

                    ErakutsiEmaitzak();
                    return;
                }
                
                int totalPhase2Work = (helburuSistolikoa - helburuDiastolikoa) + 60;
                int currentPhase2Work = (helburuSistolikoa - unekoPresioa) + extraTicks;
                int prog = 50 + (int)(50.0 * currentPhase2Work / totalPhase2Work);
                
                if (prog > 100) prog = 100; else if (prog < 0) prog = 0;
                progressBar.Value = prog;
            }
        }

        private void ErakutsiEmaitzak()
        {
            unekoEgoera = Egoera.Emaitzak;
            EguneratuPantailaEgoerarenArabera();

            lblSysDiaPul.Text = $"SYS: {helburuSistolikoa,3}\nDIA: {helburuDiastolikoa,3}\nPUL: {helburuPultsua,3}";
            lblEmaitzak.Text = "";

            TentsiometroIrakurketa irakurketa = new TentsiometroIrakurketa(helburuSistolikoa, helburuDiastolikoa, helburuPultsua);
            GordeDatuakXML(irakurketa, null);
        }

        private async void GordeDatuakXML(TentsiometroIrakurketa tIrakurketa, GlukometroIrakurketa gIrakurketa)
        {
            string karpetaBidea = @"C:\Apache24-64\htdocs\neurketak";
            string karpetaLokala = Path.Combine(Application.StartupPath, "neurketak");
            
            string prefizua = gIrakurketa != null ? "GLUCO_" : "TENS_";
            string izenaDataOrdua = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string izenaFitxategia = prefizua + izenaDataOrdua + ".xml";
            string fitxategiaBidea = Path.Combine(karpetaBidea, izenaFitxategia);
            string fitxategiaBideaLokala = Path.Combine(karpetaLokala, izenaFitxategia);

            try
            {
                // 1. Prestatu Dokumentua DB taula egiturarekin
                XElement neurketaBerria = new XElement("Neurketa",
                    new XElement("paziente_id", 1),
                    new XElement("glukosa_mg_dl", gIrakurketa != null ? gIrakurketa.Glukosa.ToString("F2", CultureInfo.InvariantCulture) : null),
                    new XElement("tentsio_sistolikoa", tIrakurketa != null ? tIrakurketa.Sistolikoa.ToString() : null),
                    new XElement("tentsio_diastolikoa", tIrakurketa != null ? tIrakurketa.Diastolikoa.ToString() : null),
                    new XElement("pultsua_ppm", tIrakurketa != null ? tIrakurketa.Pultsua.ToString() : null),
                    new XElement("sintomak", ""),
                    new XElement("erregistro_data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                );

                XElement root = new XElement("Neurketak", neurketaBerria);
                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                
                // 2. Gorde APACHE karpetan
                if (!Directory.Exists(karpetaBidea)) Directory.CreateDirectory(karpetaBidea);
                doc.Save(fitxategiaBidea);

                // 3. Gorde karpeta Lokalean ere (Memoria)
                if (!Directory.Exists(karpetaLokala)) Directory.CreateDirectory(karpetaLokala);
                doc.Save(fitxategiaBideaLokala);

                // 4. Bidali zerbitzarira sarean zehar (HTTP POST)
                var emaitza = await BidaliZerbitzariraHTTPBidezAsync(doc.ToString(), izenaFitxategia);
                
                if (emaitza.Success)
                {
                    MessageBox.Show(string.Format("Datuak ongi gorde dira lokalki, APACHE karpetan ETA zerbitzarian:\n{0}\n\nZERBITZARIAK DIO: {1}", izenaFitxategia, emaitza.ErrorMessage), "Gordeta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                     MessageBox.Show(string.Format("Datuak LOKALKI eta APACHEn gorde dira.\n\nERROREA ZERBITZARIAN: {0}", emaitza.ErrorMessage), "Errorea Zerbitzarian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Errorea gertatu da lokalki gordetzean: {0}", ex.Message), "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Emaitza egitura lagungarria
        private class HttpResult { public bool Success; public string ErrorMessage; }

        private async Task<HttpResult> BidaliZerbitzariraHTTPBidezAsync(string testuXML, string fitxIzena)
        {
            try
            {
                // GURE ZERBITZARIAREN IP HELBIDE ETA BIDE ZUZENA
                string urlHartzailea = "http://192.168.115.163/GOsasun/php_laguntzaileak/jaso_datuak.php"; 

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var content = new System.Net.Http.StringContent(testuXML, System.Text.Encoding.UTF8, "application/xml");
                    httpClient.DefaultRequestHeaders.Add("X-File-Name", fitxIzena); 
                    
                    // Timeout-a 5 segundora mugatu dugu
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    
                    var response = await httpClient.PostAsync(urlHartzailea, content);
                    string erantzunTestua = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                        return new HttpResult { Success = true, ErrorMessage = erantzunTestua };
                    else
                        return new HttpResult { Success = false, ErrorMessage = string.Format("HTTP {0} ({1})\nZERBITZARIAK DIO: {2}", (int)response.StatusCode, response.ReasonPhrase, erantzunTestua) };
                }
            }
            catch (TaskCanceledException)
            {
                return new HttpResult { Success = false, ErrorMessage = "TIMEOUT: Zerbitzariak ez du erantzun 5 segundotan. Ziurtatu Apache piztuta dagoela 192.168.115.163-n." };
            }
            catch (Exception ex)
            {
                return new HttpResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        private void picTentsiometroa_Click(object sender, EventArgs e)
        {

        }

        private void lblMezua_Click(object sender, EventArgs e)
        {

        }
    }
}
