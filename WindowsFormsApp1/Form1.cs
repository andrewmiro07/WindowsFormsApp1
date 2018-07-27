using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        WebBrowser wb = new WebBrowser();
        string[] arrNum = new string[0];
        string[] arrTime = new string[0];
        Boolean isautorize = false;

        public Form1()
        {
            InitializeComponent();
            cb_crdt.Checked = false;
            tb_login.Enabled = false;
            tb_pass.Enabled = false;
            cb_period.SelectedIndex = 0;
            Autorize();
        }

        private void Autorize()
        {
            try
            {
                string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/rdmncrdt.txt";
                if (tb_login.Text == "" || tb_pass.Text == "")
                {
                    StreamReader sr = new StreamReader(currentPath);
                    string[] crdtbls = sr.ReadLine().Split(' ');
                    sr.Close();
                    tb_login.Text = crdtbls[0];
                    tb_pass.Text = crdtbls[1];
                }
                wb.Navigate("http://red.transset.ru/login");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                wb.Document.GetElementById("username").SetAttribute("value", tb_login.Text);
                wb.Document.GetElementById("password").InnerText = tb_pass.Text;
                foreach (HtmlElement he in wb.Document.GetElementsByTagName("input"))
                {
                    if (he.GetAttribute("value").Equals("Вход »"))
                    {
                        he.InvokeMember("click");
                    }
                }
                Thread.Sleep(5000);
                if (wb.DocumentText.IndexOf("Неправильное имя пользователя или пароль") >= 0)
                {
                    MessageBox.Show("Неправильное имя пользователя или пароль =/", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cb_crdt.Checked = true;
                }
                else
                {
                    isautorize = true;
                    StreamWriter sw = new StreamWriter(currentPath);
                    sw.Write(tb_login.Text + " " + tb_pass.Text);
                    sw.Close();
                }
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось выполнить авторизацию" + Environment.NewLine + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Parse_time()
        {
            string tmp_str = "";
            try {
                cb_crdt.Checked = false;
                if (!isautorize)
                    Autorize();
                string url = "http://red.transset.ru/time_entries/report?utf8=%E2%9C%93&criteria%5B%5D=issue&f%5B%5D=spent_on&op%5Bspent_on%5D=%3E%3C&v%5Bspent_on%5D%5B%5D=" + dtp_periodB.Value.ToString("yyyy-MM-dd") + "&v%5Bspent_on%5D%5B%5D=" + dtp_periodE.Value.ToString("yyyy-MM-dd") + "&f%5B%5D=user_id&op%5Buser_id%5D=%3D&v%5Buser_id%5D%5B%5D=me&f%5B%5D=activity_id&op%5Bactivity_id%5D=%21&v%5Bactivity_id%5D%5B%5D=23&v%5Bactivity_id%5D%5B%5D=30&f%5B%5D=project_id&op%5Bproject_id%5D=%3D&v%5Bproject_id%5D%5B%5D=55&v%5Bproject_id%5D%5B%5D=124&v%5Bproject_id%5D%5B%5D=7&f%5B%5D=&c%5B%5D=project&c%5B%5D=spent_on&c%5B%5D=user&c%5B%5D=activity&c%5B%5D=issue&c%5B%5D=comments&c%5B%5D=hours&columns=month&criteria%5B%5D=";
                //string url = @"C:\Users\mironov_ai\Downloads\Telegram Desktop\Затраченное_время_Отчёт_Управление.html";
                //Console.WriteLine(url);
                wb.Navigate(url);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                string[] pageFull = wb.DocumentText.Split('\n');
                int i = 0, j = 0;
                Boolean flag_1 = false, flag_2 = true;
                Regex pattern_num = new Regex(@"\#\d*:");
                Regex pattern_hour = new Regex(@"hours-int.>\d*");
                Regex pattern_hourdec = new Regex(@"hours-dec.>\.\d*");

                while ((tmp_str = pageFull[j]) != null)
                {
                    j++;
                    if (!flag_1 && tmp_str.IndexOf("last-level") >= 0)
                    {
                        flag_1 = true;
                    }
                    if (tmp_str != "" && flag_1 && flag_2)
                    {
                        if (tmp_str.IndexOf("#") >= 0)
                        {
                            Array.Resize(ref arrNum, arrNum.Length + 1);
                            Match match = pattern_num.Match(tmp_str);
                            arrNum[i] = match.Value.Remove(match.Value.Length - 1).Remove(0, 1);
                        }
                        if (tmp_str.IndexOf("hours-int") >= 0)
                        {
                            Array.Resize(ref arrTime, arrTime.Length + 1);
                            Match match = pattern_hour.Match(tmp_str);
                            arrTime[i] = match.Value.Remove(0, 11);
                        }
                        if (tmp_str.IndexOf("hours-dec") >= 0)
                        {
                            Match match = pattern_hourdec.Match(tmp_str);
                            arrTime[i] += match.Value.Remove(0, 11);
                            i++;
                            flag_2 = !flag_2;
                        }
                    }
                    else if (tmp_str != "" && flag_1)
                    {
                        flag_2 = !flag_2;
                    }
                    if (flag_1 && tmp_str.IndexOf("Общее время") >= 0)
                    {
                        Array.Resize(ref pageFull, 0);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось прочитать документ" + Environment.NewLine + ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Set_time()
        {
            int i = 0;
            int l_size = arrNum.Length;
            string str_tmp;
            while (i != l_size - 1)
            {
                for (int j = 0; j < l_size - 1; j++)
                {
                    if (Convert.ToDouble(arrTime[j].Replace(".", ",")) < Convert.ToDouble(arrTime[j + 1].Replace(".", ",")))
                    {
                        str_tmp = arrTime[j];
                        arrTime[j] = arrTime[j + 1];
                        arrTime[j + 1] = str_tmp;
                        str_tmp = arrNum[j];
                        arrNum[j] = arrNum[j + 1];
                        arrNum[j + 1] = str_tmp;
                    }
                }
                i++;
            }
            i = 0;
            str_tmp = "";
            while (i != l_size)
            {
                if (Convert.ToDouble(arrTime[i].Replace(".", ",")) >= 0.5)
                {
                    str_tmp += arrNum[i] + "\t-\t\t\t\t" + arrTime[i] + "\thttp://red.transset.ru/issues/" + arrNum[i] + Environment.NewLine;
                }
                i++;
            }
            Clipboard.SetText(str_tmp);
            Array.Resize(ref arrNum, 0);
            Array.Resize(ref arrTime, 0);
        }

        private void bt_run_Click(object sender, EventArgs e)
        {
            if (dtp_periodB.Value > dtp_periodE.Value || dtp_periodE.Value > DateTime.Today)
            {
                MessageBox.Show("Некорректный период", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Parse_time();
            Set_time();
        }

        private void cb_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dateB = DateTime.Today;
            DateTime dateE = new DateTime();
            switch (cb_period.SelectedIndex)
            {
                case 0:
                    dtp_periodB.Enabled = false;
                    dtp_periodE.Enabled = false;
                    switch (Convert.ToInt16(dateB.DayOfWeek))
                    {
                        case 1:
                            dateB = dateB.AddDays(-7);
                            break;
                        case 2:
                            dateB = dateB.AddDays(-8);
                            break;
                        case 3:
                            dateB = dateB.AddDays(-9);
                            break;
                        case 4:
                            dateB = dateB.AddDays(-10);
                            break;
                        case 5:
                            dateB = dateB.AddDays(-11);
                            break;
                        case 6:
                            dateB = dateB.AddDays(-12);
                            break;
                        case 7:
                            dateB = dateB.AddDays(-13);
                            break;
                    }
                    dateE = dateB.AddDays(6);
                    break;
                case 1:
                    dtp_periodB.Enabled = false;
                    dtp_periodE.Enabled = false;
                    switch (Convert.ToInt16(dateB.DayOfWeek))
                    {
                        case 1:
                            break;
                        case 2:
                            dateB = dateB.AddDays(-1);
                            break;
                        case 3:
                            dateB = dateB.AddDays(-2);
                            break;
                        case 4:
                            dateB = dateB.AddDays(-3);
                            break;
                        case 5:
                            dateB = dateB.AddDays(-4);
                            break;
                        case 6:
                            dateB = dateB.AddDays(-5);
                            break;
                        case 7:
                            dateB = dateB.AddDays(-6);
                            break;
                    }
                    dateE = DateTime.Today;
                    break;
                case 2:
                    dateB = DateTime.Today.AddDays(-1);
                    dateE = DateTime.Today;
                    dtp_periodB.Enabled = true;
                    dtp_periodE.Enabled = true;
                    break;
            }
            dtp_periodB.Value = dateB;
            dtp_periodE.Value = dateE;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_crdt.Checked == true)
            {
                tb_login.Enabled = true;
                tb_pass.Enabled = true;
            }
            else
            {
                tb_login.Enabled = false;
                tb_pass.Enabled = false;
            }
        }

        private void bt_plan_Click(object sender, EventArgs e)
        {
            Set_plan(Parse_plan());
            //Clipboard.SetText(Substr(">Учет ресурсов - \rОбъекты. ЭКУ. При изменении старшего в РВБ не подтянулись изменения во \rвкладку \"Ответственные\"- вид ответственности-тех. обслуживание в \r    ЭКУ.</A></TD>", 1));
        }

        static void Set_plan(string[,] p_plan_arr)
        {
            string result = "RM\tСт\tПр\tОписание\tСрок\tРл\tФорум\tКомментарий" + Environment.NewLine;
            string executor = "";
            for (int i = 0; i < p_plan_arr.GetLength(0); i++)
            {
                if (executor != p_plan_arr[i, 6])
                {
                    executor = p_plan_arr[i, 6];
                    result += executor + Environment.NewLine;
                }
                if ((p_plan_arr[i, 1] != "П" || p_plan_arr[i, 1] == "-") && p_plan_arr[i, 4] == executor && (p_plan_arr[i, 7] != "Р" || DateTime.Today.DayOfWeek == DayOfWeek.Wednesday))
                    if (p_plan_arr[i, 0] != null)
                    {
                        for (int j = 0; j < p_plan_arr.GetLength(1); j++)
                        {
                            if (j != 6 && j != 4)
                                result += p_plan_arr[i, j];
                            if (j != 6 && j != 8 && j != 4)
                                result += "\t";
                        }
                        result += Environment.NewLine;
                    }
            }
            Clipboard.SetText(result);
        }

        private string[,] Parse_plan()
        {
            string[,] plan_arr = new string[100, 9];
            int tr = -2, td = -2;
            string strt = "";
            try
            {
                if (!isautorize)
                    Autorize();
                string url = "http://red.transset.ru/projects/support-projects-css/issues?utf8=%E2%9C%93&set_filter=1&f%5B%5D=status_id&op%5Bstatus_id%5D=o&f%5B%5D=cf_71&op%5Bcf_71%5D=%3D&v%5Bcf_71%5D%5B%5D=475&v%5Bcf_71%5D%5B%5D=69&v%5Bcf_71%5D%5B%5D=535&v%5Bcf_71%5D%5B%5D=334&v%5Bcf_71%5D%5B%5D=611&f%5B%5D=assigned_to_id&op%5Bassigned_to_id%5D=%3D&v%5Bassigned_to_id%5D%5B%5D=475&v%5Bassigned_to_id%5D%5B%5D=69&v%5Bassigned_to_id%5D%5B%5D=535&v%5Bassigned_to_id%5D%5B%5D=334&v%5Bassigned_to_id%5D%5B%5D=611&f%5B%5D=&c%5B%5D=status&c%5B%5D=priority&c%5B%5D=subject&c%5B%5D=assigned_to&c%5B%5D=due_date&c%5B%5D=cf_71&c%5B%5D=cf_175&c%5B%5D=cf_179&group_by=";
                //Console.WriteLine(url);
                wb.Navigate(url);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                string[] pageFull = wb.DocumentText.Split('\n');
                Boolean flag = false;

                for (int i = 0; i < 100; i++)
                    for (int j = 0; j < 9; j++)
                        plan_arr[i, j] = "";

                foreach (var str in pageFull)
                {
                    if (!flag && str.IndexOf("list issues") != -1)
                        flag = true;

                    if (flag && str.IndexOf("<TR") != -1)
                    {
                        tr++;
                        td = -2;
                    }
                    if (flag && str.IndexOf("<TD class") != -1 && tr >= 0)
                        td++;
                    if (td == 0) //номер в RM
                        plan_arr[tr, td] = Substr(str, 1);
                    else if (td == 1) //статус
                        plan_arr[tr, td] = ReadStatus(Substr(str, 0));
                    else if (td == 2) //приоритет
                        plan_arr[tr, td] = ReadPriority(Substr(str, 0));
                    else if (td == 3) //тема
                        plan_arr[tr, td] += str; //= Substr(str, 1);
                    else if (td == 4) //назначена
                        plan_arr[tr, td] += str; //= Substr(str, 1);
                    else if (td == 5)
                    { //дата выполнения
                        //plan_arr[tr, td] = Substr(str, 0);
                        strt = Substr(str, 0);
                        if (strt != "") {
                            DateTime data = DateTime.ParseExact(strt, "yyyy-M-dd", null);
                            plan_arr[tr, td] = data < DateTime.Today ? (Substr(str, 0) + " Просрочка!") : Substr(str, 0);
                        }
                        else
                            plan_arr[tr, td] = Substr(str, 0);
                    }
                    else if (td == 6) //исполнитель
                        plan_arr[tr, td] = Substr(str, 1);
                    else if (td == 7) //готовность к релизу
                        plan_arr[tr, td] = Substr(str, 0);
                    else if (td == 8) //номер задачи ТП
                        plan_arr[tr, td] += str; //= Substr(str, 0);
                    if (flag && str.IndexOf("</table>") != -1)
                        break;
                }

                for (int i = 0; i < 100; i++)
                    if (plan_arr[i, 0] != "")
                    {
                        plan_arr[i, 3] = Substr(plan_arr[i, 3], 1);
                        plan_arr[i, 4] = Substr(plan_arr[i, 4], 1);
                        if (plan_arr[i, 7].IndexOf("К релизу") != -1)
                            plan_arr[i, 7] = "Р";
                        plan_arr[i, 8] = Substr(plan_arr[i, 8], 0);
                    }

                plan_arr = Sort(plan_arr, 6);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось прочитать документ" + Environment.NewLine +
                    td + " " + tr + " " + strt + Environment.NewLine +
                    ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return plan_arr;
        }

        private string[,] Sort(string[,] p_arr, int p_j)
        {
            int l_size = p_arr.GetLength(0);
            int l_length = p_arr.GetLength(1);
            int l_uniq = 0;
            string[] tmp = new string[l_length];
            string[] uniq_arr = new string[l_size];
            string[,] sort_arr = new string[l_size, l_length];
            Boolean flag = false;
            int step = 0;

            for (int i = 0; i < l_size; i++)
            {
                if (p_arr[i, 0] != "")
                {
                    foreach (var item in uniq_arr)
                        if (p_arr[i, p_j] == item)
                        {
                            flag = true;
                            break;
                        }
                    if (!flag)
                        for (int k = 0; k < l_size; k++)
                            if (uniq_arr[k] == null)
                            {
                                uniq_arr[k] = p_arr[i, p_j];
                                l_uniq = k + 1;
                                break;
                            }
                    flag = false;
                }
                else
                    break;
            }

            for (int i = 0; i < l_uniq; i++)
            {
                for (int j = 0; j < l_size; j++)
                    if (p_arr[j, 0] != "")
                    {
                        if (uniq_arr[i] == p_arr[j, p_j])
                        {
                            for (int k = 0; k < l_length; k++)
                                sort_arr[step, k] = p_arr[j, k];
                            step++;
                        }
                    }
                    else
                        break;
            }

            return sort_arr;
    }

        private string Substr(string p_str, int p_sw)
        {
            Regex pattern;
            Match match;
            string result = "";
            switch (p_sw)
            {
                case 0:
                    if (p_str.IndexOf("</TR>") != -1)
                        p_str = p_str.Remove(p_str.Length - 6);
                    pattern = new Regex(@">.*<\/TD>");
                    match = pattern.Match(p_str);
                    result = match.Value.Remove(match.Value.Length - 5).Remove(0, 1);
                    break;
                case 1:
                    pattern = new Regex(@">.*<\/A><\/TD>");
                    match = pattern.Match(p_str);
                    result = match.Value.Remove(match.Value.Length - 9);
                    result = result.Remove(0, result.LastIndexOf(">") + 1);
                    if (result.IndexOf("ЛРП") == 0)
                        result = result.Remove(0, 11);
                    if (result.IndexOf(" ") == 0)
                        result = result.Remove(0, 1);
                    while (result.IndexOf("  ") != -1)
                        result = result.Replace("  ", " ");
                    if (result.IndexOf("\r") != -1 || result.IndexOf("\n") != -1)
                        result = result.Replace("\n", "").Replace("\r", "");
                    break;
            }
            return result;
        }

        static string ReadStatus(string p_str)
        {
            string result = "";
            if (p_str == "Запрос информации")
                result = "З";
            else if (p_str == "Новая")
                result = "Н";
            else if (p_str == "В работе")
                result = "Р";
            else if (p_str == "Приостановлена")
                result = "П";
            else if (p_str == "Решена")
                result = "Ж";
            return result;
        }

        static string ReadPriority(string p_str)
        {
            string result = "";
            if (p_str == "Нормальный")
                result = "Н";
            else if (p_str == "Высокий")
                result = "В";
            else if (p_str == "Срочный")
                result = "С";
            return result;
        }
    }
}
