using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoAssembly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.txt_code.Text = GetTest();
        }

        private void btn_sumit_Click(object sender, EventArgs e)
        {
            this.txt_result.BackColor = Color.White;

            bool err;

            CodeDriver driver = new CodeDriver();

            this.txt_result.Text = driver.ComplileAndRun(this.txt_code.Text, out err);

            if (err)
            {
                this.txt_result.BackColor = Color.Red;
            }
        }

        public string GetTest()
        {
            return @"System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for(int i=0;i<10;i++)
            {
                sb.Append(i.ToString());
            }

            Console.WriteLine(sb.ToString());";
        }

    }

    public class CodeDriver
    {
        public const string prefix =
            @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
              public static class Driver
               {
                 public static void Run()
                   {";


        public const string postfix =
            "}" +
            "}";

        public string ComplileAndRun(string input, out bool hasError)
        {
            hasError = false;

            string returnData = null;

            CompilerResults results = null;

            using (var provider = new CSharpCodeProvider())
            {
                var options = new CompilerParameters();

                //  设置在内存中使用
                options.GenerateInMemory = true;

                var sb = new StringBuilder();
                sb.Append(prefix);
                sb.Append(input);
                sb.Append(postfix);

                results = provider.CompileAssemblyFromSource(options, sb.ToString());
            }

            if (results.Errors.HasErrors)
            {
                hasError = true;

                var errorMessage = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    errorMessage.AppendFormat("{0}  {1}", error.Line, error.ErrorText);
                }
                returnData = errorMessage.ToString();

            }
            else
            {
                TextWriter temp = Console.Out;

                var writer = new StringWriter();

                Console.SetOut(writer);

                Type driverType = results.CompiledAssembly.GetType("Driver");

                driverType.InvokeMember("Run", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, null);

                Console.SetOut(temp);

                returnData = writer.ToString();
            }

            return returnData;
        }
    }
}
