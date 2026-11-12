using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace filecreation
{
    internal class Program
    {
       static string date ,ndate;
        static void Main(string[] args)
        {
            //Console.WriteLine(convert("2025"));
            string file;
            string date = "";
           createefile();
           createsfile();
          createeifile();
               //Console.WriteLine(getdate(extract("26022715092600180185000998054218081")));
                Console.ReadLine();
            }

           static SqlConnectionStringBuilder CreateConnectionStringBuilder()
           {
               string connectionStringPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connectionString.txt");
               if (!File.Exists(connectionStringPath))
               {
                   throw new FileNotFoundException("Create connectionString.txt next to the executable and put the SQL connection string in it.", connectionStringPath);
               }

               string connectionString = File.ReadAllText(connectionStringPath).Trim();
               if (string.IsNullOrWhiteSpace(connectionString))
               {
                   throw new InvalidOperationException("The connectionString.txt file is empty.");
               }

               return new SqlConnectionStringBuilder(connectionString);
           }

           public static string buildNumber(string nis)
        {
            if (nis.Length < 10)
            {
                nis = nis.PadLeft(10, '0');
            }
            return nis;
        }
        public static string getfilename()
        {
            string date1 = DateTime.Now.ToString("yyyy");
            string date2 = DateTime.Now.ToString("MMM");
            string date3 = DateTime.Now.ToString("dd");
            string date4 = DateTime.Now.ToString("MMMM").ToUpper();
            string file = @"C:\Data\EZI " + date2 + " " + date3 + " " + date1 + ".txt";
            //Console.ReadLine(); ;
            return file;
        }
        static string convert(string input)
        {
            string temp=input.Substring(2);
            return temp+"##";
        }
        static string extract(string input)
        {
            string temp = input.Substring(0,20);
            Console.WriteLine(temp);
            return temp;
        }
        public static int getnumber()
        {
            int number1 = 0;
            try
            {
                string value;

                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    // Console.WriteLine(code);
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.counter", connection);
                    // command1.Parameters.AddWithValue("@zip", code);
                    // int result = command.ExecuteNonQuery();
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine(String.Format("{0}", reader[0]));
                                value = reader[0].ToString();
                                number1 = Int32.Parse(value);
                                Console.WriteLine(number1);
                                number1 = number1 + 1;

                            }
                        }
                    }
                    connection.Close();
                    using (SqlConnection connection1 = new SqlConnection(builder.ConnectionString))
                    {
                        connection1.Open();
                        String query = "UPDATE counter SET count = @rnumber ";
                        using (SqlCommand command = new SqlCommand(query, connection1))
                        {
                            //command.Parameters.AddWithValue("@code", code);
                            command.Parameters.AddWithValue("@rnumber", number1);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                Console.WriteLine("No record found with the provided code.");
                            }
                        }
                        connection.Close();






                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return number1;
        }
        public static int resetnumber()
        {
            int number1 = 0;
            try
            {
                string value;

                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    // Console.WriteLine(code);
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.counter", connection);
                    // command1.Parameters.AddWithValue("@zip", code);
                    // int result = command.ExecuteNonQuery();
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine(String.Format("{0}", reader[0]));
                                value = reader[0].ToString();
                                number1 = Int32.Parse(value);
                                Console.WriteLine(number1);
                                number1 = number1 + 1;

                            }
                        }
                    }
                    connection.Close();
                    using (SqlConnection connection1 = new SqlConnection(builder.ConnectionString))
                    {
                        connection1.Open();
                        String query = "UPDATE counter SET count = 0 ";
                        using (SqlCommand command = new SqlCommand(query, connection1))
                        {
                            //command.Parameters.AddWithValue("@code", code);
                            command.Parameters.AddWithValue("@rnumber", number1);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                Console.WriteLine("No record found with the provided code.");
                            }
                        }
                        connection.Close();






                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return number1;
        }
        static string getdate(string input)
        {
            string date="";
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.nheader where TransID=@trans", connection);
                    command1.Parameters.AddWithValue("@trans", input);
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            date = reader[3].ToString();
                            

                        }
                    }
                    connection.Close();
                }
               
            }
            catch (Exception e) { Console.WriteLine(e); }

            return date;
            
        }
       
       static  string getefdate()
        {
            string temp = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(temp).ToString("yyyyMMdd");
            return "BILL_EMP_CON_EZPAY_"+createddate;
        }
        static string geteifdate()
        {
            string temp = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(temp).ToString("yyyyMMdd");
            return "BILL_EMP_INT_EZPAY_" + createddate;
        }
        static string getsfdate()
        {
            string temp = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(temp).ToString("yyyyMMdd");
            return "BILL_SELF_EZPAY_" + createddate;
        }
        static void createsfile() {



            // int flag = 1;
            string transid,nis,etype,per,pay,cacc,fdate;
            string otransid="";
            decimal amt = 0;
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();

                fdate = getsfdate();
                string filePath = @"\\bginis-fs4\Data\Computer\Electronic Payment Collections\ezpay\data_files\test_files\original_files\" + fdate + ".csv";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.selfe where flag=0", connection);
                    //command1.Parameters.AddWithValue("@trans", transid);
                    using (var writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("bank_receipt_number,amount_paid,payment_date,bp_number,bp_number_valid,contract_number_valid,contribution_contract_number,contract_type,sap_bank_code,contribution_period,file_name");
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                            while (reader.Read())
                            {
                                transid = reader[0].ToString();
                                otransid = transid;
                                nis = reader[1].ToString();
                                etype = reader[2].ToString();
                                per = convert(reader[4].ToString().Trim());
                                pay = reader[5].ToString();
                                pay = convert1(pay);
                               date = getdate(extract(transid));
                                cacc = FormatNumberString(nis);
                                nis = buildNumber(nis);
                                amt = Convert.ToDecimal(pay);
                                // amt = Math.Round(amt, 2);
                                transid = transid.Remove(21, 2);
                                string line = transid + "," + pay + "," + date + "," + nis + ",TRUE,TRUE," + cacc.Trim() + "," + etype + ",EZPAY," + per + "," + fdate;
                                writer.WriteLine(line);
                                Console.WriteLine(line);
                                update2(otransid);
                                // Console.WriteLine(extract(transid));

                            }
                            }
                            
                            writer.Close();
                            connection.Close();
                           
                            copyfile(fdate + ".csv");
                            sendmail("quincy.yarde@bginis.gov.bb","Self-employed Contributions","\\billernew\\EZPAY\\"+fdate+".csv");



                        }
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }



        }

        
        public static string FormatNumberString(string number)
        {
            if(number.Length < 10)
            {
                number = number.PadLeft(10, '0');
            }
            if (number == null)
                throw new ArgumentNullException(nameof(number));
            number = number.Substring(number.Length-7);
            char firstChar = number.First();
            if (firstChar=='9')
            {
                // Pad with leading zeros to make length 6
                //string padded = number.PadLeft(6, '0');
                return "E" + number;
            }
           
            else
            {
                // If length is 6 or anything else not specified, just return original
                return "S" + number;
            }
        }
        public static string convert1(string num)
        {
            if (num.Contains("."))
            {

            }
            else
            {
                num = num + ".00";
            }


            return num;
        }
        public static void sendmail(string email, string type, string fname)
        {

            SmtpClient client = new SmtpClient();
            MailMessage objeto_mail = new MailMessage();
            client.Port = 25;
            client.Host = "Bginis-MX1";
            client.Timeout = 300000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("byer_da", "Mythd@2027");
            objeto_mail.From = new MailAddress("sysadmin@bginis.gov.bb");
            //objeto_mail.To.Add(new MailAddress(email));
            objeto_mail.To.Add(new MailAddress("david.byer@bginis.gov.bb"));
            objeto_mail.Subject = "Files to be loaded";
            objeto_mail.Body = "The follwing " + type + " file has has been created and ready to be loaded.<br> The file path is " + fname + "<br>";

            objeto_mail.IsBodyHtml = true;
            try
            {
                client.Send(objeto_mail);

            }
            catch (Exception e)
            { }

        }

        static void update(string transid)
        {
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("update dbo.emp set flag=1 where Transid=@trans", connection);
                    command1.Parameters.AddWithValue("@trans", transid);
                    command1.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
        static void update2(string transid)
        {
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("update dbo.selfe set flag=1 where Transid=@trans", connection);
                    command1.Parameters.AddWithValue("@trans", transid);
                    command1.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        static void copyfile(string filename)
        {

            string path = @"\\bginis-fs4\Data\Computer\Electronic Payment Collections\ezpay\data_files\test_files\original_files\"+filename;
            string path2 = @"\\niseccprd\billernew\EZPAY\"+filename;

            try
            {
                if (!File.Exists(path))
                {
                    // This statement ensures that the file is created,
                    // but the handle is not kept.
                    using (FileStream fs = File.Create(path)) { }
                }

                // Ensure that the target does not exist.
                if (File.Exists(path2))
                    File.Delete(path2);

                // Move the file.
                File.Copy(path, path2);
                Console.WriteLine("{0} was moved to {1}.", path, path2);

                // See if the original exists now.
                if (File.Exists(path))
                {
                    //Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    //Console.WriteLine("The original file no longer exists, which is expected.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            Console.Read();
        }




        

        static void createefile()
        {



            // int flag = 1;
            string transid, nis, etype, per, pay, cacc, fdate,date;
            string otransid = "";
            decimal amt = 0;
            int rows = 0;
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                fdate = getefdate();
                string filePath = @"\\bginis-fs4\Data\Computer\Electronic Payment Collections\ezpay\data_files\test_files\original_files\" + fdate + ".csv";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.emp where flag=0 and Ttype='Contributions'", connection);
                    //command1.Parameters.AddWithValue("@trans", transid);
                    using (var writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("bank_receipt_number,amount_paid,payment_date,bp_number,bp_number_valid,contract_number_valid,contribution_contract_number,contract_type,sap_bank_code,contribution_period,file_name");
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    transid = reader[0].ToString();
                                    otransid = transid;
                                    nis = reader[1].ToString();
                                    etype = reader[2].ToString();
                                    per = reader[4].ToString().Trim();
                                    pay = reader[5].ToString();
                                    pay = convert1(pay);
                                    date = getdate(extract(transid));
                                    cacc = cacc = FormatNumberString(nis);
                                    fdate = getefdate();
                                    // nis = buildNumber(nis);
                                     transid = transid.Remove(21, 2);
                                    //Remove(indexToRemove, 1

                                     amt = Convert.ToDecimal(pay);
                                    //amt=Math.Round(amt,2);
                                    string line = transid + "," + pay + "," + date + "," + nis + ",TRUE,TRUE," + cacc.Trim() + "," + etype + ",EZPAY," + per + "," + fdate;
                                    writer.WriteLine(line);
                                    rows++;
                                    Console.WriteLine(line);
                                    update(otransid);

                                }
                            }
                           
                            }
                            writer.Close();
                            connection.Close();
                            copyfile(fdate + ".csv");
                        sendmail("quincy.yarde@bginis.gov.bb", "Employers Contributions", "\\billernew\\EZPAY\\" + fdate + ".csv");
                    }
                    }
                
            }
            catch (Exception e) { Console.WriteLine(e); }



        }


        static void createeifile()
        {



            // int flag = 1;
            string transid, nis, etype, per, pay, cacc, fdate, date;
            string otransid = "";
            decimal amt = 0;
            string file=getfilename();
            Console.WriteLine(file);
            int rows = 0;
            try
            {
                SqlConnectionStringBuilder builder = CreateConnectionStringBuilder();
                fdate = geteifdate();
                //Console.WriteLine(fdate);
                string filePath =  file;
                // string filePath = @"\\bginis-fs4\Data\Computer\Electronic Payment Collections\ezpay\data_files\test_files\original_files\" + fdate + ".csv";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from dbo.emp where flag=0 and Ttype='Interests'", connection);
                    //command1.Parameters.AddWithValue("@trans", transid);
                   // using (var writer = new StreamWriter(filePath))
                   // {
                       // writer.WriteLine("bank_receipt_number,amount_paid,payment_date,bp_number,bp_number_valid,contract_number_valid,contribution_contract_number,contribution_contract_number,sap_bank_code,contribution_period,file_name");
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                int num= getnumber();
                                transid = reader[0].ToString();
                                    otransid = transid;
                                    nis = reader[1].ToString();
                                    etype = reader[2].ToString();
                                    per = reader[4].ToString().Trim();
                                    pay = reader[5].ToString();
                                    pay = convert1(pay);
                                    date = getdate(extract(transid));
                                    cacc = cacc = FormatNumberString(nis);
                                    fdate = geteifdate();
                                    nis = buildNumber(nis);
                                    transid = transid.Remove(0, 10);
                                    amt = Convert.ToDecimal(pay);
                                //amt=Math.Round(amt,2);
                                string plot ="E"+ DateTime.Now.ToString("yyyyMMdd")+num.ToString();
                                string line = transid + "," + pay + "," + date + "," + nis + ",TRUE,TRUE," + cacc.Trim() + "," + etype + ",EZPAY," + per + "," + fdate;
                                   // writer.WriteLine(line);
                                    Console.WriteLine(line);
                                date = date.Replace("-", "");
                               // update(otransid);
                                AppendRow(filePath,
                        plot, "EZPay interest", plot, "112327", "NIB", "PY", "BBD",
                        date, date,date,"X",
                         "01", "Interest", "C", "P", "Y",
                        cacc.Trim(), per, "0040",pay, "X");
                                rows++;

                                }
                            }

                            //writer.Close();
                            connection.Close();
                        //copyfile(fdate + ".csv");
                        resetnumber();
                       // sendmail("carole.layne-browne@bginis.gov.bb", "Employers Interest", file);

                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }



        }


        static void AppendRow(
            string filePath,
            string paymentLot,
            string searchTerm,
            string reconciliationKey,
            string bankClearingAccount,
            string companyCode,
            string documentType,
            string currencyKey,
            string postingDate,
            string documentDate,
            string valueDate,
            string createLineItem,
            string clearingReason,
            string additionalInfo,
            string selectionCat1,
            string selectionCat2,
            string selectionCat3,
            string fieldVal1,
            string fieldVal2,
            string fieldVal3,
            string paymentAmount,
            string postOnAccount)
        {
            string[] values = {
                paymentLot,
                searchTerm,
                reconciliationKey,
                bankClearingAccount,
                companyCode,
                documentType,
                currencyKey,
                postingDate,
                documentDate,
                valueDate,
                createLineItem,
                clearingReason,
                additionalInfo,
                selectionCat1,
                selectionCat2,
                selectionCat3,
                fieldVal1,
                fieldVal2,
                fieldVal3,
                paymentAmount,
                postOnAccount
            };

            File.AppendAllText(filePath, string.Join("\t", values) + Environment.NewLine);
            Console.WriteLine($"Appended row for {paymentLot}");
        }

    }
}
