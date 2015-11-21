using System;
using System.Windows.Forms;

namespace AllgLib
{
    public static class DeEncoding
    {
        public static string Encode(string s)
        {
            string ret = "";
            try
            {
                byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(s);
                ret = Convert.ToBase64String(encbuff);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Fehler beim Verschlüsseln");
            }
            return ret;
        }
        public static string Decode(string s)
        {
            string ret = "";
            try
            {
                byte[] decbuff = Convert.FromBase64String(s);
                ret = System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fehler beim Entschlüsseln");
            }
            return ret;
        }
    }
}
