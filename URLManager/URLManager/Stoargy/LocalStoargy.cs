using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using URLManager.Core.Attribute;
using URLManager.Core.Executor;
using URLManager.Core.Executor.Base;
using URLManager.Data;
using URLManager.Stoargy.Base;

namespace URLManager.Stoargy
{
    class LocalStoargy : BaseStoargy
    {
        public LocalStoargy(string FileLocation = "URLManager.urlset")
        {

            this.FileLocation = new FileInfo(FileLocation).FullName;

            list = new StoargyItemList();
        }

        #region [   변수   ]

        /// <summary>저장될 파일의 정보입니다.</summary>
        public string FileLocation { get; set; }

        public StoargyItemList list;

        StreamWriter sw;

        private string UnknownStr = "UnknownData\\";

        #endregion

        public override bool Load(out CategoryData[] data)
        {
            StreamReader sr = new StreamReader(FileLocation);

            data = new CategoryData[] { };
            
            string filedata = "";

            try
            {
                filedata = Encoding.Default.GetString(Convert.FromBase64String(sr.ReadToEnd()));
            }
            catch (Exception)
            {
                MessageBox.Show("파일을 읽던 중에 내부 구조적으로 오류가 발생했습니다.\n더이상 읽어올 수 없습니다. 파일이 손상된 것 같습니다." +
                    "\n\n상세 정보 : 인코딩된 문자열을 디코딩 시키던 중에 문제가 발생했습니다. 파일이 임의적으로 변경된 것 같습니다.");
            }


            var spDatas = new Tuple<string, string>[] { new Tuple<string, string>("[","]"), new Tuple<string, string>("<", ">"),
                                                        new Tuple<string, string>("{", "}"), new Tuple<string, string>("(", ")")};

            string gt = UnknownStr, s = UnknownStr, g = UnknownStr;


            foreach (string d in filedata.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (d == null) continue;

                foreach (var spData in spDatas)
                {
                    if (d.StartsWith(spData.Item1) && d.EndsWith(spData.Item2))
                    {
                        string str = d.Substring(1, d.Length - 2);
                        switch (spData.Item1)
                        {
                            case "[":
                                //MessageBox.Show("GroupType Detect! Content : " + str);

                                gt = str;
                                break;
                            case "<":
                                //MessageBox.Show("Section Detect! Content : " + str);

                                s = str;
                                if (gt == UnknownStr)
                                    MessageBox.Show("파일을 읽던 중에 내부 구조적으로 오류가 발생했습니다.\n더이상 읽어올 수 없습니다. 파일이 손상된 것 같습니다." +
                                                    "\n\n상세 정보 : GroupType이 정의되지 않은 상태에서 Section이 감지되었습니다. 파일의 오류이거나 코드 상의 문제일 가능성도 있습니다.");
                                break;
                            case "{":
                                //MessageBox.Show("Group Detect! Content : " + str);

                                g = str;
                                if (gt == UnknownStr || s == UnknownStr)
                                    MessageBox.Show("파일을 읽던 중에 내부 구조적으로 오류가 발생했습니다.\n더이상 읽어올 수 없습니다. 파일이 손상된 것 같습니다." +
                                                    "\n\n상세 정보 : GroupType이나 Section이 정의되지 않은 상태에서 Group이 감지되었습니다. 파일의 오류이거나 코드 상의 문제일 가능성도 있습니다.");
                                break;
                            case "(":
                                //MessageBox.Show("Key + Value Detect! Content : " + str);

                                if (gt == UnknownStr || s == UnknownStr || g == UnknownStr)
                                    MessageBox.Show("파일을 읽던 중에 내부 구조적으로 오류가 발생했습니다.\n더이상 읽어올 수 없습니다. 파일이 손상된 것 같습니다." +
                                                    "\n\n상세 정보 : GroupType이나 Section, 혹은 Group이 정의되지 않은 상태에서 KeyValue이 감지되었습니다. 파일의 오류이거나 코드 상의 문제일 가능성도 있습니다.");


                                string ptn = "(.+?)=(.+)";

                                if (!Regex.IsMatch(str, ptn)) return false;


                                GroupCollection gc = Regex.Match(str, ptn).Groups;

                                string Key, Value;

                                Key = gc[1].Value;
                                Value = gc[2].Value;

                                list.SetValue(gt, s, g, Key, Value);

                                break;
                        }
                    }
                    else continue;
                }
            }



            foreach (var itm in list)
            {
                switch (itm.GroupType)
                {
                    case "Executor":

                        switch (itm.Section)
                        {
                            case "LocalFileExecutor":

                                break;
                            case "ProgramExecutor":
                                ProgramExecutor programexe;

                                break;
                            case "URLExecutor":
                                URLExecutor urlexe;

                                break;
                        }

                        break;
                }
            }



            return true;
        }

        public override bool Save(CategoryData[] data, bool CheckOverride)
        {
            if (CheckOverride) OnOverrideDetect();

            // Property 읽어오기
            foreach (PropertyInfo PropInfo in data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy))
            {
                bool flag = false;

                foreach (var itm in PropInfo.CustomAttributes)
                {
                    Type t = itm.AttributeType;

                    if (t == typeof(ExecutorAttribute)) flag = true;

                    if (t == typeof(LocalDataAttribute) && flag)
                    {

                        var value = (List<BaseExecutor>)PropInfo.GetValue(data, null);

                        if (value != null)
                        {
                            foreach (BaseExecutor executor in value)
                            {
                                if (executor.GetType() == typeof(LocalFileExecutor))
                                {
                                    LocalFileExecutor localExecutor = (LocalFileExecutor)executor;

                                    list.SetValue("Executor", "LocalFileExecutor", localExecutor.Name, "CanExecute", localExecutor.CanExecute.ToString());
                                    list.SetValue("Executor", "LocalFileExecutor", localExecutor.Name, "Path", localExecutor.Path);
                                }
                                else if (executor.GetType() == typeof(ProgramExecutor))
                                {
                                    ProgramExecutor progrmExecutor = (ProgramExecutor)executor;

                                    list.SetValue("Executor", "ProgramExecutor", progrmExecutor.Name, "CanExecute", progrmExecutor.CanExecute.ToString());
                                    list.SetValue("Executor", "ProgramExecutor", progrmExecutor.Name, "Path", progrmExecutor.Path);
                                }
                                else if (executor.GetType() == typeof(URLExecutor))
                                {
                                    URLExecutor urlExecutor = (URLExecutor)executor;

                                    list.SetValue("Executor", "URLExecutor", urlExecutor.Name, "CanExecute", urlExecutor.CanExecute.ToString());
                                    list.SetValue("Executor", "URLExecutor", urlExecutor.Name, "TargetURL", urlExecutor.URLLink);
                                }

                                // Executor 추가시 추가해야 할 부분
                            }
                        }

                        break;
                    }
                }
            }

            // 파일로부터 저장 시작 지점


            IEnumerable<StoargyItem> it = list.OrderBy(i => i.GroupType).ThenBy(i => i.Section).ThenBy(i => i.Group).ThenBy(i => i.Key);

            string gt = UnknownStr, s = UnknownStr, g = UnknownStr;

            StringBuilder sb = new StringBuilder();

            foreach (var itm in it)
            {
                if (gt != itm.GroupType)
                {
                    gt = itm.GroupType;
                    sb.AppendLine($"[{itm.GroupType}]");
                }
                if (s != itm.Section)
                {
                    s = itm.Section;
                    sb.AppendLine($"<{itm.Section}>");
                }
                if (g != itm.Group)
                {
                    g = itm.Group;
                    sb.AppendLine($"{{{itm.Group}}}");
                }

                sb.AppendLine($"({itm.Key}={itm.Value})");
            }

            string strdata = Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));

            //MessageBox.Show(strdata);
            //MessageBox.Show(Encoding.Default.GetString(Convert.FromBase64String(strdata)));

            sw = new StreamWriter(FileLocation);
            sw.Write(strdata);
            sw.Dispose();

            return true;
        }
    }
}
