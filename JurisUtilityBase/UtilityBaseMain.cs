using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
using Gizmox.Controls;
using JDataEngine;
using JurisAuthenticator;
using JurisUtilityBase.Properties;
using System.Data.OleDb;

namespace JurisUtilityBase
{
    public partial class UtilityBaseMain : Form
    {
        #region Private  members

        private JurisUtility _jurisUtility;

        #endregion

        #region Public properties

        public string CompanyCode { get; set; }

        public string JurisDbName { get; set; }

        public string JBillsDbName { get; set; }

        public int FldClient { get; set; }

        public int FldMatter { get; set; }

        #endregion

        #region Constructor

        public UtilityBaseMain()
        {
            InitializeComponent();
            _jurisUtility = new JurisUtility();
        }

        #endregion

        #region Public methods

        public void LoadCompanies()
        {
            var companies = _jurisUtility.Companies.Cast<object>().Cast<Instance>().ToList();
//            listBoxCompanies.SelectedIndexChanged -= listBoxCompanies_SelectedIndexChanged;
            listBoxCompanies.ValueMember = "Code";
            listBoxCompanies.DisplayMember = "Key";
            listBoxCompanies.DataSource = companies;
//            listBoxCompanies.SelectedIndexChanged += listBoxCompanies_SelectedIndexChanged;
            var defaultCompany = companies.FirstOrDefault(c => c.Default == Instance.JurisDefaultCompany.jdcJuris);
            if (companies.Count > 0)
            {
                listBoxCompanies.SelectedItem = defaultCompany ?? companies[0];
            }
        }

        #endregion

        #region MainForm events

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void listBoxCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_jurisUtility.DbOpen)
            {
                _jurisUtility.CloseDatabase();
            }
            CompanyCode = "Company" + listBoxCompanies.SelectedValue;
            _jurisUtility.SetInstance(CompanyCode);
            JurisDbName = _jurisUtility.Company.DatabaseName;
            JBillsDbName = "JBills" + _jurisUtility.Company.Code;
            _jurisUtility.OpenDatabase();
            if (_jurisUtility.DbOpen)
            {
                ///GetFieldLengths();
            }

        }



        #endregion

        #region Private methods

        private void DoDaFix()
        {
            string sql = "";
            string mats = "";
            List<string> indivMats = new List<string>();
            mats = getMatID("AEGI", "35445").ToString() + ",";
            indivMats.Add(getMatID("AEGI", "35445").ToString());
            mats = mats + getMatID("AIG", "35221").ToString() + "," ;
            indivMats.Add(getMatID("AEGI", "35445").ToString());
            mats = mats + getMatID("MISC", "35131").ToString() + ",";
            indivMats.Add(getMatID("AEGI", "35445").ToString());
            mats = mats + getMatID("MISC", "35257").ToString();
            indivMats.Add(getMatID("AEGI", "35445").ToString());

            sql = "update matter set matppdbalance = 0.00 where matsysnbr not in (" + mats + ")";
             _jurisUtility.ExecuteNonQuery(0, sql);

            foreach (string mat in indivMats)
            {
                sql = " SELECT Sum(CASE WHEN LedgerHistory.lhtype = '5' OR " +
                  "  LedgerHistory.lhtype = '1' THEN LedgerHistory.lhcashamt WHEN LedgerHistory.lhtype = '6' OR LedgerHistory.lhtype = 'B' THEN LedgerHistory.lhcashamt * -1 END) AS PrepaidBalance " +
                " FROM BillTo " +
                "  INNER JOIN Employee ON Employee.EmpSysNbr = BillTo.BillToBillingAtty " +
                "  INNER JOIN Matter ON BillTo.BillToSysNbr = Matter.MatBillTo " +
                "  INNER JOIN LedgerHistory ON Matter.MatSysNbr = LedgerHistory.LHMatter " +
                "  INNER JOIN Client ON Client.CliSysNbr = Matter.MatCliNbr " +
                "  INNER JOIN (SELECT client.clisysnbr, Sum(CASE WHEN ledgerhistory.lhtype = '5' OR ledgerhistory.lhtype = '1' THEN ledgerhistory.lhcashamt WHEN ledgerhistory.lhtype = '6' OR ledgerhistory.lhtype = 'B' THEN ledgerhistory.lhcashamt * -1 END) AS CPPD " +
                 "   FROM ledgerhistory " +
                 "     INNER JOIN matter ON ledgerhistory.lhmatter = matter.matsysnbr " +
                 "     INNER JOIN client ON matter.matclinbr = client.clisysnbr " +
                 "   WHERE ledgerhistory.lhdate <= getdate() AND ledgerhistory.lhtype IN ('1', '5', '6', 'B') " +
                 "   GROUP BY client.clisysnbr) CPPD ON CPPD.CliSysNbr = Client.CliSysNbr " +
                 " WHERE LedgerHistory.lhdate <= getDate() AND LedgerHistory.lhtype IN ('1', '5', '6', 'B') AND matsysnbr = " + mat + " " +
                 "   HAVING Sum(CASE WHEN LedgerHistory.lhtype = '5' OR LedgerHistory.lhtype = '1' THEN LedgerHistory.lhcashamt WHEN LedgerHistory.lhtype = '6' OR LedgerHistory.lhtype = 'B' THEN LedgerHistory.lhcashamt * -1 END) <> 0 ";

                DataSet bal = _jurisUtility.RecordsetFromSQL(sql);
                decimal balance = 0;
                if (bal != null && bal.Tables != null && bal.Tables.Count > 0 && bal.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in bal.Tables[0].Rows)
                    {
                        balance = Convert.ToDecimal(dr[0].ToString());
                    }
                    sql = "update matter set matppdbalance = cast(" + balance + " as money) where matsysnbr = " + mat;
                    _jurisUtility.ExecuteNonQuery(0, sql);

                }
                else
                {
                    MessageBox.Show("no data for matter " + mat);
                }



            }



            MessageBox.Show("Done", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.None);
            this.Close();
        }


        private int getMatID(string client, string matter)
        {
            string sql = "select clisysnbr from client where clicode = '" + client + "'";
            DataSet dds = _jurisUtility.RecordsetFromSQL(sql);
            int matsys = 0;
            int clisys = 0;
            if (dds != null && dds.Tables != null && dds.Tables.Count > 0 && dds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dds.Tables[0].Rows)
                {
                    clisys = Convert.ToInt32(dr[0].ToString());
                }
                dds.Clear();
                sql = "select matsysnbr from matter where dbo.jfn_FormatMatterCode(matcode) = '" + matter + "' and matclinbr = " + clisys.ToString();
                dds = _jurisUtility.RecordsetFromSQL(sql);
                if (dds != null && dds.Tables.Count > 0 && dds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dds.Tables[0].Rows)
                    {
                        matsys = Convert.ToInt32(dr[0].ToString());
                    }
                    return matsys;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }

        private int getCliID(string client)
        {
            string sql = "select clisysnbr from client where clicode = '" + client + "'";
            DataSet dds = _jurisUtility.RecordsetFromSQL(sql);
            int clisys = 0;
            if (dds != null && dds.Tables != null && dds.Tables.Count > 0 && dds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dds.Tables[0].Rows)
                {
                    clisys = Convert.ToInt32(dr[0].ToString());
                }
                return clisys;
            }
            else
            {
                return 0;
            }
        }
        private bool VerifyFirmName()
        {
            //    Dim SQL     As String
            //    Dim rsDB    As ADODB.Recordset
            //
            //    SQL = "SELECT CASE WHEN SpTxtValue LIKE '%firm name%' THEN 'Y' ELSE 'N' END AS Firm FROM SysParam WHERE SpName = 'FirmName'"
            //    Cmd.CommandText = SQL
            //    Set rsDB = Cmd.Execute
            //
            //    If rsDB!Firm = "Y" Then
            return true;
            //    Else
            //        VerifyFirmName = False
            //    End If

        }

        private bool FieldExistsInRS(DataSet ds, string fieldName)
        {

            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                if (column.ColumnName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }


        private static bool IsDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum; 
        }

        private void WriteLog(string comment)
        {
            var sql =
                string.Format("Insert Into UtilityLog(ULTimeStamp,ULWkStaUser,ULComment) Values('{0}','{1}', '{2}')",
                    DateTime.Now, GetComputerAndUser(), comment);
            _jurisUtility.ExecuteNonQueryCommand(0, sql);
        }

        private string GetComputerAndUser()
        {
            var computerName = Environment.MachineName;
            var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var userName = (windowsIdentity != null) ? windowsIdentity.Name : "Unknown";
            return computerName + "/" + userName;
        }

        /// <summary>
        /// Update status bar (text to display and step number of total completed)
        /// </summary>
        /// <param name="status">status text to display</param>
        /// <param name="step">steps completed</param>
        /// <param name="steps">total steps to be done</param>


        private void DeleteLog()
        {
            string AppDir = Path.GetDirectoryName(Application.ExecutablePath);
            string filePathName = Path.Combine(AppDir, "VoucherImportLog.txt");
            if (File.Exists(filePathName + ".ark5"))
            {
                File.Delete(filePathName + ".ark5");
            }
            if (File.Exists(filePathName + ".ark4"))
            {
                File.Copy(filePathName + ".ark4", filePathName + ".ark5");
                File.Delete(filePathName + ".ark4");
            }
            if (File.Exists(filePathName + ".ark3"))
            {
                File.Copy(filePathName + ".ark3", filePathName + ".ark4");
                File.Delete(filePathName + ".ark3");
            }
            if (File.Exists(filePathName + ".ark2"))
            {
                File.Copy(filePathName + ".ark2", filePathName + ".ark3");
                File.Delete(filePathName + ".ark2");
            }
            if (File.Exists(filePathName + ".ark1"))
            {
                File.Copy(filePathName + ".ark1", filePathName + ".ark2");
                File.Delete(filePathName + ".ark1");
            }
            if (File.Exists(filePathName ))
            {
                File.Copy(filePathName, filePathName + ".ark1");
                File.Delete(filePathName);
            }

        }

            

        private void LogFile(string LogLine)
        {
            string AppDir = Path.GetDirectoryName(Application.ExecutablePath);
            string filePathName = Path.Combine(AppDir, "VoucherImportLog.txt");
            using (StreamWriter sw = File.AppendText(filePathName))
            {
                sw.WriteLine(LogLine);
            }	
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DoDaFix();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {

            this.Close();
          
        }




    }
}
